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

        public int cantidad { get; set; }

        public decimal descuento { get; set; }

        public virtual Cupones Cupones { get; set; }

        public virtual Pedido Pedido { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
