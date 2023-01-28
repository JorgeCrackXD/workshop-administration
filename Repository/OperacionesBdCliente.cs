using Administracion_de_Taller.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Repository
{
    internal class OperacionesBdCliente
    {
        public Cliente obtenerUnClientePorId(int id)
        {
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM cliente WHERE id = {id}";

            Cliente cliente = new Cliente();
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
                        cliente.Id = int.Parse(reader.GetString(0));
                        cliente.Nombre = reader.GetString(1);
                        cliente.Telefono = reader.GetString(2);
                        cliente.Direccion = reader.GetString(3);
                        cliente.FechaRegistro = DateTime.Parse(reader.GetString(4));
                        cliente.AparatosEnTaller = int.Parse(reader.GetString(5));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return cliente;
        }

        public Cliente obtenerClientePorNombre(String nombre)
        {
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM cliente WHERE nombre = '{nombre}'";

            Cliente cliente = new Cliente();
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
                        cliente.Id = int.Parse(reader.GetString(0));
                        cliente.Nombre = reader.GetString(1);
                        cliente.Telefono = reader.GetString(2);
                        cliente.Direccion = reader.GetString(3);
                        cliente.FechaRegistro = DateTime.Parse(reader.GetString(4));
                        cliente.AparatosEnTaller = int.Parse(reader.GetString(5));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return cliente;
        }

        public List<Cliente> obtenerClientes()
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;
            string query = "SELECT * FROM cliente";

            List<Cliente> clientes = new List<Cliente>();
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
                        Cliente cliente = new Cliente();
                        cliente.Id = int.Parse(reader.GetString(0));
                        cliente.Nombre = reader.GetString(1);
                        cliente.Telefono = reader.GetString(2);
                        cliente.Direccion = reader.GetString(3);
                        cliente.FechaRegistro = DateTime.Parse(reader.GetString(4));
                        cliente.AparatosEnTaller = int.Parse(reader.GetString(5));
                        clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return clientes;
        }

        public List<Cliente> obtenerClientesPorNombre(string nombre)
        {
            //Establecer conexion a la BD
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            string query = $"SELECT * FROM cliente WHERE nombre LIKE '%{nombre}%'";

            List<Cliente> clientes = new List<Cliente>();

            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    int contador = 0;

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.Id = int.Parse(reader.GetString(0));
                        cliente.Nombre = reader.GetString(1);
                        cliente.Telefono = reader.GetString(2);
                        cliente.Direccion = reader.GetString(3);
                        cliente.FechaRegistro = DateTime.Parse(reader.GetString(4));
                        cliente.AparatosEnTaller = int.Parse(reader.GetString(5));
                        clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return clientes;
        }

        public int insertarCliente(Cliente cliente)
        {
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"INSERT INTO cliente (nombre, telefono, direccion, fechaRegistro, aparatosEnTaller) VALUES ('{cliente.Nombre}', '{cliente.Telefono}', '{cliente.Direccion}', CURRENT_DATE(), {cliente.AparatosEnTaller})";

            MySqlCommand dbcmd = conexion.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();

            long clienteRegistradoId = dbcmd.LastInsertedId;

            return Convert.ToInt32(clienteRegistradoId);
        }

        public bool actualizarCliente(Cliente cliente)
        {
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"UPDATE cliente SET aparatosEnTaller={cliente.AparatosEnTaller} WHERE id = {cliente.Id}";

            try
            {
                MySqlCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = query;
                dbcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        public bool actualizarDatosCliente(Cliente cliente)
        {
            Models.Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            string query = $"UPDATE cliente SET nombre='{cliente.Nombre}', telefono='{cliente.Telefono}', direccion='{cliente.Direccion}' WHERE id = {cliente.Id}";

            try
            {
                MySqlCommand dbcmd = conexion.CreateCommand();
                dbcmd.CommandText = query;
                dbcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }
    }
}
