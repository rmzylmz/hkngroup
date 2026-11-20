using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.Admins
{
    public class AdminLoginDto
    {
        [DisplayName("Kullanıcı Adı veya E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")] 
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MaxLength(100, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
        [MinLength(4, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır!")]
        public string Password { get; set; } = string.Empty;
    }
}
