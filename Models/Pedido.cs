namespace ImpecableJB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pedido")]
    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            Detalle_Pedido = new HashSet<Detalle_Pedido>();
        }

        [Key]
        [Display(Name ="Código del pedido")]
        public int idPedido { get; set; }

        [Display(Name ="Código del usuario")]
        public int idUsuario { get; set; }

        [Display(Name ="Fecha de la compra")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_hora { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "₡ {0:#}")]
        public decimal total { get; set; }

        [Display(Name ="Estado del pedido")]
        public bool? estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Pedido> Detalle_Pedido { get; set; }

        [Display(Name ="Usuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
