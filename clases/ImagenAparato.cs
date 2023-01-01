using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion_de_Taller.clases
{
    internal class ImagenAparato
    {
        private int id;
        private string idCloudinary;
        private string linkCloudinary;
        private int idCliente;

        public ImagenAparato(int id, string idCloudinary, string linkCloudinary, int idCliente)
        {
            this.id = id;
            this.idCloudinary = idCloudinary;
            this.linkCloudinary = linkCloudinary;
            this.idCliente = idCliente;
        }

        public ImagenAparato()
        {

        }

        public ImagenAparato(string idCloudinary, string linkCloudinary, int idCliente)
        {
            this.idCloudinary = idCloudinary;
            this.linkCloudinary = linkCloudinary;
            this.idCliente = idCliente;
        }

        public int Id { get => id; set => id = value; }
        public string IdCloudinary { get => idCloudinary; set => idCloudinary = value; }
        public string LinkCloudinary { get => linkCloudinary; set => linkCloudinary = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
    }
}
