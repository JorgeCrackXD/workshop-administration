using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.Models
{
    internal class Aparato
    {
        private int id;
        private string tipo;
        private string marca;
        private string modelo;
        private int control;
        private int cable;
        private string fechaIngreso;
        private string fechaDiagnostico;
        private string fechaEntrega;
        private int entregado;
        private string linkCloudinary;
        private int idCliente;

        public Aparato(int id, string tipo, string marca, string modelo, int control, int cable, string fechaIngreso, string fechaDiagnostico, string fechaEntrega, int entregado, string linkCloudinary, int idCliente)
        {
            this.id = id;
            this.tipo = tipo;
            this.marca = marca;
            this.modelo = modelo;
            this.control = control;
            this.cable = cable;
            this.fechaIngreso = fechaIngreso;
            this.fechaDiagnostico = fechaDiagnostico;
            this.fechaEntrega = fechaEntrega;
            this.entregado = entregado;
            this.linkCloudinary = linkCloudinary;
            this.idCliente = idCliente;
        }

        public Aparato()
        {

        }

        public Aparato(string tipo, string marca, string modelo, int control, int cable, string fechaIngreso, int entregado, string linkCloudinary, int idCliente)
        {
            this.tipo = tipo;
            this.marca = marca;
            this.modelo = modelo;
            this.control = control;
            this.cable = cable;
            this.fechaIngreso = fechaIngreso;
            this.entregado = entregado;
            this.linkCloudinary = linkCloudinary;
            this.idCliente = idCliente;
        }

        public int Id { get => id; set => id = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public int Control { get => control; set => control = value; }
        public int Cable { get => cable; set => cable = value; }
        public string FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public string FechaDiagnostico { get => fechaDiagnostico; set => fechaDiagnostico = value; }
        public string FechaEntrega { get => fechaEntrega; set => fechaEntrega = value; }
        public int Entregado { get => entregado; set => entregado = value; }
        public string LinkCloudinary { get => linkCloudinary; set => linkCloudinary = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
    }
}
