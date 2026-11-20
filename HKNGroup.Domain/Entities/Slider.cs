using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Slider : EntityBase, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Description2 { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string? siteBaseUrl { get; set; }
        public int RowNumbar { get; set; } = 1;
    }
}

