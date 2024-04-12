using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class KioscoContext : DbContext
{
    public KioscoContext()
    {
    }

    public KioscoContext(DbContextOptions<KioscoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detalleventa> Detalleventa { get; set; }

    public virtual DbSet<Oferta> Oferta { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Tipoproducto> Tipoproductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseMySql("server=127.0.0.1;port=3306;database=kiosco;user=root;password=mysql", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Cif, "cif_UNIQUE").IsUnique();

            entity.HasIndex(e => e.OfertaId, "fk_cliente_oferta1_idx");

            entity.HasIndex(e => e.PersonaId, "fk_cliente_persona1_idx");

            entity.HasIndex(e => e.Idcliente, "idcliente_UNIQUE").IsUnique();

            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Cif)
                .HasMaxLength(20)
                .HasColumnName("cif");
            entity.Property(e => e.OfertaId).HasColumnName("oferta_id");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");

            entity.HasOne(d => d.Oferta).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.OfertaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente_oferta1");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente_persona1");
        });

        modelBuilder.Entity<Detalleventa>(entity =>
        {
            entity.HasKey(e => new { e.VentaId, e.ProductoId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("detalleventa");

            entity.HasIndex(e => e.ProductoId, "fk_detalleventa_producto1_idx");

            entity.HasIndex(e => e.VentaId, "fk_detalleventa_venta1_idx");

            entity.Property(e => e.VentaId).HasColumnName("venta_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalleventa_producto1");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalleventa_venta1");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Idoferta).HasName("PRIMARY");

            entity.ToTable("oferta");

            entity.HasIndex(e => e.Idoferta, "idoferta_UNIQUE").IsUnique();

            entity.Property(e => e.Idoferta).HasColumnName("idoferta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Fichero)
                .HasMaxLength(15)
                .HasColumnName("fichero");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso).HasName("PRIMARY");

            entity.ToTable("permiso");

            entity.HasIndex(e => e.Idpermiso, "idpermiso_UNIQUE").IsUnique();

            entity.Property(e => e.Idpermiso).HasColumnName("idpermiso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");

            entity.HasMany(d => d.Rols).WithMany(p => p.Permisos)
                .UsingEntity<Dictionary<string, object>>(
                    "Tiene",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_permiso_has_rol_rol1"),
                    l => l.HasOne<Permiso>().WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_permiso_has_rol_permiso1"),
                    j =>
                    {
                        j.HasKey("PermisoId", "RolId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("tiene");
                        j.HasIndex(new[] { "PermisoId" }, "fk_permiso_has_rol_permiso1_idx");
                        j.HasIndex(new[] { "RolId" }, "fk_permiso_has_rol_rol1_idx");
                        j.IndexerProperty<int>("PermisoId").HasColumnName("permiso_id");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                    });
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Idpersona).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Idpersona, "idpersona_UNIQUE").IsUnique();

            entity.Property(e => e.Idpersona).HasColumnName("idpersona");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(30)
                .HasColumnName("apellidos");
            entity.Property(e => e.CodPostal)
                .HasMaxLength(10)
                .HasColumnName("cod_postal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .HasColumnName("nombre");
            entity.Property(e => e.Poblacion)
                .HasMaxLength(25)
                .HasColumnName("poblacion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.OfertaId, "fk_producto_oferta_idx");

            entity.HasIndex(e => e.TipoproductoId, "fk_producto_tipoproducto1_idx");

            entity.HasIndex(e => e.Idproducto, "idproducto_UNIQUE").IsUnique();

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(25)
                .HasColumnName("imagen");
            entity.Property(e => e.Iva).HasColumnName("iva");
            entity.Property(e => e.OfertaId).HasColumnName("oferta_id");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.TipoproductoId).HasColumnName("tipoproducto_id");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(25)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.Oferta).WithMany(p => p.Productos)
                .HasForeignKey(d => d.OfertaId)
                .HasConstraintName("fk_producto_oferta");

            entity.HasOne(d => d.Tipoproducto).WithMany(p => p.Productos)
                .HasForeignKey(d => d.TipoproductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_producto_tipoproducto1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.HasIndex(e => e.Idrol, "idrol_UNIQUE").IsUnique();

            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(15)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Tipoproducto>(entity =>
        {
            entity.HasKey(e => e.Idtipoproducto).HasName("PRIMARY");

            entity.ToTable("tipoproducto");

            entity.HasIndex(e => e.Idtipoproducto, "idproducto_UNIQUE").IsUnique();

            entity.Property(e => e.Idtipoproducto).HasColumnName("idtipoproducto");
            entity.Property(e => e.Categoria)
                .HasMaxLength(25)
                .HasColumnName("categoria");
            entity.Property(e => e.Imagen)
                .HasMaxLength(15)
                .HasColumnName("imagen");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.PersonaId, "fk_usuario_persona1");

            entity.HasIndex(e => e.RolId, "fk_usuario_rol1_idx");

            entity.HasIndex(e => e.Idusuario, "idusuario_UNIQUE").IsUnique();

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");

            entity.HasOne(d => d.Persona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("fk_usuario_persona1");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol1");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.ClienteId, "fk_venta_cliente1_idx");

            entity.HasIndex(e => e.UsuarioId, "fk_venta_usuario1_idx");

            entity.HasIndex(e => e.Idventa, "idventa_UNIQUE").IsUnique();

            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(45)
                .HasColumnName("mensaje");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_venta_cliente1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Venta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_venta_usuario1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
