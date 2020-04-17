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
        private string _codigo;
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
        /// <param name="codigo"></param>
        public CarritoItem(Producto producto,int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;
        }

        public CarritoItem(Producto producto, int cantidad, string codigo)
        {
            this._producto = producto;
            this._cantidad = cantidad;
            this._codigo = codigo;
        }

        public Producto Producto { get => _producto; set => _producto = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
    }
}