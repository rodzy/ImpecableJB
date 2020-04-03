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
        public ActionResult MuestraProductos()
        {
            if (TempData.ContainsKey("Validacion") && TempData.ContainsKey("Rango") && TempData.ContainsKey("Usuario"))
            {
                ViewBag.mensaje = TempData["Validacion"].ToString();
                ViewBag.rango = TempData["Rango"].ToString();
                ViewBag.Usuario = TempData["Usuario"].ToString();
            }
            return View(db.Producto.ToList());
        }

        public ActionResult FiltrarProductos(int idTipo)
        {
            return PartialView("_MuestraProducto", db.Producto.Where(x => x.idTipoProducto == idTipo).ToList());
        }
        
    }
}