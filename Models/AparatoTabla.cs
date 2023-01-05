using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Models
{
    internal class AparatoTabla
    {
        private int id;
        private string tipo;
        private string marca;
        private string modelo;
        private string fechaIngreso;
        private string cliente;

        public AparatoTabla(int id, string tipo, string marca, string modelo, string fechaIngreso, string cliente)
        {
            this.id = id;
            this.tipo = tipo;
            this.marca = marca;
            this.modelo = modelo;
            this.fechaIngreso = fechaIngreso;
            this.cliente = cliente;
        }

        public AparatoTabla()
        {

        }

        public int Id { get => id; set => id = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public string FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public string Cliente { get => cliente; set => cliente = value; }
    }
}
