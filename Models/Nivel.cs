namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nivel")]
    public partial class Nivel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nivel()
        {
            Cupones = new HashSet<Cupones>();
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int idNivel { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Rango")]
        public string nombre { get; set; }

        [Required]
        [Display(Name ="Beneficios")]
        public string beneficios { get; set; }

        [Display(Name = "Estado")]
        [UIHint("Activo")]
        public bool? estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name ="Cupones")]
        public virtual ICollection<Cupones> Cupones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name ="Usuario")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
