using Menus = HKNGroup.Domain.Entities.Menus;
using AutoMapper;

namespace HKNGroup.Application.Features.Menus
{
    public class MenusManager : IMenusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenusManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<MenusDto>> Add(MenusAddDto menusAddDto, string createdByName)
        {
            var menus = _mapper.Map<HKNGroup.Domain.Entities.Menus>(menusAddDto);
            menus.ModifiedTime = DateTime.Now;
            var addedMenuCategory = await _unitOfWork.MenuCategory.AddAsync(menus);
            await _unitOfWork.SaveAsync();
            return new DataResult<MenusDto>(ResultStatus.Success, $"{addedMenuCategory.Name} isimli menü başarılı bir şekilde kayıt edilmiştir.", new MenusDto
            {
                MenuCategory = addedMenuCategory,
                Message = $"{addedMenuCategory.Name} isimli menü başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }
        public async Task<IDataResult<MenusDto>> Get(int menusId)
        {
            var menus = await _unitOfWork.MenuCategory.GetAsync(x => x.Id == menusId);
            if (menus != null)
            {
                return new DataResult<MenusDto>(ResultStatus.Success, new MenusDto
                {
                    MenuCategory = menus,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<MenusDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new MenusDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                MenuCategory = null
            });
        }
        public async Task<IDataResult<MenusListDto>> GetAll()
        {
            var menus = await _unitOfWork.MenuCategory.GetAllAsync();
            if (menus != null)
            {
                return new DataResult<MenusListDto>(ResultStatus.Success, new MenusListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Menus = menus
                });
            }
            return new DataResult<MenusListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new MenusListDto
            {
                ResultStatus = ResultStatus.Error,
                Menus = menus,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }
        public async Task<IDataResult<MenusListDto>> GetAllByNonDeleteAndActive()
        {
            var menus = await _unitOfWork.MenuCategory.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);

            if (menus != null)
            {
                return new DataResult<MenusListDto>(ResultStatus.Success, new MenusListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Menus = menus
                });
            }
            return new DataResult<MenusListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new MenusListDto
            {
                ResultStatus = ResultStatus.Error,
                Menus = menus,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }
        public async Task<IDataResult<MenusListDto>> GetAllMainMenus(int parentId)
        {
            var menus = await _unitOfWork.MenuCategory.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true && x.ParentId == parentId);

            if (menus != null)
            {
                return new DataResult<MenusListDto>(ResultStatus.Success, new MenusListDto
                {
                    Menus = menus,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<MenusListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new MenusListDto
            {
                ResultStatus = ResultStatus.Error,
                Menus = menus,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }
        public async Task<IDataResult<MenusUpdateDto>> GetUpdateDto(int menusId)
        {
            var menus = await _unitOfWork.MenuCategory.GetAsync(x => x.Id == menusId);

            if (menus != null)
            {              
                var menusUpdateDto = _mapper.Map<MenusUpdateDto>(menus);
                return new DataResult<MenusUpdateDto>(ResultStatus.Success, menusUpdateDto);
            }

            return new DataResult<MenusUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);

        }
        public async Task<IResult> HardDelete(int menusId)
        {
            var menus = await _unitOfWork.MenuCategory.GetAsync(x => x.Id == menusId);
            if (menus != null)
            {
                await _unitOfWork.MenuCategory.DeleteAsync(menus);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{menus.Name} isimli menü başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }
        public async Task<IDataResult<MenusDto>> Update(MenusUpdateDto menusUpdateDto, string modifiedByName)
        {
            var menus = _mapper.Map<HKNGroup.Domain.Entities.Menus>(menusUpdateDto);
            var updatedMenuCategory = await _unitOfWork.MenuCategory.UpdateAsync(menus);
          
            await _unitOfWork.SaveAsync();
            return new DataResult<MenusDto>(ResultStatus.Success, $"{updatedMenuCategory.Name} isimli menü başarılı bir şekilde güncellenmiştir.", new MenusDto
            {
                ResultStatus = ResultStatus.Success,
                MenuCategory = updatedMenuCategory,
                Message = $"{updatedMenuCategory.Name} isimli menü başarılı bir şekilde güncellenmiştir."
            });
        }
    }
}



