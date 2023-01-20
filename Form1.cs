using Administracion_de_Taller.Models;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Administracion_de_Taller
{
    public partial class Form1 : Form
    {
        public FormClientes formClientes;

        public FormAparatos formAparatos;

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
            label4.Text = DateTime.Now.ToLongDateString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            //Establecer conexion a la BD
            Conexion conexionBd = new Models.Conexion();
            MySqlConnection conexion = conexionBd.establecerConexion();

            MySqlDataReader reader = null;
            String query = "SELECT * FROM aparato WHERE entregado=0";

            try
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    label8.Text = "Hay aparatos pendientes por reparar.";
                    label8.ForeColor = Color.Red;
                    button1.Visible = true;
                    conexion.Close();
                }
                else
                {
                    label8.Text = "Felicitaciones, no hay reparaciones pendientes.";
                    label8.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}