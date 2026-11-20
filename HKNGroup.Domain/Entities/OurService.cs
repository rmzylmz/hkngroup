using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class OurService : EntityBase, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Images { get; set; }
        public string? siteBaseUrl { get; set; }
        public string? Url { get; set; }
        public string? SeoTags { get; set; }
        public string? SeoDescription { get; set; }
    }
}

