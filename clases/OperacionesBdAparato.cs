using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_de_Taller.clases
{
    internal class OperacionesBdAparato
    {
        public int insertarAparato(Aparato aparato)
        {
            clases.Conexion conexionBd = new clases.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            String query = $"INSERT INTO aparato (tipo, marca, modelo, control, cable, entregado, linkCloudinary, idCliente, fechaIngreso) VALUES ('{aparato.Tipo}', '{aparato.Marca}', '{aparato.Modelo}', '{aparato.Control}', '{aparato.Cable}', '{aparato.Entregado}', '{aparato.LinkCloudinary}', '{aparato.IdCliente}', '{aparato.FechaIngreso}')";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long aparatoRegistrado = dbcmd.LastInsertedId;

            return Convert.ToInt32(aparatoRegistrado);
        }
    }
}
