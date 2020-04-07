using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class CuponesController : Controller
    {
        ImpecableContext db = new ImpecableContext();
        // GET: Cupones
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RegistrarCupones()
        {
            ViewBag.idProductos = new SelectList(db.Producto, "idProducto", "nombre");
            ViewBag.idNivel = new SelectList(db.Nivel, "idNivel", "nombre");
            return View();
        }

        /// <summary>
        /// Método POST para registrar los cupones por el administrador, a se asignan
        /// a los productos y niveles correspondientes
        /// </summary>
        /// <param name="cupones">Modelo cupones</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrarCupones(Cupones cupones)
        {
            if (ModelState.IsValid)
            {
                db.Cupones.Add(cupones);
                db.SaveChanges();
                TempData["Registrado"] = "Cupón registrado correctamente";
                return RedirectToAction("ListaCupones", "Cupones");
            }
            ViewBag.idProductos = new SelectList(db.Producto, "idProducto", "nombre",cupones.idProducto);
            ViewBag.idNivel = new SelectList(db.Nivel, "idNivel", "nombre",cupones.idNivel);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaCupones()
        {
            return View(db.Cupones.ToList());
        }
    }
}