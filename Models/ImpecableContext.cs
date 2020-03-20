namespace ImpecableJB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ImpecableContext : DbContext
    {
        public ImpecableContext()
            : base("name=ImpecableContext1")
        {
        }

        public virtual DbSet<Cupones> Cupones { get; set; }
        public virtual DbSet<Detalle_Pedido> Detalle_Pedido { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tipo_Producto> Tipo_Producto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cupones>()
                .Property(e => e.promocion)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Cupones>()
                .HasMany(e => e.Detalle_Pedido)
                .WithRequired(e => e.Cupones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nivel>()
                .HasMany(e => e.Cupones)
                .WithRequired(e => e.Nivel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Nivel>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Nivel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pedido>()
                .HasMany(e => e.Detalle_Pedido)
                .WithRequired(e => e.Pedido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Cupones)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Detalle_Pedido)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipo_Producto>()
                .HasMany(e => e.Producto)
                .WithRequired(e => e.Tipo_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Pedido)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
