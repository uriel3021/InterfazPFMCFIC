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

    public virtual DbSet<CatMotivoRechazo> CatMotivoRechazos { get; set; }

    public virtual DbSet<InterfazPfmCficArchivo> InterfazPfmCficArchivos { get; set; }

    public virtual DbSet<InterfazPfmCficCancelacione> InterfazPfmCficCancelaciones { get; set; }

    public virtual DbSet<InterfazPfmCficConfirmacionEnvio> InterfazPfmCficConfirmacionEnvios { get; set; }

    public virtual DbSet<InterfazPfmCficPlantilla> InterfazPfmCficPlantillas { get; set; }

    public virtual DbSet<InterfazPfmCficProductorecibido> InterfazPfmCficProductorecibidos { get; set; }

    public virtual DbSet<InterfazPfmCficRechazo> InterfazPfmCficRechazos { get; set; }

    public virtual DbSet<InterfazPfmCficSolicitud> InterfazPfmCficSolicituds { get; set; }

    public virtual DbSet<InterfazPfmPfmCficConfirmacionRecepcion> InterfazPfmPfmCficConfirmacionRecepcions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Db_InterfazPFMCFIC;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatMotivoRechazo>(entity =>
        {
            entity.HasKey(e => e.MotivoRechaId).HasName("PK__CatMotiv__94DBA2E7DA43BD7E");

            entity.ToTable("CatMotivoRechazo");

            entity.Property(e => e.Cenapi).HasColumnName("CENAPI");
            entity.Property(e => e.Cgsp).HasColumnName("CGSP");
            entity.Property(e => e.Pfm).HasColumnName("PFM");
        });

        modelBuilder.Entity<InterfazPfmCficArchivo>(entity =>
        {
            entity.HasKey(e => e.ArchivoId)
                .HasName("PK__INTERFAZ__3D24276A103985C2")
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
                .HasName("PK__INTERFAZ__5A8447EED1F56CD4")
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

        modelBuilder.Entity<InterfazPfmCficConfirmacionEnvio>(entity =>
        {
            entity.HasKey(e => e.ConfirmacionId)
                .HasName("PK__INTERFAZ__63750390844B40E9")
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
                .HasName("PK__INTERFAZ__C5DEB58C86D1F7B2")
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
                .HasName("PK__INTERFAZ__EEE564390E41E2CD")
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
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");

            entity.HasOne(d => d.SolictudPfmcfic).WithMany(p => p.InterfazPfmCficRechazos)
                .HasForeignKey(d => d.SolictudPfmcficid)
                .HasConstraintName("FK_INTERFAZ_PFM_CFIC_RECHAZOS_INTERFAZ_PFM_CFIC_SOLICITUD");
        });

        modelBuilder.Entity<InterfazPfmCficSolicitud>(entity =>
        {
            entity.HasKey(e => e.SolicitudPfmcficid)
                .HasName("PK__INTERFAZ__3411BB0B65FDDCD1")
                .HasFillFactor(80);

            entity.ToTable("INTERFAZ_PFM_CFIC_SOLICITUD");

            entity.Property(e => e.SolicitudPfmcficid).HasColumnName("SolicitudPFMCFICID");
            entity.Property(e => e.ActoId).HasColumnName("ActoID");
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
                .HasName("PK__INTERFAZ__63750390F9478C23")
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
