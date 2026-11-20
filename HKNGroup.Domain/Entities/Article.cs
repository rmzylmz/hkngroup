using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Article : EntityBase, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ShortContent { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Thumbnail { get; set; }
        public string? siteBaseUrl { get; set; }
        public int ViewsCount { get; set; } = 0;
        public string? SeoTags { get; set; }
        public string? SeoDescription { get; set; }
        public int CategoryId { get; set; }
        public string? Url { get; set; }
        public bool? Headline { get; set; }
        public DateTime? HeadlineStartDate { get; set; }
        public DateTime? HeadlineEndingDate { get; set; }
        public ArticleCategory? Category { get; set; }
        public ICollection<ArticleComment> Comments { get; set; } = new List<ArticleComment>();
    }
}

