using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DGA001.Models;

public partial class ImportacionContext : DbContext
{
    public ImportacionContext()
    {
    }

    public ImportacionContext(DbContextOptions<ImportacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Declaracion> Declaracions { get; set; }

    public virtual DbSet<Importacion> Importacions { get; set; }

    public virtual DbSet<Importador> Importadors { get; set; }

    public virtual DbSet<Impuesto> Impuestos { get; set; }

    public virtual DbSet<Transporte> Transportes { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=manuel\\sqlexpress01;Initial Catalog=DGA;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Declaracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Declarac__3214EC071E9FEB70");

            entity.ToTable("Declaracion");

            entity.Property(e => e.Colect)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Colecturia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConEmb)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Declara)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Manifiesto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDespacho)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Importador).WithMany(p => p.Declaracions)
                .HasForeignKey(d => d.ImportadorId)
                .HasConstraintName("FK__Declaraci__Impor__398D8EEE");
        });

        modelBuilder.Entity<Importacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Importac__3214EC076540A036");

            entity.ToTable("Importacion");

            entity.HasIndex(e => e.NumeroDeclaracion, "UQ__Importac__BA56066761781D62").IsUnique();

            entity.Property(e => e.NumeroDeclaracion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDespacho)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Importador).WithMany(p => p.Importacions)
                .HasForeignKey(d => d.ImportadorId)
                .HasConstraintName("FK__Importaci__Impor__45F365D3");

            entity.HasOne(d => d.Transporte).WithMany(p => p.Importacions)
                .HasForeignKey(d => d.TransporteId)
                .HasConstraintName("FK__Importaci__Trans__46E78A0C");
        });

        modelBuilder.Entity<Importador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Importad__3214EC07084C3561");

            entity.ToTable("Importador");

            entity.HasIndex(e => e.Rnc, "UQ__Importad__CAFF69504423750F").IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Regimen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rnc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RNC");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Impuesto__3214EC07DD843CF6");

            entity.Property(e => e.Flete).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FobUnit)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("FOB_Unit");
            entity.Property(e => e.Gravamen).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Itbis)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ITBIS");
            entity.Property(e => e.Otros).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioAlPorMenor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Seguro).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Selectivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalApagar)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalAPagar");
            entity.Property(e => e.ValorFob)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ValorFOB");
            entity.Property(e => e.Vcifbruto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VCIFBruto");
            entity.Property(e => e.Vcifneto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VCIFNeto");

            entity.HasOne(d => d.Declaracion).WithMany(p => p.Impuestos)
                .HasForeignKey(d => d.DeclaracionId)
                .HasConstraintName("FK__Impuestos__Decla__3F466844");
        });

        modelBuilder.Entity<Transporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transpor__3214EC0737BD7F81");

            entity.ToTable("Transporte");

            entity.Property(e => e.CodigoPuerto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Embarcador)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaisOrigen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaisOrigenIso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PaisOrigenISO");
            entity.Property(e => e.PaisProceso)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaisProcesoIso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PaisProcesoISO");
            entity.Property(e => e.Puerto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Declaracion).WithMany(p => p.Transportes)
                .HasForeignKey(d => d.DeclaracionId)
                .HasConstraintName("FK__Transport__Decla__4222D4EF");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehiculo__3214EC073326B8A1");

            entity.ToTable("Vehiculo");

            entity.Property(e => e.Chasis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CilindrajeCc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CilindrajeCC");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Motor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoVehiculo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Declaracion).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.DeclaracionId)
                .HasConstraintName("FK__Vehiculo__Declar__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
