using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Referance : EntityBase, IEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string? siteBaseUrl { get; set; }
        public int RowNumbar { get; set; } = 1;
    }
}

