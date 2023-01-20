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
    public partial class FormNuevoTipo : Form
    {

        private OperacionesBdTipo operacionesBdTipo = new OperacionesBdTipo();

        public FormNuevoTipo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Tipo tipo = new Tipo(textBox1.Text);

            operacionesBdTipo.insertarTipo(tipo);

            MessageBox.Show($"El aparato de tipo {textBox1.Text} ha sido registrada correctamente");

            this.Close();
        }
    }
}
