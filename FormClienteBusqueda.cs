using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;
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

        private OperacionesBdCliente operacionesCliente = new OperacionesBdCliente();

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
            Conexion conexionBd = new Models.Conexion();
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
            if (Int32.Parse(aparatosTallerText.Text) > 0)
            {
                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mostrarInputs();
            
        }

        private void mostrarInputs()
        {
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox1.Text = nombresText.Text;
            nombresText.Visible = false;

            textBox2.Enabled = true;
            textBox2.Visible = true;
            textBox2.Text = telefonoText.Text;
            telefonoText.Visible = false;

            textBox3.Enabled = true;
            textBox3.Visible = true;
            textBox3.Text = direccionText.Text;
            direccionText.Visible = false;

            button5.Enabled= true;
            button5.Visible= true;
        }
        
        private void ocultarInputs()
        {
            textBox1.Enabled = false;
            textBox1.Visible = false;
            textBox1.Text = "";
            nombresText.Visible = true;

            textBox2.Enabled = false;
            textBox2.Visible = false;
            textBox2.Text = "";
            telefonoText.Visible = true;

            textBox3.Enabled = false;
            textBox3.Visible = false;
            textBox3.Text = "";
            direccionText.Visible = true;

            button5.Enabled = false;
            button5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int clienteId = Int32.Parse(((FormClientes)formClientes).labelIdCliente.Text);

            Cliente clienteRepositorio = operacionesCliente.obtenerUnClientePorId(clienteId);
            //Actualizando datos del cliente
            clienteRepositorio.Nombre = textBox1.Text;
            clienteRepositorio.Telefono= textBox2.Text;
            clienteRepositorio.Direccion = textBox3.Text;

            operacionesCliente.actualizarDatosCliente(clienteRepositorio);
            MessageBox.Show("Los datos han sido actualizados correctamente.");

            ocultarInputs();

            mostrarCliente(clienteId);

        }
    }
}
