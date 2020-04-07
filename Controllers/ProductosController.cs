using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class ProductosController : Controller
    {
        private ImpecableContext db = new ImpecableContext();
        // GET: Productos
        /// <summary>
        /// Método que devuelve la vista de los productos
        /// </summary>
        /// <returns></returns>
        public ActionResult MuestraProductos()
        {
            return View(db.Producto.ToList());
        }

        /// <summary>
        /// Método que se apoya de PartialView para filtrar lista de los productos por la categoría seleccionada
        /// </summary>
        /// <param name="idTipo">Identificador de los tipos de producto</param>
        /// <returns></returns>
        public ActionResult FiltrarProductos(int idTipo)
        {
            return PartialView("_MuestraProducto", db.Producto.Where(x => x.idTipoProducto == idTipo).ToList());
        }
        
    }
}