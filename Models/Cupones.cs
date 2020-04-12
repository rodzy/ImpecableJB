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
        [Display(Name ="Producto aplicable")]
        [Required(ErrorMessage ="Seleccione un producto")]
        public int idProducto { get; set; }

        [Display(Name ="Rango que aplica")]
        [Required(ErrorMessage ="Seleccione un rango aplicable")]
        public int idNivel { get; set; }

        [Required(ErrorMessage ="El titular del cupón es requerido")]
        [StringLength(50)]
        [Display(Name ="Titular cupón")]
        public string titulo { get; set; }

        [Required(ErrorMessage ="La descripción del cupón es requerida")]
        [Display(Name="Descripción")]
        public string descripcion { get; set; }

        [Display(Name ="Promoción")]
        [DisplayFormat(DataFormatString ="{0:#}%")]
        [Required(ErrorMessage ="La promoción es requerida")]
        [RegularExpression("[0-9]*",ErrorMessage ="La promoción solo acepta números")]
        public decimal promocion { get; set; }

        [Display(Name ="Estado")]
        [UIHint("Activo")]
        public bool? estado { get; set; }

        [Display(Name="Producto aplicado")]
        public virtual Producto Producto { get; set; }

        [Display(Name="Rango")]
        public virtual Nivel Nivel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }
    }
}
