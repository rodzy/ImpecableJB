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
        /// Listado de todos los cupones, con finalidad para el usuario Administador
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaCupones()
        {
            return View(db.Cupones.ToList());
        }

        /// <summary>
        /// Método para modificar el estado del cupón ya sea porque se va a quitar o si ya fue utilizado por el usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EliminarCupon(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No exite el cupón indicado";
                return RedirectToAction("ListaCupones");
            }
            Cupones cupones = db.Cupones.Find(id);
            if (cupones == null)
            {
                TempData["Mensaje"] = "No exite el cupón indicado";
                return RedirectToAction("ListaCupones");
            }
            if (cupones.estado == true) {
                cupones.estado = false;
            }
            else {
                cupones.estado = true;
            }

            db.Entry(cupones).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Mensaje"] = "Cupón desactivado con éxito";

            return RedirectToAction("ListaCupones");
            
        }

        //public ActionResult MuestrarioCupones()
        //{

        //}
    }
}