using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Administracion_de_Taller.Models
{
    internal class Conexion
    {
        MySqlConnection conexion = new MySqlConnection();

        static string server = "localhost";
        static string database = "taller";
        static string user = "root";
        static string password = "centauro@22447";
        static string port = "3306";

        string connectionString = "server=" + server + ";port=" + port + ";user id=" + user + ";password=" + password + ";database=" + database + ";";

        public MySqlConnection establecerConexion()
        {
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos");
            }
            return conexion;
        }
    }
}
