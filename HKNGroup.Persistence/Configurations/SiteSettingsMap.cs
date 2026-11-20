using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class SiteSettingsMap : IEntityTypeConfiguration<SiteSetting>
    {
        public void Configure(EntityTypeBuilder<SiteSetting> builder)
        {
            builder.ToTable("SiteSettings", @"dbo");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();

            builder.HasData(new SiteSetting
            {
                Id = 1,
                SiteDefaultTitle = "demo",
                SiteDescription = "demo",
                SiteKeyword = "demo",
                SiteMail = "demo@demo.com",
                SiteMailPass = "1234",
                SiteOwner = "demo",
                FacebookSite = "demo.com",
                TwitterSite = "demo.com",
                GooglePlusSite = "demo.com",
                InstagramSite = "demo.com",
                SiteAdress = "demo.com",
                YoutubeSite = "demo.com",
                SiteDefaultLongTitle = "demo",
                ContactAdress = "demo",
                ContactNumber = "demo",
                IFrame = "demo",
                Maps = "demo",
                Logo = "demo",
                MailServer = "demo",
                Image1 = "demo",
                Image2 = "demo",
                Video1 = "demo",
                Video2 = "demo",
                mainTitle = "demo",
                WorkingTime = "0",
                Yetkili1 = "demo",
                Yetkili2 = "demo",
                Yetkili1Tel = "5556667788",
                Yetkili2Tel = "4446665554",
                IsActive = true,
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                HasImage1 = false,
                HasImage2 = false,
            });
        }
    }
}
