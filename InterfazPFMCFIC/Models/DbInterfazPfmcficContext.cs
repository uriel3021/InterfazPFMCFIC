using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterfazPFMCFIC.Models;

public partial class DbInterfazPfmcficContext : DbContext
{
    public DbInterfazPfmcficContext()
    {
    }

    public DbInterfazPfmcficContext(DbContextOptions<DbInterfazPfmcficContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InterfazPfmCficArchivo> InterfazPfmCficArchivos { get; set; }

    public virtual DbSet<InterfazPfmCficCancelacione> InterfazPfmCficCancelaciones { get; set; }

    public virtual DbSet<InterfazPfmCficCatCodigoRetorno> InterfazPfmCficCatCodigoRetornos { get; set; }

    public virtual DbSet<InterfazPfmCficCatEstatusSolicitud> InterfazPfmCficCatEstatusSolicituds { get; set; }

    public virtual DbSet<InterfazPfmCficCatTipoConfirmacion> InterfazPfmCficCatTipoConfirmacions { get; set; }

    public virtual DbSet<InterfazPfmCficCatTipoProducto> InterfazPfmCficCatTipoProductos { get; set; }

    public virtual DbSet<InterfazPfmCficConfirmacionEnvio> InterfazPfmCficConfirmacionEnvios { get; set; }

    public virtual DbSet<InterfazPfmCficPlantilla> InterfazPfmCficPlantillas { get; set; }

    public virtual DbSet<InterfazPfmCficProductorecibido> InterfazPfmCficProductorecibidos { get; set; }

    public virtual DbSet<InterfazPfmCficRechazo> InterfazPfmCficRechazos { get; set; }

    public virtual DbSet<InterfazPfmCficSolicitud> InterfazPfmCficSolicituds { get; set; }

    public virtual DbSet<InterfazPfmPfmCficConfirmacionRecepcion> InterfazPfmPfmCficConfirmacionRecepcions { get; set; }

    public virtual DbSet<MotivoRechazo> MotivoRechazos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuración se hace desde Program.cs usando appsettings.json
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterfazPfmCficArchivo>(entity =>
        {
            entity.HasKey(e => e.ArchivoId)
                .HasName("PK__INTERFAZ__3D24276A7FD7C9C3")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_ARCHIVO");

            entity.Property(e => e.ArchivoId).HasColumnName("ArchivoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PesoArchivoKb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PesoArchivoKB");
            entity.Property(e => e.ProcesoId).HasColumnName("ProcesoID");
            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");
            entity.Property(e => e.Ruta)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficCancelacione>(entity =>
        {
            entity.HasKey(e => e.CancelacionId)
                .HasName("PK__INTERFAZ__5A8447EE5E201ADC")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_CANCELACIONES");

            entity.Property(e => e.CancelacionId).HasColumnName("CancelacionID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaCancelacion).HasColumnType("datetime");
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");

            entity.HasOne(d => d.SolictudPfmcfic).WithMany(p => p.InterfazPfmCficCancelaciones)
                .HasForeignKey(d => d.SolictudPfmcficid)
                .HasConstraintName("FK_INTERFAZ_PFM_CFIC_CANCELACIONES_INTERFAZ_PFM_CFIC_SOLICITUD");
        });

        modelBuilder.Entity<InterfazPfmCficCatCodigoRetorno>(entity =>
        {
            entity.HasKey(e => e.CatCodigoRetornoId).HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_CatCodigoRetorno");

            entity.Property(e => e.CatCodigoRetornoId)
                .ValueGeneratedNever()
                .HasColumnName("CatCodigoRetornoID");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<InterfazPfmCficCatEstatusSolicitud>(entity =>
        {
            entity.HasKey(e => e.CatEstatusSolicitudId).HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_CatEstatusSolicitud");

            entity.Property(e => e.CatEstatusSolicitudId)
                .ValueGeneratedNever()
                .HasColumnName("CatEstatusSolicitudID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<InterfazPfmCficCatTipoConfirmacion>(entity =>
        {
            entity.HasKey(e => e.CatTipoConfirmacionId).HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_CatTipoConfirmacion");

            entity.Property(e => e.CatTipoConfirmacionId)
                .ValueGeneratedNever()
                .HasColumnName("CatTipoConfirmacionID");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<InterfazPfmCficCatTipoProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("INTERFAZ_PFM_CFIC_CatTipoProducto");

            entity.Property(e => e.CatTipoProductoId).HasColumnName("CatTipoProductoID");
            entity.Property(e => e.Pfmcfic).HasColumnName("PFMCFIC");
        });

        modelBuilder.Entity<InterfazPfmCficConfirmacionEnvio>(entity =>
        {
            entity.HasKey(e => e.ConfirmacionId)
                .HasName("PK__INTERFAZ__637503903EAD6EE9")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO");

            entity.Property(e => e.ConfirmacionId).HasColumnName("ConfirmacionID");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FolioConfirmacionCfic).HasColumnName("FolioConfirmacionCFIC");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");

            entity.HasOne(d => d.SolictudPfmcfic).WithMany(p => p.InterfazPfmCficConfirmacionEnvios)
                .HasForeignKey(d => d.SolictudPfmcficid)
                .HasConstraintName("FK_INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO_INTERFAZ_PFM_CFIC_SOLICITUD");
        });

        modelBuilder.Entity<InterfazPfmCficPlantilla>(entity =>
        {
            entity.HasKey(e => e.PlantillaId)
                .HasName("PK__INTERFAZ__C5DEB58C9A287233")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_PLANTILLAS");

            entity.Property(e => e.PlantillaId).HasColumnName("PlantillaID");
            entity.Property(e => e.CatTipoProductoId).HasColumnName("CatTipoProductoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficProductorecibido>(entity =>
        {
            entity.HasKey(e => e.ProductoRecibidoId).HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_PRODUCTORECIBIDO");

            entity.Property(e => e.ProductoRecibidoId).HasColumnName("ProductoRecibidoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaProducto).HasColumnType("datetime");
            entity.Property(e => e.GeneralesAntecedentesId).HasColumnName("GeneralesAntecedentesID");
            entity.Property(e => e.SolicitudPfmcficid).HasColumnName("SolicitudPFMCFICID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficRechazo>(entity =>
        {
            entity.HasKey(e => e.RechazoId)
                .HasName("PK__INTERFAZ__EEE56439E97E3F10")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_RECHAZOS");

            entity.Property(e => e.RechazoId).HasColumnName("RechazoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.MotivoRechazoId).HasColumnName("MotivoRechazoID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
            entity.Property(e => e.TipoRechazoId).HasColumnName("TipoRechazoID");


            entity.HasOne(d => d.SolictudPfmcfic).WithMany(p => p.InterfazPfmCficRechazos)
                .HasForeignKey(d => d.SolictudPfmcficid)
                .HasConstraintName("FK_INTERFAZ_PFM_CFIC_RECHAZOS_INTERFAZ_PFM_CFIC_SOLICITUD");
        });

        modelBuilder.Entity<InterfazPfmCficSolicitud>(entity =>
        {
            entity.HasKey(e => e.SolicitudPfmcficid)
                .HasName("PK__INTERFAZ__3411BB0B5B5A8B25")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_SOLICITUD");

            entity.Property(e => e.SolicitudPfmcficid).HasColumnName("SolicitudPFMCFICID");
            entity.Property(e => e.ActosApfmid).HasColumnName("ActosAPFMID");
            entity.Property(e => e.AdscripcionId).HasColumnName("AdscripcionID");
            entity.Property(e => e.CatEstatusSolicitudId).HasColumnName("CatEstatusSolicitudID");
            entity.Property(e => e.CatTipoEnvioId).HasColumnName("CatTipoEnvioID");
            entity.Property(e => e.CatTipoMandamientoId).HasColumnName("CatTipoMandamientoID");
            entity.Property(e => e.CatTipoProductoId).HasColumnName("CatTipoProductoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaOficio).HasColumnType("datetime");
            entity.Property(e => e.FechaSistema).HasColumnType("datetime");
            entity.Property(e => e.FechaTermino).HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Oficio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PersonalId).HasColumnName("PersonalID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmPfmCficConfirmacionRecepcion>(entity =>
        {
            entity.HasKey(e => e.ConfirmacionId)
                .HasName("PK__INTERFAZ__6375039090C53A46")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION");

            entity.Property(e => e.ConfirmacionId).HasColumnName("ConfirmacionID");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FolioConfirmacionPfm).HasColumnName("FolioConfirmacionPFM");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");

            entity.HasOne(d => d.SolictudPfmcfic).WithMany(p => p.InterfazPfmPfmCficConfirmacionRecepcions)
                .HasForeignKey(d => d.SolictudPfmcficid)
                .HasConstraintName("FK_INTERFAZ_PFM_PFM_CFIC_CONFIRMACION_RECEPCION_INTERFAZ_PFM_CFIC_SOLICITUD");
        });

        modelBuilder.Entity<MotivoRechazo>(entity =>
        {
            entity.HasKey(e => e.MotivoRechazoId).HasName("PK_dbo.MotivoRechazoNueva");

            entity.ToTable("MotivoRechazo");

            entity.Property(e => e.Cenapi).HasColumnName("CENAPI");
            entity.Property(e => e.Cgsp).HasColumnName("CGSP");
            entity.Property(e => e.Motivo).HasMaxLength(200);
            entity.Property(e => e.Pfm).HasColumnName("PFM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
