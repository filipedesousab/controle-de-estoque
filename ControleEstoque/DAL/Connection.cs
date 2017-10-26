using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



namespace ControleEstoque.DAL
{
    // Esta classe é responsável por criar uma conexão com o banco de dados de forma genérica
    public class Connection
    {
        //private static string connString = @"Server=bd-ce.mysql.uhserver.com; Port=3306; Database=bd_ce; Uid=ceapp; Pwd=gfstd2017*FG;";
        private static string connString = @"Server=localhost; Port=3306; Database=bd_ce; Uid=root; Pwd=;";
        private static MySqlConnection conn = null; // Objeto que vai receber a conexão

        /// <summary>
        /// Abre uma conexão com o banco de dados
        /// </summary>
        /// <returns>Connection</returns>
        public static MySqlConnection GetConnection()
        {
            try
            {
                conn = new MySqlConnection(connString); // Cria conexão usando a os parametros da variavel connString
                // abre a conexão e a devolve ao chamador do método
                conn.Open();
            }
            catch (SqlException exception)
            {
                conn = null;
                throw new Exception("A conexão com o banco de dados falhou." + exception.Message, exception.InnerException);
            }

            return conn;
        }

        /// <summary>
        /// Fecha a conexão com a base de dados quando não precisar mais
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                //if(conn.State == ConnectionState.Open)
                if (conn != null) // Se a conexão estiver estabelecida
                {
                    conn.Close(); // Fecha a conexão
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Não foi possível fechar a conexão com o banco de dados" + exception.Message, exception.InnerException);
            }
        }
    }
}