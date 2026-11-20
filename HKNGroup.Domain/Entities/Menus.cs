using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Menus : EntityBase, IEntity
    {
        public int ParentId { get; set; } = 0;
    
        public string? Name { get; set; }
        public string? LinkPage { get; set; }
        public int? MenuRow { get; set; }
        public int? AutomaticListing { get; set; } = 0;
        public string? MenuSeo { get; set; }
        public bool YanSekme { get; set; } = false;
    }
}

