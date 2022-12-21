using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScaffoldSample.Models;

public partial class MenuCardsContext : DbContext
{
    public MenuCardsContext()
    {
    }

    public MenuCardsContext(DbContextOptions<MenuCardsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuCard> MenuCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MenuCards;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menus", "mc");

            entity.HasIndex(e => e.MenuCardId, "IX_Menus_MenuCardId");

            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Text).HasMaxLength(120);

            entity.HasOne(d => d.MenuCard).WithMany(p => p.Menus).HasForeignKey(d => d.MenuCardId);
        });

        modelBuilder.Entity<MenuCard>(entity =>
        {
            entity.ToTable("MenuCards", "mc");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
