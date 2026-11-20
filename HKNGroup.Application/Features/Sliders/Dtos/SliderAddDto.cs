using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.Sliders
{
    public class SliderAddDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(250, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Title { get; set; } = string.Empty;
        //
        [DisplayName("Açıklama-1")]  
        [MinLength(20, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Description { get; set; } = string.Empty;
        //
        [DisplayName("Açıklama-2")]
        [MinLength(20, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string? Description2 { get; set; }
        //
        [DisplayName("Resim")] 
        public string? Thumbnail { get; set; }
        public string? siteBaseUrl { get; set; }
        //
        [DisplayName("Slayt Sıra Nosu")]
        public int RowNumbar { get; set; }
        
    }
}

