using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.ProjectCategories
{
    public class ProjectCategoryAddDto
    {
        [DisplayName("Kategori Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Name { get; set; } = string.Empty;
        // 
        [DisplayName("Kategori Sıra Nosu")]
        public int CategoryNumber { get; set; } = 1;
        //

        public string? Url { get; set; }
        //
        [DisplayName("Arkaplan Görseli ")]
        public string? BackgroundImage { get; set; }
        //
        [DisplayName("Kapak Resmi ")]
        public string? CoverPhoto { get; set; }
        public string? siteBaseUrl { get; set; }
        //
        [DisplayName("Acıklama ")]
        public string? Description { get; set; }
        //
        [DisplayName("SEO Başlık (Title) ")]
        public string? SEOTitle { get; set; }
        //
        [DisplayName("SEO Kelimeler (Keywords)")]
        public string? SEOKeywords { get; set; }
        //
        [DisplayName("SEO Açıklama (Description) ")]
        public string? SEODescription { get; set; }
        public int SiteId { get; set; } = 0;
    }
}

