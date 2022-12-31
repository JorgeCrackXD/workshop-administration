using Administracion_de_Taller.clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_de_Taller
{
    public partial class FormClienteBusqueda : Form
    {

        System.Windows.Forms.Form formClientes = System.Windows.Forms.Application.OpenForms["FormClientes"];

        public FormClienteBusqueda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Owner.Show();  //Show the previous form
            Close();
        }

        private void FormClienteBusqueda_Load(object sender, EventArgs e)
        {
            int clienteId = Int32.Parse(((FormClientes)formClientes).labelIdCliente.Text);

            mostrarCliente(clienteId);
           
        }

        private void FormClienteBusqueda_Shown(object sender, EventArgs e)
        {
            int clienteId = Int32.Parse(((FormClientes)formClientes).labelIdCliente.Text);

            mostrarCliente(clienteId);
        }

        private void mostrarCliente(int clienteId)
        {
            //Establecer conexion a la BD
            clases.Conexion conexionBd = new clases.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            String query = $"SELECT * FROM cliente WHERE id = {clienteId}";

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
                        Cliente cliente = new Cliente();
                        cliente.Id = int.Parse(reader.GetString(0));

                        cliente.Nombre = reader.GetString(1);
                        nombresText.Text = reader.GetString(1);

                        cliente.Telefono = reader.GetString(2);
                        telefonoText.Text = reader.GetString(2);

                        cliente.Direccion = reader.GetString(3);
                        direccionText.Text = reader.GetString(3);

                        cliente.FechaRegistro = reader.GetString(4);
                        fechaRegistroText.Text = reader.GetString(4);

                        cliente.AparatosEnTaller = int.Parse(reader.GetString(5));
                        aparatosTallerText.Text = reader.GetString(5);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
