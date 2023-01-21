using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Administracion_de_Taller
{
    public partial class FormClientes : Form
    {
        public GuardarAparatoCliente formGuardarAparatoCliente;

        public FormClienteBusqueda formClienteBusqueda;

        public FormAparatos formAparatos;

        private OperacionesBdCliente operacionesCliente = new OperacionesBdCliente();

        public Form1 form1;

        public int idCliente;

        public string nombreCliente;

        System.Windows.Forms.Form formPrincipal = System.Windows.Forms.Application.OpenForms["form1"];


        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            llenarTabla();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Creando un cliente con los datos a guardar
            Cliente cliente = new Cliente(textBox2.Text + " " + textBox3.Text, textBox4.Text, textBox5.Text, DateTime.Now.ToString("yyyy/MM/dd"), 0);

            try
            {
                // Se realiza el INSERT
                idCliente = operacionesCliente.insertarCliente(cliente);

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Se llenan variables globales con informacion del cliente
            labelIdCliente.Text = idCliente.ToString();
            labelNombre.Text = cliente.Nombre;

            llenarTabla();
            limpiarBoxesRegistro();


            DialogResult dialogResult = MessageBox.Show("Desea registrar un aparato para este cliente?", "Aparato cliente", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                openChildForm(new GuardarAparatoCliente());  //Show Form assigning this form as the forms owner
            }
        }

        private Boolean validarTextBoxes()
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                return true;
            }
            return false;
        }

        private void llenarTabla()
        {
            // Se realiza el FindAll Clientes
            List<Cliente> clientes = operacionesCliente.obtenerClientes();

            dataGridView1.DataSource = clientes;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].HeaderText = "aparatos";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Se realiza el FindAllByName Clientes
            List<Cliente> clientes = operacionesCliente.obtenerClientesPorNombre(textBox1.Text);

            if (clientes.Count > 0)
            {
                if(clientes.Count == 1)
                {
                    // Si solo hay un cliente, se abre el form para ver la informacion de un cliente en específico.
                    idCliente = clientes[0].Id;
                    nombreCliente = clientes[0].Nombre;
                    labelIdCliente.Text = idCliente.ToString();
                    openChildForm(new FormClienteBusqueda());
                }

                //Se llena la tabla con la informacion de los clientes
                dataGridView1.DataSource = clientes;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            else
            {
                MessageBox.Show("No existen registros");
                llenarTabla();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            } else
            {
                button1.Enabled = false;
                llenarTabla();
            }
        }

        void formAparatos_FormClosed(object sender, FormClosedEventArgs e)
        {
            formAparatos = null;  //If form is closed make sure reference is set to null
            Show();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Se manda a llamar la interfaz de ver información del cliente
            if (e.RowIndex >= 0 && dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);

                idCliente = id;
                labelIdCliente.Text = idCliente.ToString();
                openChildForm(new FormClienteBusqueda());
            }
        }

        private void limpiarBoxesRegistro()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (validarTextBoxes())
            {
                button2.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (validarTextBoxes())
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (validarTextBoxes())
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (validarTextBoxes())
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
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
