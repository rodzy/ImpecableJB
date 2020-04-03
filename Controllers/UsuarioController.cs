using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class UsuarioController : Controller
    {
        /// <summary>
        /// Contexto del entity framework
        /// </summary>
        ImpecableContext db = new ImpecableContext();
        // GET: Login
        public ActionResult InicioClientes()
        {
            return View();
        }
        /// <summary>
        /// Método POST para inciar la sesión verificando los credenciales del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InicioClientes(Usuario usuario)
        {
            Usuario user = db.Usuario.Where(x => x.correoElectronico.Equals(usuario.correoElectronico) && x.contrasena.Equals(usuario.contrasena)).FirstOrDefault();
            if (user != null)
            {
                Session["Usuario"] = user.idUsuario;
                TempData["Rango"] = user.Nivel.nombre;
                if (user.Rol.descripcion.Equals("Administrador"))
                {
                    TempData["Usuario"] = user.correoElectronico + "(" + user.Rol.descripcion + ")";
                }
                else
                {
                    TempData["Usuario"] = "Bienvenido," + user.nombre;
                }
                   
                return RedirectToAction("MuestraProductos","Productos");
            }
            else
            {
                ViewBag.mensaje = "No se encuentra el usuario, puede ser que no se encuentre registrado";
                return View();
            }
        }
        /// <summary>
        /// Método que cierra todas las Sessions activas desde el inicio de sesión
        /// acá se incluyen las de Usuario y las de Carro de compras
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSession()
        {
            Session.Abandon();
            return RedirectToAction("MuestraProductos","Productos");
        }
        public ActionResult RegistroClientes()
        {
            return View();
        }

    }
}