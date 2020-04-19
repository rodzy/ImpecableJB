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
            if (Request.HttpMethod != "POST")
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
                    if (index == -1)
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
            }
            TempData["Mensaje"] = "Añadido al carrito";
            return RedirectToAction("MuestraProductos","Productos");
        }
        /// <summary>
        /// Redirecciona a la página de la visualización del carrito
        /// </summary>
        /// <returns></returns>
        public ActionResult CarritoPrevia()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
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
            if (car != null)
            {
                for (int i = 0; i < car.Count; i++)
                {
                    if (car[i].Producto.idProducto == id)
                    {
                        return i;
                    }
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
            if (carritoItems != null)
            {
                carritoItems.RemoveAt(GetIndex(id));
                if (carritoItems.Count == 0)
                {
                    Session["Carrito"] = null;
                }
            }
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
        /// Almacena los calculos del pedido en una session con el pedido construido para luego poder formalizar el detalle 
        /// Aplica los calculos por si aplica descuentos con un cupon
        /// </summary>
        /// <returns>PartialView para cargar los datos del pedido en la página del carrito antes de formalizar</returns>
        public ActionResult ConfirmarPedido()
        {
            Pedido pedido = new Pedido();
            decimal total = 0;
            decimal subtotal = 0;
            decimal descuento = 0;
            //Recorrer la lista de Session en donde se guardan los cupones seleccionados por el usaurio
            if (Session["Cupones"] != null)
            {
                foreach (var item in Session["Cupones"] as List<Cupones>)
                {
                    //Total en descuentos por cupones aplicados al final
                    descuento += item.promocion / 100;
                }
            }
            //Recorrer el carrito de compras con todos los items
            foreach(var item in Session["Carrito"] as List<CarritoItem>)
            {
                //Total en compras con descuentos aplicados si es que tiene descuentos
                //CONSULTAR CON LA PROFE: Si el descuento lo realizo por cada producto o es asignado al total
                if (descuento !=0)
                {                   
                    subtotal += (item.Producto.precio * item.Cantidad) * descuento;
                    total += (item.Producto.precio * item.Cantidad) - subtotal;
                }
                else
                {
                    total += item.Producto.precio * item.Cantidad;
                }              
            }
            if (Session["Usuario"] != null)
            {
                //Construcción del objeto pedido
                Session["Descuento"] = descuento;
                pedido.idUsuario = Convert.ToInt32(Session["Usuario"]);
                pedido.fecha_hora = DateTime.Now;
                pedido.total = total;
                pedido.estado = false;
                //Guardado en base de datos con el estado en false para que cuando se formaliza la compra se modifica a verdadero
                Context.Pedido.Add(pedido);
                Context.SaveChanges();
                return PartialView("_CarritoPedido", pedido);
            }
            else
            {
                TempData["Mensaje"] = "Para continuar con el pedido debes estár autenticado";
                return RedirectToAction("InicioClientes","Usuario");
            }
            
        }

        /// <summary>
        /// Realiza el proceso de insertar el detalle del pedido a las bases de datos 
        /// una vez que el usuario se encuentre registrado y con sesión activa
        /// de lo contrario será redireccionado al inicio de sesión
        /// </summary>
        /// <param name="id">Identificador del pedido que se está enviando</param>
        /// <returns></returns>
        public ActionResult FormalizarCompra(int? id)
        {         
            //Si el usuario no está logueado no puede continuar con la formalización
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioClientes", "Usuario");
            }
            else
            {
                if (id == null)
                {
                    TempData["Mensaje"] = "El pedido especificado no está disponible";
                    return View("CarritoPrevia");
                }
                //Encontrando el pedido para asociarlo
                Pedido pedido = Context.Pedido.Find(id);
                if (pedido == null)
                {
                    TempData["Mensaje"] = "El pedido no se encuantra reservado";
                    return View("CarritoPrevia");
                }
                //Recorriendo el carrito para llenar un detalle por cada producto que se quiere registrar
                foreach (var item in Session["Carrito"] as List<CarritoItem>)
                {
                    Detalle_Pedido detalle_Pedido = new Detalle_Pedido();         
                    detalle_Pedido.idPedido = pedido.idPedido;
                    detalle_Pedido.idProducto = item.Producto.idProducto;
                    detalle_Pedido.cantidad = item.Cantidad;
                    detalle_Pedido.descuento = Convert.ToDecimal(Session["Descuento"]);
                    Context.Detalle_Pedido.Add(detalle_Pedido);
                }
                Context.SaveChanges();
                //Cambiando el estado del pedido para colocarlo en procesado
                pedido.estado = true;
                Context.Entry(pedido).State = System.Data.Entity.EntityState.Modified;
                Context.SaveChanges();
                //Eliminando los elementos del carrito
                Session.Remove("Carrito");

                //Verificando las compras del usuario para la validación de rango
                VerificandoRango();

                //Mensaje de confirmación
                TempData["Mensaje"] = "Pedido realizado con éxito, gracias por su compra!";
                return RedirectToAction("MuestraProductos","Productos");
            }        
        }

        /// <summary>
        /// Método para verificar el rango del cliente
        /// </summary>
        public void VerificandoRango()
        {
            decimal total = 0;
            Usuario usuario = Context.Usuario.Find(Session["Usuario"]);
            List<Pedido> pedido = Context.Pedido.Where(x => x.idUsuario == Convert.ToInt32(Session["Usuario"])).ToList();
            foreach(var item in pedido)
            {
                total += item.total;
            }
            if (total >= 10000 && total<= 20000)
            {
                usuario.idNivel = 2;
            }
            if(total >= 20000 && total<=20000)
            {
                usuario.idNivel = 3;
            }
            if(total >= 40000 && total <= 40000)
            {
                usuario.idNivel = 4;
            }
            if (total > 60000)
            {
                usuario.idNivel = 5;
            }
            Context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();           
        }
        /*//Obteniendo la lista de cupones reservados
                    var cupones = Session["Cupones"] as List<Cupones>;
                    //Asignación del cupón usado a la respectiva compra
                    Cupones cup = null;
                    //Validación para saber si existen cupones asociados al producto dispuesto
                    if (cupones != null)
                    {
                        cup = cupones.Where(x => x.idProducto == item.Producto.idProducto).FirstOrDefault();
                    }    */
    }
}