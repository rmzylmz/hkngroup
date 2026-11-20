using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.OurServices
{
    public class OurServicesUpdateDto
    {
        [Required]
        public int Id { get; set; }
        //

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
        //
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir!")]
        public bool IsActive { get; set; }
        //
        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir!")]
        public bool IsDeleted { get; set; }

        //
        [DisplayName("Seo Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public string? SeoTags { get; set; }
        //
        [DisplayName("Seo Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public string? SeoDescription { get; set; }
    }
}

