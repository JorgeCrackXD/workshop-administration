using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdAparato
    {
        OperacionesBdCliente operacionesBdCliente = new OperacionesBdCliente();
        public int insertarAparato(Aparato aparato)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO aparato (tipo, marca, modelo, control, cable, entregado, linkCloudinary, idCliente, fechaIngreso) VALUES ('{aparato.Tipo}', '{aparato.Marca}', '{aparato.Modelo}', '{aparato.Control}', '{aparato.Cable}', '{aparato.Entregado}', '{aparato.LinkCloudinary}', '{aparato.IdCliente}', '{aparato.FechaIngreso}')";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
            long aparatoRegistrado = dbcmd.LastInsertedId;

            return Convert.ToInt32(aparatoRegistrado);
        }

        public Aparato obtenerAparato(int aparatoId)
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM aparato where id={aparatoId}";
            Aparato aparato = new Aparato();

            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        aparato.Id = int.Parse(reader.GetString(0));
                        aparato.Tipo = reader.GetString(1);
                        aparato.Marca = reader.GetString(2);
                        aparato.Modelo = reader.GetString(3);
                        aparato.Control = int.Parse(reader.GetString(4));
                        aparato.Cable = int.Parse(reader.GetString(5));
                        aparato.FechaIngreso = reader.GetString(6);
                        aparato.FechaDiagnostico = (reader.IsDBNull(7)) ? "" : reader.GetString(7);
                        aparato.FechaEntrega = (reader.IsDBNull(8)) ? "" : reader.GetString(8);
                        aparato.Entregado= int.Parse(reader.GetString(9));
                        aparato.LinkCloudinary = reader.GetString(10);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return aparato;
        }

        public List<AparatoTabla> obtenerAparatos()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM aparato";

            List<AparatoTabla> aparatos = new List<AparatoTabla>();

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
                        Cliente cliente = operacionesBdCliente.obtenerUnClientePorId(int.Parse(reader.GetString(11)));
                        AparatoTabla aparato = new AparatoTabla();
                        aparato.Id = int.Parse(reader.GetString(0));
                        aparato.Tipo = reader.GetString(1);
                        aparato.Marca = reader.GetString(2);
                        aparato.Modelo = reader.GetString(3);
                        aparato.FechaIngreso = reader.GetString(6);
                        aparato.Cliente = cliente.Nombre;
                        aparatos.Add(aparato);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return aparatos;
        }

        public List<AparatoTabla> consultaCustomLista(String query)
        {
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();
            MySqlDataReader reader = null;

            List<AparatoTabla> aparatos = new List<AparatoTabla>();
            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cliente cliente = operacionesBdCliente.obtenerUnClientePorId(int.Parse(reader.GetString(11)));
                        AparatoTabla aparato = new AparatoTabla();
                        aparato.Id = int.Parse(reader.GetString(0));
                        aparato.Tipo = reader.GetString(1);
                        aparato.Marca = reader.GetString(2);
                        aparato.Modelo = reader.GetString(3);
                        aparato.FechaIngreso = reader.GetString(6);
                        aparato.Cliente = cliente.Nombre;
                        aparatos.Add(aparato);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return aparatos;
        }
    }
}
