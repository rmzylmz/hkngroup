using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Project : EntityBase, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? ShortContent { get; set; }
        public string? Url { get; set; }
        public string? Content { get; set; }
        public string? Location { get; set; }
        public DateTime ProjectDate { get; set; } = DateTime.Now;        
        public string? siteBaseUrl { get; set; }
        public int? ViewsCount { get; set; } = 0;
        public string? SeoTags { get; set; }
        public string? SeoDescription { get; set; }
        public int ProjectCategoryId { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public ICollection<ProjectImages> ProjectImages { get; set; } = new List<ProjectImages>();
        public ProjectCategory? Category { get; set; } 
    }
}

