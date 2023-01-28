using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Models
{
    internal class Diagnostico
    {
        private int id;
        private string diagnosticoAparato;
        private int costo;
        private DateTime fechaDiagnostico;
        private int idAparato;
        private int idCliente;

        public Diagnostico()
        {

        }
        public Diagnostico(int id, string diagnosticoAparato, int costo, DateTime fechaDiagnostico, int idAparato, int idCliente)
        {
            this.id = id;
            this.diagnosticoAparato = diagnosticoAparato;
            this.costo = costo;
            this.fechaDiagnostico = fechaDiagnostico;
            this.idAparato = idAparato;
            this.idCliente = idCliente;
        }

        public Diagnostico(string diagnosticoAparato, int costo, DateTime fechaDiagnostico, int idAparato, int idCliente)
        {
            this.diagnosticoAparato = diagnosticoAparato;
            this.costo = costo;
            this.fechaDiagnostico = fechaDiagnostico;
            this.idAparato = idAparato;
            this.idCliente = idCliente;
        }

        public int Id { get => id; set => id = value; }
        public string DiagnosticoAparato { get => diagnosticoAparato; set => diagnosticoAparato = value; }
        public int Costo { get => costo; set => costo = value; }
        public DateTime FechaDiagnostico { get => fechaDiagnostico; set => fechaDiagnostico = value; }
        public int IdAparato { get => idAparato; set => idAparato = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }

    }
}
