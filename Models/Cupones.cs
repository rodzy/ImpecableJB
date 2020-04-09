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
        [Display(Name ="Titular cupón")]
        public string titulo { get; set; }

        [Required]
        [Display(Name="Descripción")]
        public string descripcion { get; set; }

        [Display(Name ="Promoción")]
        [DisplayFormat(DataFormatString ="%{0:#}")]
        public decimal promocion { get; set; }

        [Display(Name ="Estado")]
        public bool? estado { get; set; }

        [Display(Name="Producto aplicado")]
        public virtual Producto Producto { get; set; }

        [Display(Name="Rango")]
        public virtual Nivel Nivel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }
    }
}
