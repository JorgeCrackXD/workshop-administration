using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdMarca
    {
        public List<Marca> obtenerMarcas()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM marca_aparato";

            List<Marca> marcas = new List<Marca>();

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
                        Marca marca = new Marca();
                        marca.Id = int.Parse(reader.GetString(0));
                        marca.Nombre = reader.GetString(1);
                        marcas.Add(marca);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return marcas;
        }

        public int insertarMarca(Marca marca)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO marca_aparato (nombre_marca) VALUES ('{marca.Nombre}')";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long marcaRegistrada = dbcmd.LastInsertedId;

            return Convert.ToInt32(marcaRegistrada);
        }
    }
}
