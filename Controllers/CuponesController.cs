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
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
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
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            return View(db.Cupones.Where(x=>x.estado==true).ToList());
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
            if (Session["Usuario"] == null)
            {
                TempData["Mensaje"] = "Inicie sesión para poder acceder";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("MuestraProductos","Productos");
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario != null)
            {
                List<Cupones> cupones = db.Cupones.Where(x => x.estado == true).ToList();
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
            List<Cupones> cupones = new List<Cupones>();
            
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("MuestrarioCupones");
            }
            Cupones cup = db.Cupones.Find(id);
            if (cup == null)
            {
                TempData["Mensaje"] = "El cupón no existe";
                return RedirectToAction("MuestrarioCupones");
            }
            //Verificar que se tengan elementos en el carrito para la poder utilizar cupones
            if (Session["Carrito"] == null)
            {
                TempData["Mensaje"] = "Para utilizar cupones debe tener articulos elegidos en el carrito";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            else
            {
                //Recorriendo el carrito
                foreach (var item in Session["Carrito"] as List<CarritoItem>)
                {
                    //Añadiendo los cupones a la lista para poder aplicar los descuentos
                    if (item.Producto.idProducto.Equals(cup.idProducto))
                    {
                        cupones.Add(cup);
                        Session["Cupones"] = cupones;                        
                    }
                }
                TempData["Mensaje"] = "Cupón agregado con éxito, el descuento se verá reflejado en el total del pedido";
                return RedirectToAction("CarritoPrevia","Carrito");
            }

        }

        /// <summary>
        /// Método que se apoya de los Ajax helpers para filtrar los cupones
        /// Dependiendo 1=Cupones disponibles / 2=Cupones gastados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FiltrarCupones(int? id)
        {
            switch (id)
            {
                case 1:
                    return PartialView("_CuponesDisponibles", db.Cupones_Usuarios.Where(x=>x.estado==true).ToList());
                case 2:
                    return PartialView("_CuponesGastados", db.Cupones_Usuarios.Where(x => x.estado == false).ToList());
                default:
                    return View();
            }
        }

        /// <summary>
        /// Método para editar el cupón seleccionado de la lista
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditarCupones(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("ListaCupones");
            }
            Cupones cup = db.Cupones.Find(id);
            if (cup == null)
            {
                TempData["Mensaje"] = "El cupón no existe";
                return RedirectToAction("ListaCupones");
            }
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", cup.idProducto);
            ViewBag.idNivel = new SelectList(db.Nivel, "idNivel", "nombre", cup.idNivel);
            return View(cup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cupones"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditarCupones(Cupones cupones)
        {
            if (cupones == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("ListaCupones");
            }
            db.Entry(cupones).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["Mensaje"] = "Cupón modificado con éxito";
            return RedirectToAction("ListaCupones");
        }

        /// <summary>
        /// Método para cargar la vista con los datos del cupón 
        /// para asignar al usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AsignarUsuario(int? id)
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            if (id == null)
            {
                TempData["Mensaje"] = "No existe el identificador especificado";
                return RedirectToAction("ListaCupones");
            }
            Cupones cupones = db.Cupones.Find(id);
            Session["idCupon"] = id;
            if (cupones == null)
            {
                TempData["Mensaje"] = "El cupón no existe";
                return RedirectToAction("ListaCupones");
            }
            return View(cupones);
        }
        /// <summary>
        /// Método para asignar los cupones al usuario por correo electrónico
        /// y retornar con mensaje de éxito si hizo la relación con el usuario
        /// sino un mensaje de error que puede darse en caso de que la relación ya exista
        /// con esto evitamos los cupones duplicados para los usuarios
        /// </summary>
        /// <param name="cupones"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AsignarUsuario()
        {
            string correo = Request["txtCorreo"].ToString();
            Cupones cupones = db.Cupones.Find(Session["idCupon"]);
            Usuario usuario = db.Usuario.Where(x => x.correoElectronico == correo && x.idNivel == cupones.idNivel).FirstOrDefault();
            if (usuario == null)
            {
                TempData["Mensaje"] = "El usuario especificado no cumple con los requisitos";
                return RedirectToAction("AsignarUsuario", new { id = cupones.idCupones });
            }
            else
            {
                Cupones_Usuario cupones_Usuario = new Cupones_Usuario();
                cupones_Usuario.idCupones = cupones.idCupones;
                cupones_Usuario.idUsuario = usuario.idUsuario;
                cupones_Usuario.estado = true;
                    try
                    {
                        db.Cupones_Usuarios.Add(cupones_Usuario);
                        db.SaveChanges();
                        TempData["Mensaje"] = "Cupón asignado a usuario con éxito";
                        return RedirectToAction("ListaCupones");
                    }
                    catch
                    {
                        TempData["Mensaje"] = "El cupón seleccionado ya se encuentra asignado al usuario";
                        return RedirectToAction("ListaCupones");
                    }
                } 
        }
    }
}