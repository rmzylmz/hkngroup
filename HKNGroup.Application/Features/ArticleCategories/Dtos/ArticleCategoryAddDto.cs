using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.ArticleCategories
{
    public class ArticleCategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Name { get; set; } = string.Empty;
        //
        [DisplayName("Kapak resmi")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        public IFormFile? CoverImages { get; set; }
        public string? siteBaseUrl { get; set; }
        public string? CategoryFA { get; set; }
        //
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Kategori Adı Boş geçilmemelidir!")]
        [MaxLength(150, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Description { get; set; } = string.Empty;
        public string? Url { get; set; }
    }
}

