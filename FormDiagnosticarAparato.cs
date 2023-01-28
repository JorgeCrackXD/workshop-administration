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
    public partial class FormDiagnosticarAparato : Form
    {
        private int aparatoIdGlobal;
        private string nombreCliente;
        private OperacionesBdAparato operacionesBdAparato = new OperacionesBdAparato();
        private OperacionesBdCliente operacionesBdCliente = new OperacionesBdCliente();
        private OperacionesBdDiagnostico operacionesBdDiagnostico= new OperacionesBdDiagnostico();
        private Aparato aparatoForm;
        private Cliente clienteForm;

        System.Windows.Forms.Form formAparatos = System.Windows.Forms.Application.OpenForms["FormAparatos"];
        public FormDiagnosticarAparato()
        {
            InitializeComponent();
        }

        private void FormDiagnosticarAparato_Load(object sender, EventArgs e)
        {
            int aparatoId = Int32.Parse(((FormAparatos)formAparatos).labelIdAparato.Text);
            aparatoIdGlobal = aparatoId;
            nombreCliente = ((FormAparatos)formAparatos).labelNombreCliente.Text;

            mostrarAparato(aparatoId);
        }

        private void mostrarAparato(int aparatoId)
        {
            Aparato aparato = operacionesBdAparato.obtenerAparato(aparatoId);
            Cliente cliente = operacionesBdCliente.obtenerClientePorNombre(nombreCliente);
            aparatoForm = aparato;
            clienteForm = cliente;

            textBox1.Text = nombreCliente;
            textBox2.Text = aparato.Tipo;
            textBox3.Text = aparato.Marca;
            textBox4.Text = aparato.Modelo;
            textBox5.Text = this.validarControl(aparato.Control);
            textBox6.Text = this.validarCable(aparato.Cable);
        }

        private string validarCable(int cable)
        {
            if (cable == 1)
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Owner.Show();  //Show the previous form
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Diagnostico diagnostico = new Diagnostico(richTextBox1.Text, Int32.Parse(textBox7.Text), DateTime.Now, aparatoForm.Id, clienteForm.Id);
            operacionesBdDiagnostico.insertarDiagnostico(diagnostico);

            MessageBox.Show("El diagnostico ha sido asignado correctamente.");

        }
    }
}
