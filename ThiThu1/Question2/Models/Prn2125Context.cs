using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Question2.Models;

public partial class Prn2125Context : DbContext
{
    public Prn2125Context()
    {
    }

    public Prn2125Context(DbContextOptions<Prn2125Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Kitchen> Kitchens { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = PRN212_5; uid=sa;pwd=123;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BBAD6BA9A");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Kitchen>(entity =>
        {
            entity.HasKey(e => e.KitchenId).HasName("PK__Kitchens__8A856CC1C3D390BA");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.KitchenName).HasMaxLength(100);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("PK__MenuItem__8943F72283C222E3");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItems_Categories");

            entity.HasOne(d => d.Kitchen).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.KitchenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItems_Kitchens");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
