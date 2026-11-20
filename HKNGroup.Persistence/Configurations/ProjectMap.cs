using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();      
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();           
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.ShortContent).HasMaxLength(800);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.SeoDescription).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.SeoTags).HasColumnType("NVARCHAR(MAX)");
            builder.HasOne<ProjectCategory>(x => x.Category).WithMany(x => x.Projects).HasForeignKey(x => x.ProjectCategoryId);
            builder.ToTable("Projects");
           
        }
    }
}
