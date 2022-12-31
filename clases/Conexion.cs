﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Administracion_de_Taller.clases
{
    internal class Conexion
    {
        MySqlConnection conexion = new MySqlConnection();

        static string server = "localhost";
        static string database = "taller";
        static string user = "root";
        static string password = "2TadoiBr";
        static string port = "3306";

        string connectionString = "server=" + server + ";port=" + port + ";user id=" + user + ";password=" + password + ";database=" + database + ";";
        
        public MySqlConnection establecerConexion()
        {
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();

            } catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos");
            }
            return conexion;
        }
    }
}
