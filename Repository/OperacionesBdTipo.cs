using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdTipo
    {
        public List<Tipo> obtenerTipos()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM tipo_aparato";

            List<Tipo> tipos = new List<Tipo>();

            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    int contador = 0;

                    while (reader.Read())
                    {
                        Tipo tipo = new Tipo();
                        tipo.Id = int.Parse(reader.GetString(0));
                        tipo.Nombre = reader.GetString(1);
                        tipos.Add(tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return tipos;
        }

        public int insertarTipo(Tipo tipo)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO tipo_aparato (nombre_tipo) VALUES ('{tipo.Nombre}')";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long tipoRegistrado = dbcmd.LastInsertedId;

            return Convert.ToInt32(tipoRegistrado);
        }
    }
}
