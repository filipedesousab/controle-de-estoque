using MySql.Data.MySqlClient;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;

namespace ControleEstoque.DAL
{
    public class ProdutoVendaDal
    {
        /// <summary>
        /// Adiciona
        /// </summary>
        /// <returns>true or false</returns>
        internal bool Adicionar(List<ProdutoVenda> produtosVenda)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            int idVendaInserida = 0;
            try
            {
                if (produtosVenda != null && produtosVenda.Count > 0)
                {
                    string sql = @"INSERT INTO tb_venda
                                   (
                                       pes_id,
                                       ven_desconto,
                                       ven_valor
                                   )
                                   VALUES
                                   (
                                       @IdVendedor,
                                       @Desconto,
                                       @Valor
                                   );
                                   SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IdVendedor", produtosVenda[0].Venda.Vendedor.Id);
                    cmd.Parameters.AddWithValue("@Desconto", produtosVenda[0].Venda.Desconto);
                    cmd.Parameters.AddWithValue("@Valor", produtosVenda[0].Venda.Valor);

                    cmd.Parameters.AddWithValue("@ven_id", 0).Direction = ParameterDirection.Output; //Recebe o parâmetro do último registro
                    int lines = cmd.ExecuteNonQuery();

                    if (lines > 0) // verifica se as linhas foram adicionados no banco
                    {
                        idVendaInserida = Convert.ToInt32(cmd.Parameters["@ven_id"].Value);
                        for (int i = 0; i < produtosVenda.Count; i++)
                        {
                            string sql2 = @"INSERT INTO tb_produto_venda
                                               (
                                                   ven_id,
                                                   pro_id,
                                                   pv_quantidade,
                                                   pv_valor
                                               )
                                               VALUES
                                               (
                                                   @IdVenda,
                                                   @IdProduto,
                                                   @Quantidade,
                                                   @Valor
                                               );";
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                            cmd2.Parameters.AddWithValue("@IdVenda", idVendaInserida);
                            cmd2.Parameters.AddWithValue("@IdProduto", produtosVenda[i].Produto.Id);
                            cmd2.Parameters.AddWithValue("@Quantidade", produtosVenda[i].Quantidade);
                            cmd2.Parameters.AddWithValue("@Valor", produtosVenda[i].Valor);

                            cmd2.ExecuteNonQuery();
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    Connection.CloseConnection();
            }
        }

        /// <summary>
        /// Traz uma lista de objetos pelo seu ID passado como parametro
        /// </summary>
        /// <returns>Retorna uma lista de objetos de acordo com seu id</returns>
        internal List<ProdutoVenda> ObterRegistro(int idVenda)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                List<ProdutoVenda> produtosVenda = new List<ProdutoVenda>(); // Cria uma lista de objetos
                string query = @"SELECT tb_produto_venda.*, tb_produto.* 
                                 FROM tb_produto_venda 
                                 INNER JOIN tb_produto ON tb_produto_venda.pro_id = tb_produto.pro_id
                                 WHERE tb_produto_venda.ven_id = @IdVenda"; // SQL
                MySqlCommand cmd = new MySqlCommand(query, conn); // Vincula comando SQL com conexão
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                MySqlDataReader dados = cmd.ExecuteReader(); // Aguarda o resultado em um data Reader

                if (dados.HasRows) // Verifica se o data reader esta preenchido
                {
                    while (dados.Read()) // Enquato tiver linha pra ser lida
                    {
                        ProdutoVenda produtoVenda = new ProdutoVenda();
                        produtoVenda.Id = (int)dados["pv_id"];
                        produtoVenda.Quantidade = (int)dados["pv_quantidade"];
                        produtoVenda.Valor = (double)dados["pv_valor"];
                        produtoVenda.Produto.Id = (int)dados["pro_id"];
                        produtoVenda.Produto.Nome = dados["pro_nome"].ToString();
                        produtoVenda.Produto.Descricao = dados["pro_descricao"].ToString();
                        produtoVenda.Produto.QuantidadeEstoque = (int)dados["pro_quantidade_estoque"];
                        produtoVenda.Produto.PrecoVenda = (double)dados["pro_preco_venda"];
                        produtoVenda.Produto.Fornecedor.Id = (int)dados["pes_id"];

                        produtosVenda.Add(produtoVenda); // Adiciona o Objeto a lista
                    }
                }

                return produtosVenda; // Retorna a lista preenchida com todos os objetos

            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    Connection.CloseConnection(); // fecha a conexão com o banco.
            }
        }

        /// <summary>
        /// Deleta um registro
        /// </summary>
        /// <returns>true ou false</returns>
        internal bool Deletar(int idProdutoVenda)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM tb_produto_venda WHERE pv_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idProdutoVenda);
                int lines = cmd.ExecuteNonQuery(); // Executa a query e "line" guarda o número de linhas afetadas

                if (lines > 0) // Se deletou o número de "lines" vai ser maior que zero
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    Connection.CloseConnection();
            }
        }
    }
}