using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;
using CloudinaryDotNet.Actions;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Administracion_de_Taller
{
    public partial class FormAccionRapidaAparato : Form
    {
        private String path;

        private OperacionesBdCliente operacionesCliente = new OperacionesBdCliente();
        private OperacionesBdMarca operacionesMarca = new OperacionesBdMarca();
        private OperacionesBdAparato operacionesBdAparato = new OperacionesBdAparato();
        private OperacionesBdCliente operacionesBdCliente = new OperacionesBdCliente();
        private OperacionesBdImagenAparato operacionesBdImagenAparato = new OperacionesBdImagenAparato();
        private OperacionesBdTipo operacionesTipo = new OperacionesBdTipo();

        System.Windows.Forms.Form formPrincipal = System.Windows.Forms.Application.OpenForms["form1"];

        public FormAccionRapidaAparato()
        {
            InitializeComponent();
        }

        private void FormAccionRapidaAparato_Load(object sender, EventArgs e)
        {
            timer1.Start();

            List<Cliente> clientes = operacionesCliente.obtenerClientes();

            foreach (Cliente cliente in clientes)
            {
                comboBox1.Items.Add(cliente.Nombre);
            }

            llenarTiposyMarcas();
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
                comboBox5.Items.Add(tipo.Nombre);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (comboBox4.SelectedItem == null)
            {
                comboBox4.Items.Clear();
                List<Marca> marcas = operacionesMarca.obtenerMarcas();

                foreach (Marca marca in marcas)
                {
                    comboBox4.Items.Add(marca.Nombre);
                }
            }

            if (comboBox5.SelectedItem == null)
            {
                comboBox5.Items.Clear();
                List<Tipo> tipos = operacionesTipo.obtenerTipos();

                foreach (Tipo tipo in tipos)
                {
                    comboBox5.Items.Add(tipo.Nombre);
                }
            }

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
            backgroundWorker1.RunWorkerAsync();

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
                tipo = comboBox5.SelectedItem.ToString();
            });

            String marca = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                marca = comboBox4.SelectedItem.ToString();
            });

            String problema = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                problema = richTextBox1.Text;
            });

            //Se obtiene el cliente por nombre (Actualmente solo funciona para clientes independientes, en caso de que haya gerentes con nombre igual, crear caso de uso)
            String nombreCliente = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                nombreCliente = comboBox1.SelectedItem.ToString();
            });
            Cliente cliente = operacionesBdCliente.obtenerClientePorNombre(nombreCliente);

            // Se crea el objeto de la imagen si es que existe una imagen
            ImagenAparato imagenAparato = new ImagenAparato(null, null, 0);
            if (path != null)
            {
                imagenAparato = new ImagenAparato(imagenSubida.PublicId.ToString(), imagenSubida.Uri.ToString(), cliente.Id);
                imagenAparato.IdCloudinary = imagenSubida.PublicId.ToString();
                imagenAparato.LinkCloudinary = imagenSubida.Uri.ToString();
                imagenAparato.IdCliente = cliente.Id;

            }
            // Se crea el objeto del aparato
            Aparato aparato = new Aparato(tipo, marca, textBox4.Text, this.control(), this.cable(), problema, DateTime.Now.ToString("yyyy/MM/dd"), 0, imagenAparato.LinkCloudinary, cliente.Id);

            try
            {
                // Se hace el insert a la BD
                if (path != null)
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
                Cliente clienteBd = operacionesBdCliente.obtenerUnClientePorId(cliente.Id);
                clienteBd.AparatosEnTaller = clienteBd.AparatosEnTaller += 1;

                operacionesBdCliente.actualizarCliente(clienteBd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int cable()
        {
            String texto = "";
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

        private int control()
        {
            String texto = "";
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Aparato registrado correctamente!.");
            openChildForm(new FormAparatos());
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            ((Form1)formPrincipal).panelForms.Controls.Add(childForm);
            ((Form1)formPrincipal).panelForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
