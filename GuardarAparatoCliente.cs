﻿using Administracion_de_Taller.clases;
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

        private int idCliente;

        System.Windows.Forms.Form formClientes = System.Windows.Forms.Application.OpenForms["FormClientes"];

        private OperacionesBdImagenAparato operacionesBdImagenAparato = new OperacionesBdImagenAparato();

        private OperacionesBdAparato operacionesBdAparato = new OperacionesBdAparato();

        private OperacionesBdCliente operacionesBdCliente = new OperacionesBdCliente(); 

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


            String tipo = "";
            this.Invoke((MethodInvoker)delegate ()
            {
                tipo = comboBox1.SelectedItem.ToString();
            });

            // Se crea el objeto de la imagen
            ImagenAparato imagenAparato = new ImagenAparato(imagenSubida.PublicId.ToString(), imagenSubida.Uri.ToString(), idCliente);
            // Se crea el objeto del aparato
            Aparato aparato = new Aparato(tipo, textBox3.Text, textBox4.Text, this.control(), this.cable(), DateTime.Now.ToString("yyyy/MM/dd"), 0, imagenAparato.LinkCloudinary, idCliente);

            try
            {
                // Se hace el insert a la BD
                operacionesBdImagenAparato.insertarImagenAparato(imagenAparato);

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
