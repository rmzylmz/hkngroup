using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ArticleCommentMap : IEntityTypeConfiguration<ArticleComment>
    {
        public void Configure(EntityTypeBuilder<ArticleComment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); 
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired(); 
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(25);
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(20);
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.Text).HasMaxLength(512);
            builder.HasOne<Article>(x => x.Article).WithMany(x => x.Comments).HasForeignKey(x => x.ArticleId);
            builder.ToTable("ArticleComments");
         }
    }
}
