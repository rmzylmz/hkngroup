using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.ArticleComments
{
    public class ArticleCommentAddDto
    {
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string FirstName { get; set; } = string.Empty;
        //
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string LastName { get; set; } = string.Empty;
        //
        [DisplayName("Yorum")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        [MaxLength(512, ErrorMessage = "{0} en fazla {1} karakter olabilir!")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        public string Text { get; set; } = string.Empty;
        //
        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir!")]
        public int ArticleId { get; set; }
        
    }
}

