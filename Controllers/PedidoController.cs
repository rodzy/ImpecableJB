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
        public ActionResult Index()
        {
            return View();
        }
        

    }
}