using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.Pages
{
    public class PageAddDto
    {
        [DisplayName("Sayfa Başlığı")]
        public string? PageTitle { get; set; } 
        //
        [DisplayName("Text-1")] 
        public string? Text1 { get; set; } 
        //
        [DisplayName("Text-2")]
        public string? Text2 { get; set; } 
        //
        [DisplayName("Sayfa Numarası")]
        public int PageNumber { get; set; } = 0;
        // 
        [DisplayName("Link Yapısı")]
        public string? Url { get; set; }
        //
        [DisplayName("Listeleme Görseli")]
        public string? ListingImage { get; set; }
        //
        [DisplayName("Anasayfada Görünsün mü?")]
        public bool IsMainPage { get; set; }
        //
        [DisplayName("Arka Plan Görseli")]
        public string? BackgroundImage { get; set; } 
        //
        [DisplayName("İçerik")] 
        public string? Description { get; set; } 
        //
        [DisplayName("SEO Başlık (Title)")] 
        public string? SEOTitle { get; set; } 
        //
        [DisplayName("SEO Kelimeler (Keywords)")] 
        public string? SEOKeywords { get; set; } 
        //
        [DisplayName("SEO Açıklama (Description)")] 
        public string? SEODescription { get; set; }
        
    }
}

