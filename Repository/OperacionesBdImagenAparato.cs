using Administracion_de_Taller.Models;
using CloudinaryDotNet;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdImagenAparato
    {
        public int insertarImagenAparato(ImagenAparato imagenAparato)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO imagen_aparato (idCloudinary, linkCloudinary, idCliente) VALUES ('{imagenAparato.IdCloudinary}', '{imagenAparato.LinkCloudinary}', '{imagenAparato.IdCliente}')";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long imagenAparatoRegistrada = dbcmd.LastInsertedId;

            return Convert.ToInt32(imagenAparatoRegistrada);

        }
    }
}
