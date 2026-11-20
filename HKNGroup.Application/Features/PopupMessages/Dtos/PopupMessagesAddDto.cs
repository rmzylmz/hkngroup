using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.PopupMessages
{
    public class PopupMessagesAddDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        
        public string Title { get; set; } = string.Empty;
        //
        [DisplayName("Açıklama")]
        public string Description { get; set; } = string.Empty;
        //
        [DisplayName("Mesaj Url") ]
        public string? MessageUrl { get; set; }
        //
        [DisplayName("Resim")] 
        public string? Thumbnail { get; set; }
        public string? siteBaseUrl { get; set; }
        //
        [DisplayName("Sıra Nosu")]
        public int RowNumbar { get; set; }
        
    }
}

