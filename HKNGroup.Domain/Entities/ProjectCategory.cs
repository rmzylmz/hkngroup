using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class ProjectCategory : EntityBase, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryNumber { get; set; } = 1;
        public string? Url { get; set; }
        public string? BackgroundImage { get; set; }
        public string? siteBaseUrl { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Description { get; set; }
        public string? SEOTitle { get; set; }
        public string? SEOKeywords { get; set; }
        public string? SEODescription { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}

