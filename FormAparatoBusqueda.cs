using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_de_Taller
{
    public partial class FormAparatoBusqueda : Form
    {
        System.Windows.Forms.Form formAparatos = System.Windows.Forms.Application.OpenForms["FormAparatos"];
        System.Windows.Forms.Form formPrincipal = System.Windows.Forms.Application.OpenForms["form1"];

        private OperacionesBdAparato OperacionesBdAparato = new OperacionesBdAparato();

        private String nombreCliente;

        public FormDiagnosticarAparato formDiagnosticarAparato = new FormDiagnosticarAparato();

        private Aparato aparatoForm;

        public FormAparatoBusqueda()
        {
            InitializeComponent();
        }

        private void FormAparatoBusqueda_Load(object sender, EventArgs e)
        {
            int aparatoId = Int32.Parse(((FormAparatos)formAparatos).labelIdAparato.Text);
            nombreCliente = ((FormAparatos) formAparatos).labelNombreCliente.Text;

            mostrarAparato(aparatoId);

        }

        private void FormAparatoBusqueda_Shown(object sender, EventArgs e)
        {
            int aparatoId = Int32.Parse(((FormAparatos)formAparatos).labelIdAparato.Text);
            nombreCliente = ((FormAparatos)formAparatos).labelNombreCliente.Text;

            mostrarAparato(aparatoId);
        }
        private void mostrarAparato(int aparatoId)
        {
            Aparato aparato = OperacionesBdAparato.obtenerAparato(aparatoId);
            aparatoForm = aparato;

            labelNombre.Text = nombreCliente;
            labelTipo.Text = aparato.Tipo;
            labelMarca.Text = aparato.Marca;
            labelCable.Text = this.validarCable(aparato.Cable);
            labelControl.Text = this.validarControl(aparato.Control);
            labelModelo.Text = aparato.Modelo;
            labelIngreso.Text = aparato.FechaIngreso;
            labelEstado.Text = this.validarEstado(aparato.Entregado);
            pictureBox1.ImageLocation = aparato.LinkCloudinary;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


            if (this.validarEstado(aparato.Entregado).Equals("Pendiente"))
            {
                button2.Enabled = true;
                button2.Visible = true;
            }

            if(aparato.LinkCloudinary != "")
            {
                button3.Enabled= true;
                button3.Visible = true;
            }
        }

        private string validarCable(int cable)
        {
            if(cable == 1)
            {
                return "SI";
            }
            return "NO";
        }

        private string validarControl(int control)
        {
            if (control == 1)
            {
                return "SI";
            }
            return "NO";
        }

        private string validarEstado(int estado)
        {
            if(estado == 1)
            {
                return "Entregado";
            }
            if(estado == 2)
            {
                return "Diagnosticado";
            }
            return "Pendiente";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Owner.Show();  //Show the previous form
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG image (.png)|*.png|JPG image (.jpg)|*.jpg" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDiagnosticarAparato());
        }


        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aparatoForm.Problema);
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
