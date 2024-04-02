using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pr18;

public partial class Db18Context : DbContext
{
    public Db18Context()
    {
    }

    public Db18Context(DbContextOptions<Db18Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Tbl> Tbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=db18; User=исп-31;Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl__3213E83FA9B8D09F");

            entity.ToTable("Tbl");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdresOfFabric)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameOfFabric)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameOfL)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
