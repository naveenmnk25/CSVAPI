using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CSVAPI.Models;

public partial class CSVDbContext : DbContext
{
    public CSVDbContext()
    {
    }

    public CSVDbContext(DbContextOptions<CSVDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<ColumnValue> ColumnValues { get; set; }

    public virtual DbSet<Company> Companies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dbo_Column");

            entity.ToTable("Column");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ColumnValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dbo_ColumnValues");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(100);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dbo_Company");

            entity.ToTable("Company");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
