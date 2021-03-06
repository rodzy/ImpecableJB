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
            Cupones_Usuarios = new HashSet<Cupones_Usuario>();
        }

        [Key]
        public int idCupones { get; set; }
        [Display(Name ="Producto aplicable")]
        [Required(ErrorMessage ="Seleccione un producto")]
        public int idProducto { get; set; }

        [Display(Name ="Rango que aplica")]
        [Required(ErrorMessage ="Seleccione un rango aplicable")]
        public int idNivel { get; set; }

        [Required(ErrorMessage ="El titular del cup�n es requerido")]
        [StringLength(50)]
        [Display(Name ="Titular cup�n")]
        public string titulo { get; set; }

        [Required(ErrorMessage ="La descripci�n del cup�n es requerida")]
        [Display(Name="Descripci�n")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }

        [Display(Name ="Promoci�n")]
        [DisplayFormat(DataFormatString ="{0:#}%")]
        [Required(ErrorMessage ="La promoci�n es requerida")]
        [RegularExpression("[0-9]*",ErrorMessage ="La promoci�n solo acepta n�meros")]
        public decimal promocion { get; set; }

        [Display(Name ="Estado")]
        [UIHint("Activo")]
        public bool? estado { get; set; }

        [Display(Name="Producto aplicado")]
        public virtual Producto Producto { get; set; }

        [Display(Name="Rango")]
        public virtual Nivel Nivel { get; set; }

        public virtual ICollection<Cupones_Usuario> Cupones_Usuarios { get; set; }


    }
}
