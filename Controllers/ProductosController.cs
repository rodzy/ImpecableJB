using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class ProductosController : Controller
    {
        private ImpecableContext db = new ImpecableContext();
        // GET: Productos
        /// <summary>
        /// Método que devuelve la vista de los productos
        /// Realiza el cálculo del nivel de usuario cada vez que se haya realizado
        /// una compra sino no lo realiza
        /// </summary>
        /// <returns></returns>
        public ActionResult MuestraProductos()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            if (Session["Compra"]!=null && Session["Compra"].Equals("Finalizado"))
            {
                decimal total = 0;
                Usuario usuario = db.Usuario.Find(Session["Usuario"]);
                List<Pedido> pedido = db.Pedido.Where(x => x.idUsuario == usuario.idUsuario).ToList();

                foreach(var item in pedido)
                {
                    total += item.total;
                }
                //Verificando las compras del usuario para la validación de rango
                if (total >= 10000 && total <= 20000)
                {
                    usuario.idNivel = 2;
                }
                else
                {
                    if (total >= 20000 && total <= 40000)
                    {
                        usuario.idNivel = 3;
                    }
                    else
                    {

                        if (total >= 40000 && total <= 60000)
                        {
                            usuario.idNivel = 4;
                        }
                        else
                        {
                            if (total > 60000)
                            {
                                usuario.idNivel = 5;
                            }
                        }
                    }
                }

                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session.Remove("Compra");
                Session["ImagenRango"] = "~/Content/Rangos/" + usuario.Nivel.nombre + ".png";
            }
            return View(db.Producto.ToList());
        }

        /// <summary>
        /// Método que se apoya de PartialView para filtrar lista de los productos por la categoría seleccionada
        /// </summary>
        /// <param name="idTipo">Identificador de los tipos de producto</param>
        /// <returns></returns>
        public ActionResult FiltrarProductos(int idTipo)
        {
            return PartialView("_MuestraProducto", db.Producto.Where(x => x.idTipoProducto == idTipo).ToList());
        }
        
    }
}