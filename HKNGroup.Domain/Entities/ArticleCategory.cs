using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class ArticleCategory : EntityBase, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CategoryFA { get; set; } = string.Empty;
        public string? siteBaseUrl { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Url { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}

