namespace HKNGroup.Application.Features.Menus
{
    public class MenusListDto : DtoGetBase
    {
        public IList<HKNGroup.Domain.Entities.Menus>? Menus { get; set; }
    }
}
