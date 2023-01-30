using Administracion_de_Taller.Models;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using WMPLib;

namespace Administracion_de_Taller
{
    public partial class Form1 : Form
    {
        public FormClientes formClientes;

        public FormAparatos formAparatos;

        WindowsMediaPlayer wplayer = new WindowsMediaPlayer();


        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("T");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            openChildForm(new FormClientes());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openChildForm(new FormClientes());
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            openChildForm(new FormClientes());
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAparatos());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAparatos());
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            openChildForm(new FormAparatos());
        }


        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle= FormBorderStyle.None;
            childForm.Dock= DockStyle.Fill;
            panelForms.Controls.Add(childForm);
            panelForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAccionRapidaCliente());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormNuevaMarca formMarca = new FormNuevaMarca();
            formMarca.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormNuevoTipo formTipo = new FormNuevoTipo();
            formTipo.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAccionRapidaAparato());
        }

        private void panelForms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDiagnosticos());
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            openChildForm(new FormDiagnosticos());
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            openChildForm(new FormDiagnosticos());
        }
    }
}