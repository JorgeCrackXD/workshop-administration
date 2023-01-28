using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

            string query = $"INSERT INTO diagnostico (diagnostico, costo, fechaDiagnostico, idAparato, idCliente) VALUES ('{diagnostico.DiagnosticoAparato}', {diagnostico.Costo}, '{diagnostico.FechaDiagnostico}', {diagnostico.IdAparato}, {diagnostico.IdCliente})";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long diagnosticoRegistrado = dbcmd.LastInsertedId;

            return Convert.ToInt32(diagnosticoRegistrado);
        }
    }
}
