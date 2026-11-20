using ProjectImages = HKNGroup.Domain.Entities.ProjectImages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HKNGroup.Application.Features.Projects
{
    public class ProjectUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public string Title { get; set; } = string.Empty;
        //
        [DisplayName("Kısa Açıklama")]
        [MaxLength(500, ErrorMessage = "{0} alanı {1} karakterden fazla olmamalıdır!")]
        public string? ShortContent { get; set; }
        //
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır!")]
        public string Content { get; set; } = string.Empty;

        public string? siteBaseUrl { get; set; }
        public string? Url { get; set; }
        public string? Location { get; set; }
        private DateTime? _projectDate;
        [DisplayName("Proje Tarihi")]
        public DateTime? ProjectDate
        {
            get => _projectDate ?? DateTime.Now;
            set => _projectDate = value;
        }
        //
        [DisplayName("Seo Etiketleri")]
        public string? SeoTags { get; set; }
        //
        [DisplayName("Seo Açıklaması")]
        public string? SeoDescription { get; set; }
        //
        [DisplayName("Proje Kategorisi")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir!")]
        public int? ProjectCategoryId { get; set; }
        //
        [DisplayName("Kapak Resmi")] 
        public IFormFile? FormFileCoverImageUrl { get; set; }
        //
        public string? CoverImageUrl { get; set; }
        //
        [DisplayName("Proje Resimleri")] 
        public IFormFileCollection? GalleryFiles { get; set; }
        //
        public List<ProjectImagesAddDto>? Gallery { get; set; }
        public IList<HKNGroup.Domain.Entities.ProjectImages>? ProjectImages { get; set; }
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



