using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MenusSamples
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus")
                .HasKey(m => m.MenuId);

            builder.Property(m => m.MenuId)
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Text)
                .HasMaxLength(120);

            builder.Property(m => m.Price)
                .HasColumnType("Money");

            builder.HasOne(m => m.MenuCard)
                .WithMany(c => c.Menus)
                .HasForeignKey(m => m.MenuCardId);
        }
    }
}