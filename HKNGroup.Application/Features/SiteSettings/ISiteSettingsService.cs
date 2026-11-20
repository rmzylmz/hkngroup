
namespace HKNGroup.Application.Features.SiteSettings
{
    public interface ISiteSettingsService
    {
        Task<IDataResult<SiteSettingsUpdateDto>> GetUpdateDto(int Id);
        Task<IDataResult<SiteSettingsDto>> Update(SiteSettingsUpdateDto siteSettingUpdateDto);
    }
}

