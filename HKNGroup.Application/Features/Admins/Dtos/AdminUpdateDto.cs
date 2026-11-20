using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.Admins
{
    public class AdminUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır!")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Şifre")]
        public string? Password { get; set; }

        [DisplayName("E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz!")]
        public string Email { get; set; } = string.Empty;
         
        [DisplayName("Aktif mi?")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Silindi mi?")]
        public bool IsDeleted { get; set; } = false;
    }
}
