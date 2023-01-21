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

        private void reproducirMusica()
        {
            string[] fileArray = Directory.GetFiles(@"C:\Music", "*.mp3");
            if(fileArray.Length < 0)
            {
                MessageBox.Show("No hay música actualmente.");
            }
            Random rnd = new Random();

            int cancion = rnd.Next(fileArray.Length);

            wplayer.URL = fileArray[cancion];
            wplayer.controls.play();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            reproducirMusica();
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
    }
}