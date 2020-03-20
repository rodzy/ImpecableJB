using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpecableJB.Models
{
    public class CarritoItem
    {
        private Producto _producto;
        private int _cantidad;
        /// <summary>
        /// Constructor class
        /// </summary>
        public CarritoItem()
        {

        }
        /// <summary>
        /// Constructor sobrecargado
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="cantidad"></param>
        public CarritoItem(Producto producto,int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;
        }

        public Producto Producto { get => _producto; set => _producto = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
    }
}