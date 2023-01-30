using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Models
{
    internal class DiagnosticoTabla
    {
        private int id;
        private string diagnosticoAparato;
        private int costo;
        private DateTime fechaDiagnostico;
        private string tipoAparato;
        private string nombreCliente;

        public DiagnosticoTabla() 
        {

        }

        public DiagnosticoTabla(string diagnosticoAparato, int costo, DateTime fechaDiagnostico, string tipoAparato, string nombreCliente)
        {
            this.diagnosticoAparato = diagnosticoAparato;
            this.costo = costo;
            this.fechaDiagnostico = fechaDiagnostico;
            this.tipoAparato = tipoAparato;
            this.nombreCliente = nombreCliente;
        }

        public DiagnosticoTabla(int id, string diagnosticoAparato, int costo, DateTime fechaDiagnostico, string tipoAparato, string nombreCliente)
        {
            this.id = id;
            this.diagnosticoAparato = diagnosticoAparato;
            this.costo = costo;
            this.fechaDiagnostico = fechaDiagnostico;
            this.tipoAparato = tipoAparato;
            this.nombreCliente = nombreCliente;
        }

        public int Id { get => id; set => id = value; }
        public string DiagnosticoAparato { get => diagnosticoAparato; set => diagnosticoAparato = value; }
        public int Costo { get => costo; set => costo = value; }
        public DateTime FechaDiagnostico { get => fechaDiagnostico; set => fechaDiagnostico = value; }
        public string TipoAparato { get => tipoAparato; set => tipoAparato = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
    }
}
