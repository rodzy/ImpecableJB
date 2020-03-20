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
            return View(db.Producto.ToList());
        }
        
    }
}