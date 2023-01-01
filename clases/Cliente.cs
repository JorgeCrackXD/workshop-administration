using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.clases
{
    internal class Cliente
    {
        private int id;
        private String nombre;
        private String telefono;
        private String direccion;
        private String fechaRegistro;
        private int aparatosEnTaller;

        public Cliente(string nombre, string telefono, string direccion, string fechaRegistro, int aparatosEnTaller)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.direccion = direccion;
            this.fechaRegistro = fechaRegistro;
            this.aparatosEnTaller = aparatosEnTaller;
        }

        public Cliente()
        {

        }

        public Cliente(int id, int aparatosEnTaller)
        {
            this.id = id;
            this.aparatosEnTaller = aparatosEnTaller;
        }
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public int AparatosEnTaller { get => aparatosEnTaller; set => aparatosEnTaller = value; }
    }
}
