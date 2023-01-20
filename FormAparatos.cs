using Administracion_de_Taller.Models;
using Administracion_de_Taller.Repository;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Administracion_de_Taller
{
    public partial class FormAparatos : Form
    {

        private OperacionesBdCliente operacionesCliente = new OperacionesBdCliente();
        private OperacionesBdAparato operacionesAparato = new OperacionesBdAparato();
        private OperacionesBdMarca operacionesMarca = new OperacionesBdMarca();
        private OperacionesBdTipo operacionesTipo = new OperacionesBdTipo();

        public FormAparatoBusqueda formAparatoBusqueda;
        public FormClientes formClientes;

        private int filtroEstado;

        public int idAparato;
        public string nombreCliente;

        private List<Cliente> allClientes;

        public FormAparatos()
        {
            InitializeComponent();
        }

        private void FormAparatos_Load(object sender, EventArgs e)
        {

            // Llenamos el combobox con los clientes que hay.
            List<Cliente> clientes = operacionesCliente.obtenerClientes();
            foreach (Cliente cliente in clientes)
            {
                comboBox4.Items.Add(cliente.Nombre);
            }
            allClientes = clientes;

            llenarTiposyMarcas();

            llenarTabla(operacionesAparato.obtenerAparatos());
         
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            volverFormularioInicio();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            volverFormularioInicio();
        }

        private void panel3_MouseClick(object sender, PaintEventArgs e)
        {
            volverFormularioInicio();
        }


        private void volverFormularioInicio()
        {
            Owner.Show();  //Show the previous form
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Enabled == true)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
            verificarFiltros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Enabled == true)
            {
                comboBox2.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
            }
            verificarFiltros();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Enabled == true)
            {
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
            }
            verificarFiltros();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.Enabled == true)
            {
                comboBox4.Enabled = false;
            } else
            {
                comboBox4.Enabled = true;
            }
            verificarFiltros();


        }

        private String validarActivos()
        {
            String activos = "";

            if (comboBox1.Enabled && comboBox1.SelectedIndex > -1)
            {
                activos += "1";
            }
            if (comboBox2.Enabled && comboBox2.SelectedIndex > -1)
            {
                activos += "2";
            }
            if (comboBox3.Enabled && comboBox3.SelectedIndex > -1)
            {
                activos += "3";
            }
            if (comboBox4.Enabled && comboBox4.SelectedIndex > -1)
            {
                activos += "4";
            }

            return activos;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String filtrosActivos = validarActivos();

            List<AparatoTabla> aparatos = new List<AparatoTabla>();

            String query = "";
            int idCliente = 0;
            switch (filtrosActivos)
            {
                case "1234":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND marca='{comboBox2.SelectedItem.ToString()}' AND entregado={filtroEstado} AND idCliente={idCliente}";
                    break;
                case "1":
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}'";
                    break;
                case "2":
                    query = $"SELECT * FROM aparato WHERE marca='{comboBox2.SelectedItem.ToString()}'";
                    break;
                case "3":
                    query = $"SELECT * FROM aparato WHERE entregado={filtroEstado}";
                    break;
                case "4":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE idCliente={idCliente}";
                    break;
                case "12":
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND marca='{comboBox2.SelectedItem.ToString()}'";
                    break;
                case "13":
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND entregado={filtroEstado}";
                    break;
                case "14":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND idCliente={idCliente}";
                    break;
                case "23":
                    query = $"SELECT * FROM aparato WHERE marca='{comboBox2.SelectedItem.ToString()}' AND entregado={filtroEstado}";
                    break;
                case "24":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND idCliente={idCliente}";
                    break;
                case "34":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE entregado='{filtroEstado}' AND idCliente={idCliente}";
                    break;
                case "123":
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND marca='{comboBox2.SelectedItem.ToString()}' AND entregado={filtroEstado}";
                    break;
                case "124":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND marca='{comboBox2.SelectedItem.ToString()}' AND idCliente={idCliente}";
                    break;
                case "134":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE tipo='{comboBox1.SelectedItem.ToString()}' AND entregado={filtroEstado} AND idCliente={idCliente}";
                    break;
                case "234":
                    idCliente = buscarIdCliente();
                    query = $"SELECT * FROM aparato WHERE marca='{comboBox2.SelectedItem.ToString()}' AND entregado={filtroEstado} AND idCliente={idCliente}";
                    break;
                default:
                    MessageBox.Show("No se hizo una combinación valida");
                    break;
            }
            if (!query.Equals(""))
            {
                aparatos = operacionesAparato.consultaCustomLista(query);
                this.llenarTabla(aparatos);
            }
        }

        private void llenarTabla(List<AparatoTabla> aparatos)
        {
            
            // Se realiza el FindAll Clientes
            dataGridView1.DataSource = aparatos;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; 
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem.ToString().Equals("PENDIENTE"))
            {
                filtroEstado = 0;
            }
            if (comboBox4.SelectedItem.ToString().Equals("ENTREGADO"))
            {
                filtroEstado = 1;
            }
            verificarFiltros();
        }

        private int buscarIdCliente()
        {
            int idCliente = 0;
            foreach (Cliente cliente in allClientes)
            {
                if (cliente.Nombre.Equals(comboBox4.SelectedItem.ToString()))
                {
                    idCliente = cliente.Id;
                }
            }
            return idCliente;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            llenarTabla(operacionesAparato.obtenerAparatos());

            comboBox1.Text = "Seleccione un tipo";
            comboBox2.Text = "Seleccione una marca";
            comboBox3.Text = "Seleccione un estado del aparato";
            comboBox4.Text = "Seleccione un cliente";

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;

            button6.Enabled = false;
            button6.Visible= false;

        }

        private void verificarFiltros()
        {
            if ((comboBox1.Enabled && comboBox1.SelectedIndex > -1) || (comboBox2.Enabled && comboBox2.SelectedIndex > -1) || (comboBox3.Enabled && comboBox3.SelectedIndex > -1) || (comboBox4.Enabled && comboBox4.SelectedIndex > -1) )
            {
                button6.Enabled = true;
                button6.Visible = true;
            } else
            {
                button6.Enabled = false;
                button6.Visible = false;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            verificarFiltros();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            verificarFiltros();
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            verificarFiltros();
        }

        void formAparatoBusqueda_FormClosed(object sender, FormClosedEventArgs e)
        {
            formAparatoBusqueda = null;  //If form is closed make sure reference is set to null
            Show();
        }

        void formClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            formClientes = null;
            Show();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Se manda a llamar la interfaz de ver información del cliente
            if (e.RowIndex >= 0 && dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value);
                string nombre = (dataGridView1[5, e.RowIndex].Value).ToString();

                idAparato = id;
                labelIdAparato.Text = idAparato.ToString();

                nombreCliente = nombre;
                labelNombreCliente.Text = nombre;

                if (formAparatoBusqueda == null)
                {
                    formAparatoBusqueda = new FormAparatoBusqueda();   //Create form if not created
                    formAparatoBusqueda.FormClosed += formAparatoBusqueda_FormClosed;  //Add eventhandler to cleanup after form closes
                }
                formAparatoBusqueda.Show(this);  //Show Form assigning this form as the forms owner
                Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormNuevaMarca formMarca = new FormNuevaMarca();
            formMarca.Show(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormNuevoTipo formTipo = new FormNuevoTipo();
            formTipo.Show(this);
        }

        private void llenarTiposyMarcas()
        {
            List<Marca> marcas = operacionesMarca.obtenerMarcas();

            foreach (Marca marca in marcas)
            {
                comboBox2.Items.Add(marca.Nombre);
            }

            List<Tipo> tipos = operacionesTipo.obtenerTipos();

            foreach(Tipo tipo in tipos)
            {
                comboBox1.Items.Add(tipo.Nombre);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Click(object sender, EventArgs e)
        {
            mostrarFormularioClientes();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            mostrarFormularioClientes();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mostrarFormularioClientes();
        }

        private void mostrarFormularioClientes()
        {
            if (formClientes == null)
            {
                formClientes = new FormClientes();   //Create form if not created
                formClientes.FormClosed += formClientes_FormClosed;  //Add eventhandler to cleanup after form closes
            }
            formClientes.Show(this);  //Show Form assigning this form as the forms owner
            Hide();
        }


    }
}
