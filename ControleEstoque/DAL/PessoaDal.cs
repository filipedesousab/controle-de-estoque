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
    public class PessoaDal
    {
        /// <summary>
        /// Adiciona uma Pessoa
        /// </summary>
        /// <returns>true or false</returns>
        internal bool Adicionar(Pessoa pessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO tb_pessoa");
                sql.AppendLine("(");
                sql.AppendLine("    pes_nome,");
                sql.AppendLine("    pes_data_nascimento,");
                sql.AppendLine("    pes_perfil_cliente,");
                sql.AppendLine("    pes_perfil_vendedor,");
                sql.AppendLine("    pes_perfil_fornecedor,");
                sql.AppendLine("    pes_documento,");
                sql.AppendLine("    pes_tipo_documento,");
                sql.AppendLine("    pes_email,");
                sql.AppendLine("    pes_ddd,");
                sql.AppendLine("    pes_telefone,");
                sql.AppendLine("    pes_endereco,");
                sql.AppendLine("    pes_bairro,");
                sql.AppendLine("    pes_cep,");
                sql.AppendLine("    mun_id,");
                sql.AppendLine("    pes_status");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine($"   '{pessoa.Nome}',");
                sql.AppendLine($"   '{pessoa.DataNascimento.ToString("yyyy-MM-dd")}',");
                sql.AppendLine($"   '{pessoa.PerfilCliente}',");
                sql.AppendLine($"   '{pessoa.PerfilVendedor}',");
                sql.AppendLine($"   '{pessoa.PerfilFornecedor}',");
                sql.AppendLine($"   '{pessoa.Documento}',");
                sql.AppendLine($"   '{pessoa.TipoDocumento}',");
                sql.AppendLine($"   '{pessoa.Email}',");
                sql.AppendLine($"   '{pessoa.Ddd}',");
                sql.AppendLine($"   '{pessoa.Telefone}',");
                sql.AppendLine($"   '{pessoa.Endereco}',");
                sql.AppendLine($"   '{pessoa.Bairro}',");
                sql.AppendLine($"   '{pessoa.Cep}',");
                sql.AppendLine($"   '{pessoa.Municipio.Id}',");
                sql.AppendLine($"   '{pessoa.Status}'");
                sql.AppendLine(")");

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql.ToString();

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
    }
}