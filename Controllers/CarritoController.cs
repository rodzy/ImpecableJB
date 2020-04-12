using ImpecableJB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpecableJB.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        private ImpecableContext Context = new ImpecableContext();
        /// <summary>
        /// Action Result para ingresar un producto al carrito, sea nuevo o recurrente
        /// Implementando la lógica de ir aumentando la cantidad
        /// Cuando el método GetIndex(id) devuelve un -1 quiere decir que no encontró el item 
        /// dentro del carrito, de lo contrario aumenta la cantidad del producto
        /// </summary>
        /// <param name="id">Identificador del producto escogido</param>
        /// <returns></returns>
        public ActionResult AgregarCarrito(int id)
        {
            //Agrega por primeera vez un producto al carrito
            if (Session["Carrito"] == null)
            {
                List<CarritoItem> CarritoNuevo = new List<CarritoItem>();
                CarritoNuevo.Add(new CarritoItem(Context.Producto.Find(id), 1));
                Session["Carrito"] = CarritoNuevo;
            }
            else
            {
                List<CarritoItem> CarritoRecurrente = (List<CarritoItem>)Session["Carrito"];
                int index = GetIndex(id);
                if (index==-1)
                {
                    CarritoRecurrente.Add(new CarritoItem(Context.Producto.Find(id), 1));
                    Session["Carrito"] = CarritoRecurrente;
                }
                else
                {
                    CarritoRecurrente[index].Cantidad++;
                    Session["Carrito"] = CarritoRecurrente;
                }
            }
            TempData["Mensaje"] = "Añadido al carrito";
            return View("CarritoPrevia");
        }
        /// <summary>
        /// Redirecciona a la página de la visualización del carrito
        /// </summary>
        /// <returns></returns>
        public ActionResult CarritoPrevia()
        {
            return View();
        }
        /// <summary>
        /// Método que realiza la busqueda dentro de la lista del carrito para
        /// realizar la suma en cantidades
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIndex(int id)
        {
            List<CarritoItem> car = (List<CarritoItem>)Session["Carrito"];
            for (int i = 0; i < car.Count; i++)
            {
                if (car[i].Producto.idProducto == id)
                {
                    return i;
                }             
            }
            return -1;
        }
        /// <summary>
        /// ActionResult que elimina un elemento seleccionado del carrito 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EliminarElemento(int id)
        {
            List<CarritoItem> carritoItems = (List<CarritoItem>)Session["Carrito"];
            carritoItems.RemoveAt(GetIndex(id));
            return View("CarritoPrevia");
        }

        /// <summary>
        /// ActionResult que resta una cantidad al elemento buscado por el ID definido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DisminuirCantidad(int id)
        {
            List<CarritoItem> CarritoRecurrente = (List<CarritoItem>)Session["Carrito"];
            int index = GetIndex(id);
            if (index != -1)
            { 
                if (CarritoRecurrente[index].Cantidad != 0)
                {
                    CarritoRecurrente[index].Cantidad--;
                    Session["Carrito"] = CarritoRecurrente;
                }
                else
                {
                    EliminarElemento(id);
                }
            }       
            return View("CarritoPrevia");
        }

        /// <summary>
        /// Metodo para confirmar el pago
        /// Introduce en la base de datos el pedido y el detalle de pedido
        /// Aplica los calculos por si aplica descuentos con un cupon
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmarPago()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioClientes", "Usuario");
            }
            else
            {
                Pedido pedido = new Pedido();
                Detalle_Pedido detalle_Pedido = new Detalle_Pedido();

                decimal total = 0;
                
                foreach(var item in Session["Carrito"] as List<ImpecableJB.Models.CarritoItem>)
                {
                    Cupones cupones = Context.Cupones.Where(x => x.idProducto == item.Producto.idProducto && x.idNivel==Convert.ToInt32(Session["Rango"])).FirstOrDefault();
                    if (cupones != null)
                    {
                        total += item.Producto.precio * item.Cantidad / Decimal.Multiply(cupones.promocion, Convert.ToDecimal(0.10));
                        pedido.idUsuario = Convert.ToInt32(Session["Usuario"]);
                        pedido.fecha_hora = DateTime.Now;
                        pedido.total = total;
                        Context.Pedido.Add(pedido);
                        Context.SaveChanges();
                    }
                    else
                    {
                        total += item.Producto.precio * item.Cantidad;
                        pedido.idUsuario = Convert.ToInt32(Session["Usuario"]);
                        pedido.fecha_hora = DateTime.Now;
                        pedido.total = total;
                        Context.Pedido.Add(pedido);
                        Context.SaveChanges();
                    }
                    detalle_Pedido.idPedido = pedido.idPedido;
                    detalle_Pedido.idProducto = item.Producto.idProducto;
                    detalle_Pedido.idCupones = cupones.idCupones;
                    detalle_Pedido.cantidad = item.Cantidad;
                    detalle_Pedido.descuento = cupones.promocion;
                    Context.Detalle_Pedido.Add(detalle_Pedido);
                    Context.SaveChanges();

                    //TODO: Testing y retoque
                }
                
            }
            return View();
        }
    }
}