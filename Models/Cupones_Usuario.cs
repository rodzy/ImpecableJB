namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class Cupones_Usuario
    {
        public Cupones_Usuario()
        {
            
        }
        [Key,Column(Order =0)]
        [Required(ErrorMessage ="El cupón es requerido")]
        [Display(Name ="Código del cupón")]
        public int idCupones { get; set; }

        [Key,Column(Order =1)]
        [Required(ErrorMessage = "El usuario es requerido")]
        [Display(Name = "Código del usuario")]
        public int idUsuario { get; set; }

        [UIHint("EstadoCupon")]
        [Display(Name ="Estado del cupón")]
        public bool? estado { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Cupones Cupones { get; set; }
    }
}