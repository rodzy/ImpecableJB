namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producto")]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            Cupones = new HashSet<Cupones>();
            Detalle_Pedido = new HashSet<Detalle_Pedido>();
        }

        [Key]
        public int idProducto { get; set; }

        public int idTipoProducto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Nombre producto")]
        public string nombre { get; set; }

        [Required]
        [Display(Name ="Descripción")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }

        [Display(Name ="Referencia")]
        public string imagen { get; set; }

        [DisplayFormat(DataFormatString = "₡{0:#}")]
        [Display(Name ="Precio unitario")]
        public decimal precio { get; set; }

        [Display(Name = "En stock")]
        public bool? estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cupones> Cupones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }

        [Display(Name ="Tipo de producto")]
        public virtual Tipo_Producto Tipo_Producto { get; set; }
    }
}
