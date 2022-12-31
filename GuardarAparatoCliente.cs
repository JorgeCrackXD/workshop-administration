using Administracion_de_Taller.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace Administracion_de_Taller
{
    public partial class GuardarAparatoCliente : Form
    {

        private String path;

        private String nombreClienteString;

        System.Windows.Forms.Form formClientes = System.Windows.Forms.Application.OpenForms["FormClientes"];

        public GuardarAparatoCliente()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                path = file;
                pictureBox1.Image = Image.FromFile(file);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean correcto = validarInputs();

            if(correcto == true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("La imagen se ha subido con éxito");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            CloudinaryImpl cloudinary = new CloudinaryImpl();
            ImageUploadResult imagenSubida = cloudinary.cloudinarySave(path);

            String idCloudinary = imagenSubida.PublicId.ToString();
            String linkCloudinary = imagenSubida.Url.ToString();

            //Establecer conexion a la BD
            clases.Conexion conexionBd = new clases.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;

            String query = $"INSERT INTO imagen_aparato (idCloudinary, linkCloudinary, nombreCliente) VALUES ('{idCloudinary}', '{linkCloudinary}', '{nombreClienteString}')";

            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                reader = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            reader.Close();

            String nombreCliente = nombreClienteString;
            String tipo = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                 tipo = comboBox1.SelectedItem.ToString();
            });
            String marca = textBox3.Text;
            String modelo = textBox4.Text;
            int control = this.control();
            int cable = this.cable();
            int entregado = 0;
            String fechaActual = DateTime.Now.ToString("yyyy/MM/dd");


            String query2 = $"INSERT INTO aparato (tipo, marca, modelo, control, cable, entregado, linkCloudinary, nombreCliente, fechaIngreso) VALUES ('{tipo}', '{marca}', '{modelo}', '{control}', '{cable}', '{entregado}', '{linkCloudinary}', '{nombreCliente}', '{fechaActual}')";
            try
            {
                MySqlCommand comando = new MySqlCommand(query2, conexion);
                reader = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GuardarAparatoCliente_Load(object sender, EventArgs e)
        {
            String nombreCliente = ((FormClientes)formClientes).labelNombre.Text;

            textBox1.Text = nombreCliente;

            nombreClienteString = nombreCliente;
        }

        private int cable()
        {
            String texto ="";
            this.Invoke((MethodInvoker)delegate ()
            {
                texto = comboBox2.SelectedItem.ToString();
            });
            if (texto.Equals("Si"))
            {
                return 1;
            }
            return 0;
        }

        private int control()
        {
            String texto ="";
            this.Invoke((MethodInvoker)delegate ()
            {
                texto = comboBox3.SelectedItem.ToString();
            });
            if (texto.Equals("Si"))
            {
                return 1;
            }
            return 0;
        }

        private Boolean validarInputs()
        {
            String marca = textBox3.Text;
            String modelo = textBox4.Text;

            String test = comboBox1.SelectedText;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("FALTA ALGUN DATO POR LLENAR");
                return false;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("FALTA ALGUN DATO POR LLENAR");
                return false;
            }

            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("FALTA ALGUN DATO POR LLENAR");
                return false;
            }

            if (marca == "" || modelo == "")
            {
                MessageBox.Show("FALTA ALGUN DATO POR LLENAR");
                return false;
            }

            return true;
        }
    }
}
