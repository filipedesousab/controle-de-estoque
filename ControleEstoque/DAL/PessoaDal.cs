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
                sql.AppendLine("    @Nome,");
                sql.AppendLine("    @DataNascimento,");
                sql.AppendLine("    @PerfilCliente,");
                sql.AppendLine("    @PerfilVendedor,");
                sql.AppendLine("    @PerfilFornecedor,");
                sql.AppendLine("    @Documento,");
                sql.AppendLine("    @TipoDocumento,");
                sql.AppendLine("    @Email,");
                sql.AppendLine("    @Ddd,");
                sql.AppendLine("    @Telefone,");
                sql.AppendLine("    @Endereco,");
                sql.AppendLine("    @Bairro,");
                sql.AppendLine("    @Cep,");
                sql.AppendLine("    @IdMunicipio,");
                sql.AppendLine("    @Status");
                sql.AppendLine(")");

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql.ToString();

                cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                cmd.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PerfilCliente", pessoa.PerfilCliente);
                cmd.Parameters.AddWithValue("@PerfilVendedor", pessoa.PerfilVendedor);
                cmd.Parameters.AddWithValue("@PerfilFornecedor", pessoa.PerfilFornecedor);
                cmd.Parameters.AddWithValue("@Documento", pessoa.Documento);
                cmd.Parameters.AddWithValue("@TipoDocumento", pessoa.TipoDocumento);
                cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                cmd.Parameters.AddWithValue("@Ddd", pessoa.Ddd);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", pessoa.Endereco);
                cmd.Parameters.AddWithValue("@Bairro", pessoa.Bairro);
                cmd.Parameters.AddWithValue("@Cep", pessoa.Cep);
                cmd.Parameters.AddWithValue("@IdMunicipio", pessoa.Municipio.Id);
                cmd.Parameters.AddWithValue("@Status", pessoa.Status);

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
        internal List<Pessoa> ObterRegistrosAtivos()
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                List<Pessoa> pessoas = new List<Pessoa>(); // Cria uma lista de objetos
                string query = @"SELECT * FROM tb_pessoa WHERE pes_status = 'A'"; // SQL
                MySqlCommand cmd = new MySqlCommand(query, conn); // Vincula comando SQL com conexão
                MySqlDataReader dados = cmd.ExecuteReader(); // Aguarda o resultado em um data Reader

                if (dados.HasRows) // Verifica se o data reader esta preenchido
                {
                    while (dados.Read()) // Enquato tiver linha pra ser lida
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = (int)dados["pes_id"];
                        pessoa.Nome = dados["pes_nome"].ToString();
                        pessoa.DataNascimento = Convert.ToDateTime(dados["pes_nome"]);
                        pessoa.PerfilCliente = Convert.ToChar(dados["pes_perfil_cliente"]);
                        pessoa.PerfilVendedor = Convert.ToChar(dados["pes_perfil_vendedor"]);
                        pessoa.PerfilFornecedor = Convert.ToChar(dados["pes_perfil_fornecedor"]);
                        pessoa.Documento = dados["pes_documento"].ToString();
                        pessoa.TipoDocumento = dados["pes_tipo_documento"].ToString();
                        pessoa.Email = dados["pes_email"].ToString();
                        pessoa.Ddd = dados["pes_ddd"].ToString();
                        pessoa.Telefone = dados["pes_telefone"].ToString();
                        pessoa.Endereco = dados["pes_endereco"].ToString();
                        pessoa.Bairro = dados["pes_bairro"].ToString();
                        pessoa.Cep = dados["pes_cep"].ToString();
                        pessoa.Municipio.Id = (int)dados["mun_id"];
                        pessoa.DataCadastro = Convert.ToDateTime(dados["pes_data_cad"]);
                        pessoa.DataAtualizacao = Convert.ToDateTime(dados["pes_data_atualizacao"]);
                        pessoa.Status = Convert.ToChar(dados["pes_status"]);

                        pessoas.Add(pessoa); // Adiciona o Objeto a lista
                    }
                }

                return pessoas; // Retorna a lista preenchida com todos os objetos

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
        /// Traz uma lista filtrada de pessoas
        /// </summary>
        /// <returns>Uma lista</returns>
        internal List<Pessoa> ObterRegistrosComFiltro(Pessoa pessoaFiltro)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                List<Pessoa> pessoas = new List<Pessoa>(); // Cria uma lista de objetos
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(@"SELECT * FROM tb_pessoa WHERE 1 = 1 "); // SQL

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Nome))
                    sql.AppendLine(" AND pes_nome = @Nome");

                if (pessoaFiltro.DataNascimento != DateTime.MinValue)
                    sql.AppendLine(" AND pes_data_nascimento = @DataNascimento");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.PerfilCliente.ToString()))
                    sql.AppendLine(" AND pes_perfil_cliente = @PerfilCliente");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.PerfilVendedor.ToString()))
                    sql.AppendLine(" AND pes_perfil_vendedor = @PerfilVendedor");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.PerfilFornecedor.ToString()))
                    sql.AppendLine(" AND pes_perfil_fornecedor = @PerfilFornecedor");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Documento))
                    sql.AppendLine(" AND pes_documento = @Documento");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.TipoDocumento))
                    sql.AppendLine(" AND pes_tipo_documento = @TipoDocumento");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Email))
                    sql.AppendLine(" AND pes_email = @Email");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Ddd))
                    sql.AppendLine(" AND pes_ddd = @Ddd");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Telefone))
                    sql.AppendLine(" AND pes_telefone = @Telefone");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Endereco))
                    sql.AppendLine(" AND pes_endereco = @Endereco");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Bairro))
                    sql.AppendLine(" AND pes_bairro = @Bairro");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Cep))
                    sql.AppendLine(" AND pes_cep = @Cep");

                if (pessoaFiltro.Municipio.Id != 0)
                    sql.AppendLine(" AND mun_id = @IdMunicipio");

                if (!string.IsNullOrWhiteSpace(pessoaFiltro.Status.ToString()))
                    sql.AppendLine(" AND pes_status = @Status");

                MySqlCommand cmd = new MySqlCommand(sql.ToString(), conn); // Vincula comando SQL com conexão

                cmd.Parameters.AddWithValue("@Nome", pessoaFiltro.Nome);
                cmd.Parameters.AddWithValue("@DataNascimento", pessoaFiltro.DataNascimento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PerfilCliente", pessoaFiltro.PerfilCliente);
                cmd.Parameters.AddWithValue("@PerfilVendedor", pessoaFiltro.PerfilVendedor);
                cmd.Parameters.AddWithValue("@PerfilFornecedor", pessoaFiltro.PerfilFornecedor);
                cmd.Parameters.AddWithValue("@Documento", pessoaFiltro.Documento);
                cmd.Parameters.AddWithValue("@TipoDocumento", pessoaFiltro.TipoDocumento);
                cmd.Parameters.AddWithValue("@Email", pessoaFiltro.Email);
                cmd.Parameters.AddWithValue("@Ddd", pessoaFiltro.Ddd);
                cmd.Parameters.AddWithValue("@Telefone", pessoaFiltro.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", pessoaFiltro.Endereco);
                cmd.Parameters.AddWithValue("@Bairro", pessoaFiltro.Bairro);
                cmd.Parameters.AddWithValue("@Cep", pessoaFiltro.Cep);
                cmd.Parameters.AddWithValue("@IdMunicipio", pessoaFiltro.Municipio.Id);
                cmd.Parameters.AddWithValue("@Status", pessoaFiltro.Status);

                MySqlDataReader dados = cmd.ExecuteReader(); // Aguarda o resultado em um data Reader

                if (dados.HasRows) // Verifica se o data reader esta preenchido
                {
                    while (dados.Read()) // Enquato tiver linha pra ser lida
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = (int)dados["pes_id"];
                        pessoa.Nome = dados["pes_nome"].ToString();
                        pessoa.DataNascimento = Convert.ToDateTime(dados["pes_nome"]);
                        pessoa.PerfilCliente = Convert.ToChar(dados["pes_perfil_cliente"]);
                        pessoa.PerfilVendedor = Convert.ToChar(dados["pes_perfil_vendedor"]);
                        pessoa.PerfilFornecedor = Convert.ToChar(dados["pes_perfil_fornecedor"]);
                        pessoa.Documento = dados["pes_documento"].ToString();
                        pessoa.TipoDocumento = dados["pes_tipo_documento"].ToString();
                        pessoa.Email = dados["pes_email"].ToString();
                        pessoa.Ddd = dados["pes_ddd"].ToString();
                        pessoa.Telefone = dados["pes_telefone"].ToString();
                        pessoa.Endereco = dados["pes_endereco"].ToString();
                        pessoa.Bairro = dados["pes_bairro"].ToString();
                        pessoa.Cep = dados["pes_cep"].ToString();
                        pessoa.Municipio.Id = (int)dados["mun_id"];
                        pessoa.DataCadastro = Convert.ToDateTime(dados["pes_data_cad"]);
                        pessoa.DataAtualizacao = Convert.ToDateTime(dados["pes_data_atualizacao"]);
                        pessoa.Status = Convert.ToChar(dados["pes_status"]);

                        pessoas.Add(pessoa); // Adiciona o Objeto a lista
                    }
                }

                return pessoas; // Retorna a lista preenchida com todos os objetos

            }
            catch
            {
                throw;
            }
            finally
            {
                Connection.CloseConnection(); // fecha a conexão com o banco.
            }
        }

        /// <summary>
        /// Traz um objeto pelo seu ID passado como parametro
        /// </summary>
        /// <returns>Retorna um objeto de acordo com seu id</returns>
        internal Pessoa ObterRegistro(int idPessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM tb_pessoa WHERE pes_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idPessoa);
                MySqlDataReader dados = cmd.ExecuteReader(); // Aguarda o resltado em um data Reader

                Pessoa pessoa = new Pessoa();

                if (dados.Read())
                {
                    pessoa.Id = (int)dados["pes_id"];
                    pessoa.Nome = dados["pes_nome"].ToString();
                    pessoa.DataNascimento = Convert.ToDateTime(dados["pes_nome"]);
                    pessoa.PerfilCliente = Convert.ToChar(dados["pes_perfil_cliente"]);
                    pessoa.PerfilVendedor = Convert.ToChar(dados["pes_perfil_vendedor"]);
                    pessoa.PerfilFornecedor = Convert.ToChar(dados["pes_perfil_fornecedor"]);
                    pessoa.Documento = dados["pes_documento"].ToString();
                    pessoa.TipoDocumento = dados["pes_tipo_documento"].ToString();
                    pessoa.Email = dados["pes_email"].ToString();
                    pessoa.Ddd = dados["pes_ddd"].ToString();
                    pessoa.Telefone = dados["pes_telefone"].ToString();
                    pessoa.Endereco = dados["pes_endereco"].ToString();
                    pessoa.Bairro = dados["pes_bairro"].ToString();
                    pessoa.Cep = dados["pes_cep"].ToString();
                    pessoa.Municipio.Id = (int)dados["mun_id"];
                    pessoa.DataCadastro = Convert.ToDateTime(dados["pes_data_cad"]);
                    pessoa.DataAtualizacao = Convert.ToDateTime(dados["pes_data_atualizacao"]);
                    pessoa.Status = Convert.ToChar(dados["pes_status"]);
                }

                return pessoa; // Retorna objeto

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
        /// Checa se já existe uma pessoa com o mesmo e-mail cadastrada
        /// </summary>
        /// <returns></returns>
        internal bool VerificarSeExisteEmail(Pessoa pessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Usa a classe Connection para abrir conexão
            try
            {
                string query = @"SELECT COUNT(*) FROM tb_pessoa WHERE pes_email = @Email"; // SQL
                MySqlCommand cmd = new MySqlCommand(query, conn); // Vincular a query SQL com a conexão
                cmd.Parameters.AddWithValue("@Email", pessoa.Email.Trim());
                long lines = (long)cmd.ExecuteScalar(); // Executa a query e "line" guarda o número de linhas afetadas

                if (lines > 0) //Se o número de lines for maior que 0 já existe registros
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
        /// Verifica se já outra pessoa com o mesmo e-mail
        /// </summary>
        /// <param name="pessoa">Objeto Pessoa</param>
        /// <returns>True ou False</returns>
        internal bool VerificarSeExisteEmailOutraPessoa(Pessoa pessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados

            try
            {
                string query = @"SELECT COUNT(*) FROM tb_pessoa WHERE pes_id <> @IdPessoa AND pes_email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPessoa", pessoa.Id);
                cmd.Parameters.AddWithValue("@Email", pessoa.Email.Trim());
                int lines = (int)cmd.ExecuteScalar();

                if (lines > 0) // Se o número de lines for maior que 0 já existe registro em outro usuário
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
        /// Atualiza um registro
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns>True ou False</returns>
        internal bool Alterar(Pessoa pessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE tb_pessoa
                                   SET 
                                       pes_nome = @Nome,
                                       pes_data_nascimento = @DataNascimento,
                                       pes_perfil_cliente = @PerfilCliente,
                                       pes_perfil_vendedor = @PerfilVendedor,
                                       pes_perfil_fornecedor = @PerfilFornecedor,
                                       pes_documento = @Documento,
                                       pes_tipo_documento = @TipoDocumento,
                                       pes_email = @Email,
                                       pes_ddd = @Ddd,
                                       pes_telefone = @Telefone,
                                       pes_endereco = @Endereco,
                                       pes_bairro = @Bairro,
                                       pes_cep = @Cep,
                                       mun_id = @MunicipioId,
                                       pes_status = @Status
                                   WHERE pes_id = @IdPessoa"; // SQL

                cmd.Parameters.AddWithValue("@Nome", pessoa.Nome); // Adicionando parametros @ da consulta SQL
                cmd.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PerfilCliente", pessoa.PerfilCliente);
                cmd.Parameters.AddWithValue("@PerfilVendedor", pessoa.PerfilVendedor);
                cmd.Parameters.AddWithValue("@PerfilFornecedor", pessoa.PerfilFornecedor);
                cmd.Parameters.AddWithValue("@Documento", pessoa.Documento);
                cmd.Parameters.AddWithValue("@TipoDocumento", pessoa.TipoDocumento);
                cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                cmd.Parameters.AddWithValue("@Ddd", pessoa.Ddd);
                cmd.Parameters.AddWithValue("@Telefone", pessoa.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", pessoa.Endereco);
                cmd.Parameters.AddWithValue("@Bairro", pessoa.Bairro);
                cmd.Parameters.AddWithValue("@Cep", pessoa.Cep);
                cmd.Parameters.AddWithValue("@MunicipioId", pessoa.Municipio.Id);
                cmd.Parameters.AddWithValue("@Status", pessoa.Status);
                cmd.Parameters.AddWithValue("@IdPessoa", pessoa.Id);

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
        /// Altera o status do registro para 'I'
        /// </summary>
        /// <param name="idPessoa"></param>
        /// <returns>True ou False</returns>
        internal bool Desativar(int idPessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE tb_pessoa SET pes_status = 'I' WHERE pes_id = @IdPessoa AND pes_status NOT LIKE 'I'"; // SQL

                cmd.Parameters.AddWithValue("@IdPessoa", idPessoa); // Adicionando parametros @ da consulta SQL

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
        internal bool Deletar(int idPessoa)
        {
            MySqlConnection conn = Connection.GetConnection(); // Abre a conexão com o banco de dados
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM tb_pessoa WHERE pes_id = @Id"; // SQL
                cmd.Parameters.AddWithValue("@Id", idPessoa);
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