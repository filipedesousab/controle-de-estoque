using MySql.Data.MySqlClient;
using ControleEstoque.Controllers;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;

namespace ControleEstoque.DAL
{
    public class ProdutoDal
    {
        /// <summary>
        /// Adiciona um Produto
        /// </summary>
        /// <returns>true or false</returns>
        internal bool Adicionar(Produto produto)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO tb_produto
                                   (
                                       pro_nome,
                                       pro_descricao,
                                       pro_quantidade_estoque,
                                       pro_preco_venda,
                                       pes_id
                                   )
                                   VALUES
                                   (
                                       @Nome,
                                       @Descricao,
                                       @QuantidadeEstoque,
                                       @PrecoVenda,
                                       @IdFornecedor
                                   )";

                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@QuantidadeEstoque", produto.QuantidadeEstoque);
                cmd.Parameters.AddWithValue("@PrecoVenda", produto.PrecoVenda);
                cmd.Parameters.AddWithValue("@IdFornecedor", produto.Fornecedor.Id);

                int lines = cmd.ExecuteNonQuery(); // Executa a query e "line" guarda o número de linhas afetadas

                if (lines > 0) // Se salvou o número de "lines" vai ser maior que zero
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

        /// <summary>
        /// Traz uma lista de todos os registros ativos
        /// </summary>
        /// <returns>Retorna uma lista</returns>
        internal List<Produto> ObterRegistros()
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                List<Produto> produtos = new List<Produto>(); // Cria uma lista de objetos
                string query = @"SELECT tb_produto.*, tb_pessoa FROM tb_produto INNER JOIN tb_pessoa ON tb_produto.pes_id = tb_pessoa.pes_id"; // SQL
                MySqlCommand cmd = new MySqlCommand(query, conn); // Vincula comando SQL com conexão
                MySqlDataReader dados = cmd.ExecuteReader(); // Aguarda o resultado em um data Reader

                if (dados.HasRows) // Verifica se o data reader esta preenchido
                {
                    while (dados.Read()) // Enquato tiver linha pra ser lida
                    {
                        Produto produto = new Produto();
                        produto.Id = (int)dados["pro_id"];
                        produto.Nome = dados["pro_nome"].ToString();
                        produto.Descricao = dados["pro_descricao"].ToString();
                        produto.QuantidadeEstoque = (int)dados["pro_quantidade_estoque"];
                        produto.PrecoVenda = (double)dados["pro_preco_venda"];
                        produto.Fornecedor.Id = (int)dados["pes_id"];
                        produto.Fornecedor.Nome = dados["pes_nome"].ToString();

                        produtos.Add(produto); // Adiciona o Objeto a lista
                    }
                }

                return produtos; // Retorna a lista preenchida com todos os objetos

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
        /// Traz um objeto pelo seu ID passado como parametro
        /// </summary>
        /// <returns>Retorna um objeto de acordo com seu id</returns>
        internal Produto ObterRegistro(int idProduto)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM tb_produto WHERE pro_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idProduto);
                MySqlDataReader dados = cmd.ExecuteReader(); // Guarda o resultado em um Data Reader

                Produto produto = new Produto();

                if (dados.Read())
                {
                    produto.Id = (int)dados["pro_id"];
                    produto.Nome = dados["pro_nome"].ToString();
                    produto.Descricao = dados["pro_descricao"].ToString();
                    produto.QuantidadeEstoque = (int)dados["pro_quantidade_estoque"];
                    produto.PrecoVenda = (double)dados["pro_preco_venda"];
                    produto.Fornecedor.Id = (int)dados["pes_id"];
                    produto.Fornecedor.Nome = dados["pes_nome"].ToString();
                }

                return produto; // Retorna objeto

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
        /// Atualiza um registro
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>True ou False</returns>
        internal bool Alterar(Produto produto)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE tb_produto
                                   SET 
                                       pro_nome = @Nome,
                                       pro_descricao = @Descricao,
                                       pro_quantidade_estoque = @QuantidadeEstoque,
                                       pro_preco_venda = @PrecoVenda,
                                       pes_id = @IdFornecedor
                                   WHERE pro_id = @IdProduto"; // SQL

                cmd.Parameters.AddWithValue("@Nome", produto.Nome); // Adicionando parametros @ da consulta SQL
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@QuantidadeEstoque", produto.QuantidadeEstoque);
                cmd.Parameters.AddWithValue("@PrecoVenda", produto.PrecoVenda);
                cmd.Parameters.AddWithValue("@IdFornecedor", produto.Fornecedor.Id);
                cmd.Parameters.AddWithValue("@IdProduto", produto.Id);

                int lines = cmd.ExecuteNonQuery(); // Executa a query e "line" guarda o número de linhas afetadas

                if (lines > 0) // Se salvou o número de "lines" vai ser maior que zero
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

        /// <summary>
        /// Deleta um registro
        /// </summary>
        /// <returns>true ou false</returns>
        internal bool Deletar(int idProduto)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM tb_produto WHERE pro_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idProduto);
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