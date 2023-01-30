using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdDiagnostico
    {
        public int insertarDiagnostico(Diagnostico diagnostico)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO diagnostico (diagnostico, costo, fechaDiagnostico, idAparato, idCliente) VALUES ('{diagnostico.DiagnosticoAparato}', {diagnostico.Costo}, CURRENT_DATE(), {diagnostico.IdAparato}, {diagnostico.IdCliente})";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long diagnosticoRegistrado = dbcmd.LastInsertedId;

            return Convert.ToInt32(diagnosticoRegistrado);
        }

        public List<Diagnostico> obtenerDiagnosticos()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;
            string query = "SELECT * FROM diagnostico";

            List<Diagnostico> diagnosticos = new List<Diagnostico>();
            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Diagnostico diagnostico = new Diagnostico();
                        diagnostico.Id = int.Parse(reader.GetString(0));
                        diagnostico.DiagnosticoAparato = reader.GetString(1);
                        diagnostico.Costo = int.Parse(reader.GetString(2));
                        diagnostico.FechaDiagnostico = DateTime.Parse(reader.GetString(3));
                        diagnostico.IdAparato = int.Parse(reader.GetString(4));
                        diagnostico.IdCliente = int.Parse(reader.GetString(5));
                        diagnosticos.Add(diagnostico);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return diagnosticos;
        }

        public List<DiagnosticoTabla> obtenerDiagnosticosTabla()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;
            string query = "SELECT diagnostico.id, diagnostico.diagnostico, diagnostico.costo, diagnostico.fechaDiagnostico, aparato.tipo, cliente.nombre FROM diagnostico INNER JOIN cliente ON cliente.id = diagnostico.idCliente INNER JOIN aparato ON aparato.id = diagnostico.idAparato;";

            List<DiagnosticoTabla> diagnosticos = new List<DiagnosticoTabla>();
            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DiagnosticoTabla diagnostico = new DiagnosticoTabla();
                        diagnostico.Id = int.Parse(reader.GetString(0));
                        diagnostico.DiagnosticoAparato = reader.GetString(1);
                        diagnostico.Costo = int.Parse(reader.GetString(2));
                        diagnostico.FechaDiagnostico = DateTime.Parse(reader.GetString(3));
                        diagnostico.TipoAparato = reader.GetString(4);
                        diagnostico.NombreCliente = reader.GetString(5);
                        diagnosticos.Add(diagnostico);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return diagnosticos;
        }
    }
}
