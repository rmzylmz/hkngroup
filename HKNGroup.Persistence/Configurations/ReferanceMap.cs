using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ReferanceMap : IEntityTypeConfiguration<Referance>
    {
        public void Configure(EntityTypeBuilder<Referance> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();                   
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();           
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.ToTable("Referances");
        }
    }
}
