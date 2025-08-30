using System;
using System.Collections.Generic;
using Exam_CA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam_CA.Infraestructure.Data;

public partial class DbSeg : DbContext
{
    public DbSeg()
    {
    }

    public DbSeg(DbContextOptions<DbSeg> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_seg;User ID=sa;Password=M4n0h42012;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Idcuenta);

            entity.Property(e => e.Idcuenta).HasColumnName("idcuenta");
            entity.Property(e => e.Estatus)
                .HasDefaultValue(true)
                .HasColumnName("estatus");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Idtipo).HasColumnName("idtipo");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldo");

            entity.HasOne(d => d.IdtipoNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.Idtipo)
                .HasConstraintName("FK_Cuenta_Tipo");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK_Cuenta_Usuario");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Idtipo);

            entity.ToTable("Tipo");

            entity.Property(e => e.Idtipo).HasColumnName("idtipo");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.DescTipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("desc_tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
