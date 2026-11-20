using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.Articles
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden fazla olmamalıdır!")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır!")]
        public string Title { get; set; } = string.Empty;
        //
        [DisplayName("Kısa İçerik Yazısı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(400, ErrorMessage = "{0} alanı {1} karakterden fazla olmamalıdır!")]
         public string ShortContent { get; set; } = string.Empty;
        //
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır!")]
        public string Content { get; set; } = string.Empty;

        //
        [DisplayName("Kapak resmi")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        public IFormFile? CoverImages { get; set; }
        public string? Thumbnail { get; set; }
        public string? siteBaseUrl { get; set; }
        //
        [DisplayName("Seo Etiketleri")]
        public string? SeoTags { get; set; }
        //
        [DisplayName("Seo Açıklaması")]
        public string? SeoDescription { get; set; }
        //
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public int? CategoryId { get; set; }
        public string? Url { get; set; }
        //
        [DisplayName("Aktif mi?")]
        public bool IsActive { get; set; }
        //
        [DisplayName("Silindi mi?")]
        public bool IsDeleted { get; set; }
        public bool? Headline { get; set; }
        public DateTime? HeadlineStartDate { get; set; }
        public DateTime? HeadlineEndingDate { get; set; }
    }
}

