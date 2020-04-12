using ImpecableJB.Models;
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
        public ActionResult Index()
        {
            return View();
        }


    }
}