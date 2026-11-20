using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.OurServices
{
    public class OurServicesAddDto
    {
        [DisplayName("Faaliyet Alanı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public string Title { get; set; } = string.Empty;
        //
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public string Description { get; set; } = string.Empty;
        //
        [DisplayName("Resim")]      
        public string? Images { get; set; }
        public string? siteBaseUrl { get; set; }
        public string? Url { get; set; }
        //
        [DisplayName("Seo Etiketleri")]
        public string? SeoTags { get; set; }
        //
        [DisplayName("Seo Açıklaması")]
        public string? SeoDescription { get; set; }
    }                                                  
}

