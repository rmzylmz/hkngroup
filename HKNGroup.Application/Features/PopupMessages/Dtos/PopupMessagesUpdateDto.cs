using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.PopupMessages
{
    public class PopupMessagesUpdateDto
    {
        [Required]
        public int Id { get; set; }

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
        [DisplayName("Mesaj Url")]
        [MinLength(20, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string? MessageUrl { get; set; }
        //
        [DisplayName("Resim")]
        public string? Thumbnail { get; set; }
        public string? siteBaseUrl { get; set; }
        //
        [DisplayName("Sıra Nosu")]
        public int RowNumbar { get; set; }
        
        //
        [DisplayName("Aktif mi?")]
        public bool IsActive { get; set; } = true;
        //
        [DisplayName("Silindi mi?")]
        public bool IsDeleted { get; set; } = false;
    }
}

