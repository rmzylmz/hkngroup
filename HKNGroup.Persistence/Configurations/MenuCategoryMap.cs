using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class MenuCategoryMap : IEntityTypeConfiguration<Menus>
    {
        public void Configure(EntityTypeBuilder<Menus> builder)
        {
            builder.ToTable("Menus", @"dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); 
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(350);
            builder.Property(x => x.LinkPage).HasMaxLength(300);
             builder.Property(x => x.MenuSeo).HasMaxLength(500); 

            builder.Property(x => x.IsActive)
               .HasColumnName("IsActive")
               .HasDefaultValueSql("0")
               .IsRequired();

            builder.Property(x => x.IsDeleted)
              .HasColumnName("IsDeleted")
              .HasDefaultValueSql("0")
              .IsRequired();

        }
    }
}
