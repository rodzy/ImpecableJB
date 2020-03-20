namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cupones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cupones()
        {
            Detalle_Pedido = new HashSet<Detalle_Pedido>();
        }

        [Key]
        public int idCupones { get; set; }

        public int idProducto { get; set; }

        public int idNivel { get; set; }

        [Required]
        [StringLength(50)]
        public string titulo { get; set; }

        [Required]
        public string descripcion { get; set; }

        public decimal promocion { get; set; }

        public bool? estado { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Nivel Nivel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }
    }
}
