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
    public class VendaDal
    {
        /// <summary>
        /// Traz um objeto pelo seu ID passado como parametro
        /// </summary>
        /// <returns>Retorna um objeto de acordo com seu id</returns>
        internal Venda ObterRegistro(int idVenda)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT tb_venda.*, tb_pessoa.* 
                                    FROM tb_venda 
                                    INNER JOIN tb_pessoa ON tb_venda.pes_id = tb_pessoa.pes_id 
                                    WHERE ven_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idVenda);
                MySqlDataReader dados = cmd.ExecuteReader(); // Guarda o resultado em um Data Reader

                Venda venda = new Venda();

                if (dados.Read())
                {
                    venda.Id = (int)dados["ven_id"];
                    venda.Data = Convert.ToDateTime(dados["ven_data"]);
                    venda.Desconto = (double)dados["ven_desconto"];
                    venda.Valor = (double)dados["ven_valor"];
                    venda.Vendedor.Id = (int)dados["pes_id"];
                    venda.Vendedor.Nome = dados["pes_nome"].ToString();
                }

                return venda; // Retorna objeto

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
        /// Deleta uma venda
        /// </summary>
        /// <returns>true ou false</returns>
        internal bool Deletar(int idVenda)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM tb_produto_venda WHERE ven_id = @Id;"; // SQL
                cmd.Parameters.AddWithValue("@Id", idVenda);
                int lines = cmd.ExecuteNonQuery(); // Executa a query e "line" guarda o número de linhas afetadas

                if (lines > 0) // Se deletou o número de "lines" vai ser maior que zero
                {
                    MySqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandText = @"DELETE FROM tb_venda WHERE ven_id = @Id;"; // SQL
                    cmd2.Parameters.AddWithValue("@Id", idVenda);
                    int linhasVenda = cmd2.ExecuteNonQuery(); // Executa a query e "linhasVenda" guarda o número de linhas afetadas
                    if (linhasVenda > 0)
                    {
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
    }
}