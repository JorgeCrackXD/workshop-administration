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
    public partial class FormNuevaMarca : Form
    {
        private OperacionesBdMarca operacionesBdMarca = new OperacionesBdMarca();
        public FormNuevaMarca()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca(textBox1.Text);

            operacionesBdMarca.insertarMarca(marca);

            MessageBox.Show($"La marca {textBox1.Text} ha sido registrada correctamente");

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            } else
            {
                button1.Enabled = false;
            }
        }
    }
}
