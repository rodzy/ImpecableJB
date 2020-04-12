namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detalle_Pedido
    {
        [Key]
        public int idDetalle_Pedido { get; set; }

        public int idProducto { get; set; }

        public int idPedido { get; set; }

        public int idCupones { get; set; }

        [Display(Name ="Cantidad")]
        public int cantidad { get; set; }

        [Display(Name ="Descuento aplicado")]
        [DisplayFormat(DataFormatString = "%{0:#}")]
        public decimal descuento { get; set; }

        [Display(Name ="Cupones aplicados")]
        public virtual Cupones Cupones { get; set; }

        [Display(Name="Encabezado del pedido")]
        public virtual Pedido Pedido { get; set; }

        [Display(Name ="Productos")]
        public virtual Producto Producto { get; set; }
    }
}
