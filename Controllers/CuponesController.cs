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

        /// <summary>
        /// Cargando la vista del registro de cupones
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Muestra los cupones disponibles para el usuario logueado en el momento
        /// juzgando por su rango o nivel de clasificación
        /// </summary>
        /// <returns></returns>
        public ActionResult MuestrarioCupones(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("MuestraProductos","Productos");
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario != null)
            {
                List<Cupones> cupones = db.Cupones.Where(x => x.idNivel == usuario.idNivel).ToList();
                return View(cupones);
            }
            return View();
        }
        /// <summary>
        /// Se escoge el cupón seleccionado para poder registrarlo como usado por el usuario
        /// Guardando en una variable Session para facil acceso luego en el Pedido
        /// </summary>
        /// <param name="id">Idntificador del cupón seleccionado</param>
        /// <returns></returns>
        public ActionResult CanjearCupon(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("MuestrarioCupones");
            }
            List<Cupones> cupones = new List<Cupones>();
            cupones.Add(db.Cupones.Find(id));
            Session["Cupones"] = cupones;
            return View();
        }
    }
}