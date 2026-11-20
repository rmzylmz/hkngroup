using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.References
{
    public class ReferanceUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(250, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Title { get; set; } = string.Empty;  
 
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

