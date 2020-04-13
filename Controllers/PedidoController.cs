using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class PedidoController : Controller
    {

        ImpecableContext db = new ImpecableContext();

        // GET: Pedido
        /// <summary>
        /// Método que carga la lista de pedidos para el usuario que esté autenticado
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPedidos()
        {
            List<Pedido> pedidos = db.Pedido.Where(x => x.idUsuario == Convert.ToInt32(Session["Usuario"])).ToList();
            if (pedidos == null)
            {
                TempData["Pedidos"] = "No tiene pedidos realizados";
                return View();
            }
            else
            {
                return View(pedidos);
            }
        }

        /// <summary>
        /// Metodo de busqueda para cargar la busqueda por nombre de usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult PedidosUsuario()
        {
            return View();
        }

        /// <summary>
        /// Método para cargar la vista de los pedidos de usuario por el correo del usuario
        /// </summary>
        /// <param name="correo">Correo del usuario</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PedidosUsuario(string correo)
        {
            if(correo.Equals("") || correo == null)
            {
                ViewBag.ValidaCorreo = "El campo de correo es requerido";
            }
            Usuario usuario = db.Usuario.Where(x => x.correoElectronico.Equals(correo)).FirstOrDefault();
            if (usuario != null)
            {
                List<Pedido> pedidos = db.Pedido.Where(x => x.idUsuario == usuario.idUsuario).ToList();
                return PartialView("_PedidosUsuario", pedidos);
            }
            else
            {
                ViewBag.ValidaCorreo = "El correo electrónico no se encuentra registrado";
                return View();
            }
        }


    }
}