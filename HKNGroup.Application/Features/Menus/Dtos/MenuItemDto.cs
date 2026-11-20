using Menus = HKNGroup.Domain.Entities.Menus;
namespace HKNGroup.Application.Features.Menus
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public bool YanSekme { get; set; }
        public int? MenuRow { get; set; }
        public int? AutomaticListing { get; set; } = 0;
        public List<MenuItemDto> Children { get; set; } = new List<MenuItemDto>();
    }
}


