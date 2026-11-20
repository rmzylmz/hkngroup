using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.SiteSettings
{
    public class SiteSettingsUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string? SiteDefaultTitle { get; set; }
        public string? SiteDescription { get; set; }
        public string? SiteKeyword { get; set; }
        public string? SiteMail { get; set; }
        public string? SiteMailPass { get; set; }
        public string? SiteOwner { get; set; }
        public string? FacebookSite { get; set; }
        public string? TwitterSite { get; set; }
        public string? GooglePlusSite { get; set; }
        public string? InstagramSite { get; set; }
        public string? YoutubeSite { get; set; }
        public string? Video1 { get; set; }
        public string? Maps { get; set; }
        public string? Video2 { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public bool HasImage1 { get; set; } = false;
        public bool HasImage2 { get; set; } = false;
        public string? SiteAdress { get; set; }
        public string? SiteDefaultLongTitle { get; set; }
        public string? ContactAdress { get; set; }
        public string? ContactNumber { get; set; }
        public string? IFrame { get; set; }
        public string? Logo { get; set; }
        public string? MailServer { get; set; }
        public string? mainTitle { get; set; }
        public string? WorkingTime { get; set; }
        public string? Yetkili1 { get; set; }
        public string? Yetkili2 { get; set; }
        public string? Yetkili1Tel { get; set; }
        public string? Yetkili2Tel { get; set; }
        public string? SiteSlogan { get; set; }
    }
}

