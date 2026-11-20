using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.Messages
{
    public class MessageUpdateDto
    {
        [Required]
        public int Id { get; set; }
        //
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string FirstName { get; set; } = string.Empty;
        //
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string LastName { get; set; } = string.Empty;
        //
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Email { get; set; } = string.Empty;
        //
        [DisplayName("Konu")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Subject { get; set; } = string.Empty;
        //
        [DisplayName("Mesaj")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(512, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Text { get; set; } = string.Empty;
        //
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir!")]
        public bool IsActive { get; set; }
        //
        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir!")]
        public bool IsDeleted { get; set; }
    }
}

