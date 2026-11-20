using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Page : EntityBase, IEntity
    {
        public string? PageTitle { get; set; } 
        public string? Text1 { get; set; } 
        public string? Text2 { get; set; } 
        public int PageNumber { get; set; } = 0;      
        public bool IsMainPage { get; set; }       
        public string? Url { get; set; } 
        public string? ListingImage { get; set; } 
        public string? BackgroundImage { get; set; }
        public string? siteBaseUrl { get; set; }
        public string? Description { get; set; } 
        public string? SEOTitle { get; set; } 
        public string? SEOKeywords { get; set; } 
        public string? SEODescription { get; set; } 
    }
}

