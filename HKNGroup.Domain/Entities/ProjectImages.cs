using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class ProjectImages : EntityBase, IEntity
    {
        public int ProjectId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? URL { get; set; }
        public string? siteBaseUrl { get; set; }
        public Project? Projects { get; set; }
    }
}

