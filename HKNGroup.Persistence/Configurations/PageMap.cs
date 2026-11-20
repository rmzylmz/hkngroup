using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PageMap : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages", @"dbo");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
             
            
            builder.Property(x => x.IsActive)
               .HasColumnName("IsActive")
               .HasDefaultValueSql("0")
               .IsRequired();


            builder.Property(x => x.IsDeleted)
              .HasColumnName("IsDeleted")
              .HasDefaultValueSql("0")
              .IsRequired();

            builder.Property(x => x.IsMainPage)
            .HasColumnName("IsMainPage")
            .HasDefaultValueSql("0");

            builder.HasData(new Page
            {
                Id = 1,
                PageTitle = "Kurumsal",
                Text1 = "Bir Farkımız da Yörüngemiz",
                Text2 = "",
                PageNumber = 1,
                ListingImage = "",
                BackgroundImage = "",
                Description = "<p> yılında kurulan Hema İnşaat Taahhüt ve Ticaret Limited Şirketi olarak büyük projelere imza atıyoruz ve sizlere en iyi hizmeti sağlamak için çalışıyoruz. İlk günkü heyecanımız ve iş aşkımızla, sevdiklerinizle birlikte huzur içinde yaşayıp, mutlu anılar biriktirebilmeniz için anahtar teslim projeler yapıyoruz ve sizler için güvenli yapılar inşa ediyoruz. </p><p>35 yıllık hikayemizde yaşamlarına dokunduğumuz tüm mutlu aileler, başarımızın teminatıdır. Hema İnşaat olarak sizlere hizmet vermekten gurur duyuyoruz<br></p>",
                SEOTitle = "",
                SEOKeywords = "",
                SEODescription = "",
                Url = SeoHelper.ToSeoUrl("Kurumsal"),
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                IsMainPage = false,

            }, new Page
            {
                Id = 2,
                PageTitle = "Neden Biz",
                Text1 = "Gelecek nesiller için sürdürülebilir yapılar inşa ediyoruz.",
                Text2 = "Kent mimarisine estetik ve yaşama değer katan projeler üreten Şua İnşaat; köklü geçmişi, güçlü finansal yapısı, yüksek kalite anlayışı, yenilikçi mimari tarzı ve doğaya dost yaklaşımı ile bugüne kadar binlerce konut ve ticari üniteye imza attı.",
                PageNumber = 2,
                ListingImage = "",
                BackgroundImage = "",
                Description = " <div class=\"info-media\">\r\n <div class=\"info-media_icon\">\r\n <i class=\"far fa-lightbulb-on\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Mimari</h3>\r\n  <p class=\"info-media_text\">Dynamically extend 24/365 human capital whereas premium opportunities. Continually evisculate.</p>\r\n </div>\r\n </div>\r\n <div class=\"info-media\">\r\n <div class=\"info-media_icon\">\r\n <i class=\"far fa-circle-dollar\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Tasarım</h3>\r\n  <p class=\"info-media_text\">Collaboratively customize efficient initiatives whereas cooperative synergy. Credibly promote ethical.</p>\r\n </div>\r\n </div>\r\n <div class=\"info-media mb-0\">\r\n <div class=\"info-media_icon\">\r\n<i class=\"far fa-headset\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Planlama</h3>\r\n  <p class=\"info-media_text\">Seamlessly evisculate B2C supply chains rather than efficient architectures. Rapidiously.</p>\r\n </div>\r\n </div>",
                SEOTitle = "",
                SEOKeywords = "",
                SEODescription = "",
                Url = SeoHelper.ToSeoUrl("Neden Biz"),
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                IsMainPage = false,

            }, new Page
            {
                Id = 3,
                PageTitle = "Hakkımızda",
                Text1 = "Hakan İnşaat",
                Text2 = "Kente estetik, yaşama değer katar.",
                PageNumber = 3,
                ListingImage = "",
                BackgroundImage = "",
                Description = "<p>Firmamız geniş ekipman parkı ve sektöründe uzmanlaşmış deneyimli personeli ile her türlü taahhüdü yerine getirebilecek donanıma sahiptir.&nbsp;</p><p>Deneyimli ve çağın gerektirdiği yönetim anlayışı ile sorunlarınıza duyarlı, anında çözüm üreten teknik personelimizle müşteri memnuniyetini ön planda tutarak, güven duygusu ile hizmet üretmeye devam etmektedir.&nbsp;</p><p>Deneyimli ve çağın gerektirdiği yönetim anlayışı ile sorunlarınıza duyarlı, anında çözüm üreten teknik personelimizle müşteri memnuniyetini ön planda tutarak, güven duygusu ile hizmet üretmeye devam etmektedir.&nbsp;<br></p>",
                SEOTitle = "",
                SEOKeywords = "",
                SEODescription = "",
                Url = SeoHelper.ToSeoUrl("Hakkımızda"),
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                IsMainPage = false,

            }, new Page
            {
                Id = 4,
                PageTitle = "Vizyon & Misyon",
                Url =SeoHelper.ToSeoUrl("Vizyon & Misyon"),
                Text1 = "Vizyon & Misyon",
                Text2 = "..",
                PageNumber = 4,
                ListingImage = "",
                BackgroundImage = "",
                Description = " <div class=\"title-area mb-30\"> \r\n  <h2 class=\"sec-title\">Misyon</h2>\r\n </div>\r\n <p class=\"mt-n2 mb-4\">\r\n  Holisticly whiteboard user-centric e-business before professional deliverables. Globally re-engineer cutting-edge information.\r\n  </p>\r\n \r\n <div class=\"about-grid-wrap\">\r\n </div>\r\n\r\n <div class=\"title-area mb-30\">\r\n  <h2 class=\"sec-title\">Vizyon</h2>\r\n </div>\r\n <p class=\"mt-n2 mb-4\">\r\n  Holisticly whiteboard user-centric e-business before professional deliverables. Globally re-engineer cutting-edge information.\r\n </p>",
                SEOTitle = "",
                SEOKeywords = "",
                SEODescription = "",
                CreatedTime = DateTime.Now,
                ModifiedTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                IsMainPage = false,

            }

            ) ;
        }
    }
}
