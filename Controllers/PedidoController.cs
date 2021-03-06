﻿using ImpecableJB.Models;
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
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            if (Session["Usuario"] == null)
            {
                TempData["Mensaje"] = "Inicie sesión para poder acceder";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            int? id = Convert.ToInt32(Session["Usuario"]);
            if (id == null)
            {
                TempData["Mensaje"] = "No tiene un identificador";
                return RedirectToAction("MuestraProductos", "Productos");
            }
            List <Pedido> pedidos = db.Pedido.Where(x => x.idUsuario == id && x.estado==true).ToList();
            if (pedidos == null)
            {
                TempData["Mensaje"] = "No tiene pedidos realizados";
                return RedirectToAction("MuestraProductos", "Productos");
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
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            return View();
        }

        /// <summary>
        /// Método para cargar la vista de los pedidos de usuario por el correo del usuario
        /// </summary>
        /// <param name="us">Model usuario</param>
        /// <returns></returns>
        public ActionResult BuscarCorreo(Usuario us)
        {
            if (!Session["Rol"].Equals("Administrador"))
            {
                return RedirectToAction("MuestraProductos");
            }
            Usuario usuario = db.Usuario.Where(x => x.correoElectronico.Equals(us.correoElectronico)).FirstOrDefault();
            if (usuario != null)
            {
                List<Pedido> pedidos = db.Pedido.Where(x => x.idUsuario == usuario.idUsuario && usuario.estado==true).ToList();
                return PartialView("_PedidosUsuario", pedidos);
            }
            else
            {
                TempData["Mensaje"] = "El correo electrónico no se encuentra registrado";
                return RedirectToAction("PedidosUsuario");
            }
        }


    }
}