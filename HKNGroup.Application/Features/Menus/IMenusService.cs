using Menus = HKNGroup.Domain.Entities.Menus;

namespace HKNGroup.Application.Features.Menus
{
    public interface IMenusService
    {
        Task<IDataResult<MenusDto>> Get(int menusId);
        Task<IDataResult<MenusUpdateDto>> GetUpdateDto(int menusId);
        Task<IDataResult<MenusListDto>> GetAll(); 
        Task<IDataResult<MenusListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<MenusListDto>> GetAllMainMenus(int parentId); 
        Task<IDataResult<MenusDto>> Add(MenusAddDto menusAddDto, string createdByName);
        Task<IDataResult<MenusDto>> Update(MenusUpdateDto menusUpdateDto, string modifiedByName);
        Task<IResult> HardDelete(int menusId);
    }
}


