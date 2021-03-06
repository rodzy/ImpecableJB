﻿using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class DetallePedidoController : Controller
    {

        ImpecableContext db = new ImpecableContext();

        // GET: DetallePedido
        /// <summary>
        /// Método que carga los detalles de el pedido selecciondo por el usuario
        /// </summary>
        /// <param name="id">Identificador del pedido</param>
        /// <returns></returns>
        public ActionResult MuestraDetalle(int? id)
        {
            if (id == null)
            {
                TempData["Mensaje"] = "El pedido no se encuentra registrado";
                return RedirectToAction("ListaPedidos","Pedido");
            }
            return View(db.Detalle_Pedido.Where(x => x.idPedido == id).ToList());
        }


    }
}