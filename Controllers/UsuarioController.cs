using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (TempData.ContainsKey("Registrado"))
            {
                ViewBag.EstadoRegistro = TempData["Registrado"].ToString();
            }
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
                Session["Rango"] = user.Nivel;
                Session["Rol"] = user.Rol.descripcion;
                Session["ImagenRango"] = "~/Content/Rangos/"+user.Nivel.nombre+".png";
                Session["ListaRangos"] = db.Nivel.ToList();
                if (user.Rol.descripcion.Equals("Administrador"))
                {
                    Session["Nombre"] = "Bienvenido,"+ user.nombre + "(" + user.Rol.descripcion + ")";                  
                }
                else
                {
                    Session["Nombre"] = "Bienvenido," + user.nombre;
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
        /// <summary>
        /// Método que devuelve la vista del registro de usuarios para el cliente
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistroCliente()
        {
            return View();
        }
        /// <summary>
        /// Método que se apoya de POST para añadir un nuevo cliente a la base de datos
        /// Se incluyen todos los datos necesarios para el manejo del cliente
        /// Por defecto se establece el rol en 2 que pertenece a Cliente (Los administradores solo en base de datos se registran)
        /// Nivel= Por defecto en la estapa inicial que es -Sin Rango-
        /// </summary>
        /// <param name="usuario">Objeto que viene desde el modelo de la vista construido</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistroCliente(Usuario usuario)
        {
            usuario.idNivel = 1;
            usuario.idRol = 2;
            if (usuario != null)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                TempData["Registrado"] = "Usuario correctamente registrado";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            else
            {
                TempData["Registrado"] = "Hay un problema al registrar el usuario";
                return View(usuario);
            }
        }

        /// <summary>
        /// Método para encontrar los detalles del cliente
        /// </summary>
        /// <param name="id">Identificador del usuario seleccionado</param>
        /// <returns></returns>
        public ActionResult DetallesUsuario(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "Especifique el usuario para acceder a los detalles";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                TempData["Mensaje"] = "No existe el usuario especificado";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            return View("DetallesUsuario", usuario);
        }

        /// <summary>
        /// Método que realiza una busqueda de los datos del usuario para cargarlos en modo Editable 
        /// </summary>
        /// <param name="id">Identificador del cliente especificado</param>
        /// <returns></returns>
        public ActionResult EditarDatosCliente(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No se encuentran los datos del usuario especificado";
                return RedirectToAction("MuestraProductos","Productos");
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                TempData["Mensaje"] = "No se encuentran los datos del usuario especificado";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            return View(usuario);
        }
        /// <summary>
        /// Método que se apoya de POST para actulizar la información del usuario
        /// </summary>
        /// <param name="usuario">Modelo Usuario de la vista EditarDatosCliente</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditarDatosCliente(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mensaje"] = "Información de cliente actualizada";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            return View(usuario);
        }

        /// <summary>
        /// Método para eliminar de manera lógica una cuenta de un usuario tipo cliente
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns></returns>
        public ActionResult EliminarUsuario(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "No se encuentran los datos del usuario especificado";
                if (Session["Rol"].ToString().Equals("Administrador"))
                {
                    return RedirectToAction("ListaUsuarios");
                }
                else
                {
                    return RedirectToAction("DetallesUsuario");
                }
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                TempData["Mensaje"] = "No se encuentran los datos de usuario especificado";
                return RedirectToAction("DetallesUsuario");
            }
            if (usuario.estado==true)
            {
                usuario.estado = false;
            }
            else
            {
                usuario.estado = true;
            }
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Mensaje"] = "Usuario eliminado con éxito";
            return RedirectToAction("MuestraProductos", "Producto");
        }

        /// <summary>
        /// Método que retorna una lista de usaurios para el control por el administrador
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaUsuarios()
        {
            return View(db.Usuario.ToList());
        }


    }
}