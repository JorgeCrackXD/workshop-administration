using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Models
{
    internal class Tipo
    {
        private int id;
        private string nombre;

        public Tipo(string nombre)
        {
            this.nombre = nombre;
        }

        public Tipo()
        {

        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
