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
    public partial class FormAccionRapidaCliente : Form
    {
        private OperacionesBdCliente operacionesCliente = new OperacionesBdCliente();

        System.Windows.Forms.Form formPrincipal = System.Windows.Forms.Application.OpenForms["form1"];


        public FormAccionRapidaCliente()
        {
            InitializeComponent();
        }

        private void FormAccionRapidaCliente_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Nombre = textBox1.Text; 
            cliente.Telefono = textBox2.Text;
            cliente.Direccion = textBox3.Text;
            cliente.FechaRegistro = DateTime.Now.ToString("yyyy/MM/dd");
            cliente.AparatosEnTaller = 0;

            operacionesCliente.insertarCliente(cliente);
            MessageBox.Show("El gerente se ha actualizado correctamente.");

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            openChildForm(new FormClientes());

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
