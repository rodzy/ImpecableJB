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
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AgregarCarrito(int id)
        {
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
            return View("Recordar la vista del carrito");
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
            return View("Recordar la vista del carrito");
        }
    }
}