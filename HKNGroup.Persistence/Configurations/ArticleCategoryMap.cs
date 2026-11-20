using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ArticleCategoryMap : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); 
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired(); 
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CategoryFA).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(150);
            builder.ToTable("ArticleCategories");
           
        }
    }
}
