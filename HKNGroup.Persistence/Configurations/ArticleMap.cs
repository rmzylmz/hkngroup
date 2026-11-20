using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); 
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
             builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.ShortContent).IsRequired();
            builder.Property(x => x.ShortContent).HasMaxLength(400);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Thumbnail).HasMaxLength(250);
            builder.Property(x => x.ViewsCount).IsRequired();
       
            builder.HasOne<ArticleCategory>(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId);
            builder.ToTable("Articles");
        }
    }
}
