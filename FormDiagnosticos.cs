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
    public partial class FormDiagnosticos : Form
    {

        private OperacionesBdDiagnostico operacionesDiagnostico = new OperacionesBdDiagnostico();
        public FormDiagnosticos()
        {
            InitializeComponent();
        }

        private void FormDiagnosticos_Load(object sender, EventArgs e)
        {
            llenarTabla();
        }

        private void llenarTabla()
        {

            List<DiagnosticoTabla> diagnosticosTabla = operacionesDiagnostico.obtenerDiagnosticosTabla();

            dataGridView1.DataSource = diagnosticosTabla;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            //dataGridView1.Columns[5].HeaderText = "aparatos";

        }

    }
}
