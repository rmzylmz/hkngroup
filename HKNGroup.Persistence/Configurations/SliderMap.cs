using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class SliderMap : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();                   
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();           
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Description2).HasColumnType("NVARCHAR(MAX)");
            builder.ToTable("Sliders");
        }
    }
}
