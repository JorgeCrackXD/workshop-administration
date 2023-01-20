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
using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;

namespace Administracion_de_Taller
{
    public partial class GuardarAparatoCliente : Form
    {

        private String path;

        private int idCliente;

        System.Windows.Forms.Form formClientes = System.Windows.Forms.Application.OpenForms["FormClientes"];

        private OperacionesBdImagenAparato operacionesBdImagenAparato = new OperacionesBdImagenAparato();

        private OperacionesBdAparato operacionesBdAparato = new OperacionesBdAparato();

        private OperacionesBdCliente operacionesBdCliente = new OperacionesBdCliente(); 

        private OperacionesBdMarca operacionesMarca = new OperacionesBdMarca();

        private OperacionesBdTipo operacionesTipo = new OperacionesBdTipo();

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

            DialogResult dialogResult = MessageBox.Show("Desea registrar un aparato más??", "Aparato nuevo para cliente", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                GuardarAparatoCliente formGuardar = new GuardarAparatoCliente();
                formGuardar.ShowDialog();
            } else
            {
                FormClientes formClientes = new FormClientes();
                this.Close();
                formClientes.ShowDialog();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            CloudinaryImpl cloudinary = new CloudinaryImpl();
            ImageUploadResult imagenSubida = null;

            if (path != null)
            {
                imagenSubida = cloudinary.cloudinarySave(path);
            }

            String tipo = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                tipo = comboBox1.SelectedItem.ToString();
            });

            String marca = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                marca = comboBox1.SelectedItem.ToString();
            });
            // Se crea el objeto de la imagen si es que existe una imagen
            ImagenAparato imagenAparato = new ImagenAparato(null, null, 0);
            if( path != null)
            {
                imagenAparato = new ImagenAparato(imagenSubida.PublicId.ToString(), imagenSubida.Uri.ToString(), idCliente);
                imagenAparato.IdCloudinary = imagenSubida.PublicId.ToString();
                imagenAparato.LinkCloudinary = imagenSubida.Uri.ToString();
                imagenAparato.IdCliente = idCliente;

            }
            // Se crea el objeto del aparato
            Aparato aparato = new Aparato(tipo, marca, textBox4.Text, this.control(), this.cable(), DateTime.Now.ToString("yyyy/MM/dd"), 0, imagenAparato.LinkCloudinary, idCliente);

            try
            {
                // Se hace el insert a la BD
                if(path != null)
                {
                    operacionesBdImagenAparato.insertarImagenAparato(imagenAparato);
                }
                // Se hace el insert a la BD
                operacionesBdAparato.insertarAparato(aparato);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
            // En caso de que se haya guardado correctamente el aparato e imagen ( si es que hay ) se actualiza el usuario para añadirle 1 aparato.
            try
            {
                Cliente clienteBd = operacionesBdCliente.obtenerUnClientePorId(idCliente);
                clienteBd.AparatosEnTaller = clienteBd.AparatosEnTaller += 1;

                operacionesBdCliente.actualizarCliente(clienteBd);
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

            llenarTiposyMarcas();

            idCliente = Int32.Parse(((FormClientes)formClientes).labelIdCliente.Text);
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
            String marca = comboBox4.SelectedItem.ToString();
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

        private void llenarTiposyMarcas()
        {
            List<Marca> marcas = operacionesMarca.obtenerMarcas();

            foreach (Marca marca in marcas)
            {
                comboBox4.Items.Add(marca.Nombre);
            }

            List<Tipo> tipos = operacionesTipo.obtenerTipos();

            foreach (Tipo tipo in tipos)
            {
                comboBox1.Items.Add(tipo.Nombre);
            }
        }
    }
}
