using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OurServiceMap : IEntityTypeConfiguration<OurService>
    {
        public void Configure(EntityTypeBuilder<OurService> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();           
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired();
            builder.ToTable("OurServices");
           
        }
    }
}
