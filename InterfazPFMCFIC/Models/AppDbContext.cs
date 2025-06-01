using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterfazPFMCFIC.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InterfazPfmCficArchivo> InterfazPfmCficArchivos { get; set; }

    public virtual DbSet<InterfazPfmCficCancelacione> InterfazPfmCficCancelaciones { get; set; }

    public virtual DbSet<InterfazPfmCficConfirmacionEnvio> InterfazPfmCficConfirmacionEnvios { get; set; }

    public virtual DbSet<InterfazPfmCficConfirmacionRecepcion> InterfazPfmCficConfirmacionRecepcions { get; set; }

    public virtual DbSet<InterfazPfmCficDelito> InterfazPfmCficDelitos { get; set; }

    public virtual DbSet<InterfazPfmCficRechazo> InterfazPfmCficRechazos { get; set; }

    public virtual DbSet<InterfazPfmCficSolicitud> InterfazPfmCficSolicituds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Db_InterfazPFMCFIC;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterfazPfmCficArchivo>(entity =>
        {
            entity.HasKey(e => e.ArchivoId).HasName("PK__INTERFAZ__3D24276AC73DF444");

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
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficCancelacione>(entity =>
        {
            entity.HasKey(e => e.CancelacionId).HasName("PK__INTERFAZ__5A8447EE78A41CCE");

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
        });

        modelBuilder.Entity<InterfazPfmCficConfirmacionEnvio>(entity =>
        {
            entity.HasKey(e => e.ConfirmacionId).HasName("PK__INTERFAZ__63750390203B712D");

            entity.ToTable("INTERFAZ_PFM_CFIC_CONFIRMACION_ENVIO");

            entity.Property(e => e.ConfirmacionId).HasColumnName("ConfirmacionID");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FolioConfirmacionCfic).HasColumnName("FolioConfirmacionCFIC");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
        });

        modelBuilder.Entity<InterfazPfmCficConfirmacionRecepcion>(entity =>
        {
            entity.HasKey(e => e.ConfirmacionId).HasName("PK__INTERFAZ__6375039068AF802B");

            entity.ToTable("INTERFAZ_PFM_CFIC_CONFIRMACION_RECEPCION");

            entity.Property(e => e.ConfirmacionId).HasColumnName("ConfirmacionID");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FolioConfirmacionPfm).HasColumnName("FolioConfirmacionPFM");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
        });

        modelBuilder.Entity<InterfazPfmCficDelito>(entity =>
        {
            entity.HasKey(e => e.DelitoCficpfmid).HasName("PK__INTERFAZ__4B93A22E6FB40692");

            entity.ToTable("INTERFAZ_PFM_CFIC_DELITOS");

            entity.Property(e => e.DelitoCficpfmid).HasColumnName("DelitoCFICPFMID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficRechazo>(entity =>
        {
            entity.HasKey(e => e.RechazoId).HasName("PK__INTERFAZ__EEE56439DFC6BE52");

            entity.ToTable("INTERFAZ_PFM_CFIC_RECHAZOS");

            entity.Property(e => e.RechazoId).HasColumnName("RechazoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.MotivoRechazoId).HasColumnName("MotivoRechazoID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
            entity.Property(e => e.TipoRechazoId).HasColumnName("TipoRechazoID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<InterfazPfmCficSolicitud>(entity =>
        {
            entity.HasKey(e => e.SolictudPfmcficid).HasName("PK__INTERFAZ__0E3EE6639BD998FC");

            entity.ToTable("INTERFAZ_PFM_CFIC_SOLICITUD");

            entity.Property(e => e.SolictudPfmcficid).HasColumnName("SolictudPFMCFICID");
            entity.Property(e => e.ActoId).HasColumnName("ActoID");
            entity.Property(e => e.AnioFolioSiga).HasColumnName("AnioFolioSIGA");
            entity.Property(e => e.CatEstatusSolicitudId).HasColumnName("CatEstatusSolicitudID");
            entity.Property(e => e.CatTipoProductoId).HasColumnName("CatTipoProductoID");
            entity.Property(e => e.FechaActualizacionDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaAltaDelta).HasColumnType("datetime");
            entity.Property(e => e.FechaOficio).HasColumnType("datetime");
            entity.Property(e => e.FechaRecepcion).HasColumnType("datetime");
            entity.Property(e => e.FechaTermino).HasColumnType("datetime");
            entity.Property(e => e.NombreAutoridad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExpediente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroFolioSiga).HasColumnName("NumeroFolioSIGA");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.OficioRemision)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SoliictudIdbts).HasColumnName("SoliictudIDBTS");
            entity.Property(e => e.SubAreaOrigenId).HasColumnName("SubAreaOrigenID");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
