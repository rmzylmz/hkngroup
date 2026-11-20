using Menus = HKNGroup.Domain.Entities.Menus;

namespace HKNGroup.Application.Features.Menus
{
    public class MenusAddDto : DtoGetBase
    {
        public int  Id { get; set; } = 0;
        public int ParentId { get; set; } = 0;
        public string? Name { get; set; }
        public string? LinkPage { get; set; }
        public int? MenuRow { get; set; }
        public int? AutomaticListing { get; set; } = 0;
        public string? MenuSeo { get; set; }
        public bool YanSekme { get; set; } = false;

        
    }
}


