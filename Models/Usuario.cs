namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Pedido = new HashSet<Pedido>();
            Cupones_Usuarios = new HashSet<Cupones_Usuario>();
        }

        [Key]
        public int idUsuario { get; set; }

        public int idNivel { get; set; }

        public int idRol { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [Display(Name ="Nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Display(Name ="Primer Apellido")]
        [Required(ErrorMessage ="El primer apellido es requerido")]
        [StringLength(50)]
        public string apellido1 { get; set; }

        [Display(Name ="Segundo Apellido")]
        [StringLength(50)]
        public string apellido2 { get; set; }

        [Required(ErrorMessage = "El correo electr�nico es requerido")]
        [Display(Name ="Correo electr�nico")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",ErrorMessage ="El correo electr�nico no es v�lido")]
        public string correoElectronico { get; set; }

        [Display(Name ="Contrase�a")]
        [Required(ErrorMessage = "La contrase�a es requerida")]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        //[RegularExpression(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$", ErrorMessage = "Al menos una letra may�scula,una letra min�scula,un n�mero o caracter especial,m�nimo 8 caracteres.")]
        public string contrasena { get; set; }

        [Display(Name ="Estado")]
        [UIHint("Activo")]
        public bool? estado { get; set; }

        [Display(Name="Rango")]
        public virtual Nivel Nivel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedido { get; set; }

        [Display(Name ="Tipo de usuario")]
        public virtual Rol Rol { get; set; }

        public virtual ICollection<Cupones_Usuario> Cupones_Usuarios { get; set; }
    }
}
