using Menus = HKNGroup.Domain.Entities.Menus;
using System.ComponentModel.DataAnnotations;

namespace HKNGroup.Application.Features.Menus
{
    public class MenusUpdateDto : DtoGetBase
    {
        [Required]
        public int Id { get; set; }
        public int SelectedMenuId { get; set; }
        public int ParentId { get; set; } = 0;
        public string? Name { get; set; }
        public string? LinkPage { get; set; }
        public int? MenuRow { get; set; }
        public int? AutomaticListing { get; set; } = 0;
        public string? MenuSeo { get; set; }
        public bool YanSekme { get; set; } = false;

    }
}


