using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CSVAPI.Models;

public partial class CSVDbContext : DbContext, ICSVDbContext
{
    public CSVDbContext()
    {
    }

    public CSVDbContext(DbContextOptions<CSVDbContext> options)
        : base(options)
    {
    }

    public  DbSet<Column> Columns { get; set; }

    public  DbSet<ColumnValue> ColumnValues { get; set; }

    public  DbSet<Company> Companies { get; set; }
    public DbSet<QueryResult> QueryResult { get; set; }
    public DbSet<ExecuteResult> ExecuteResult { get; set; }

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
        modelBuilder.Entity<QueryResult>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<ExecuteResult>(entity =>
        {
            entity.HasNoKey();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
