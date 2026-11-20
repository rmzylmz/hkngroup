
namespace HKNGroup.Application.Features.SiteSettings
{
    public class SiteSettingsListDto : DtoGetBase
    {
        public IList<SiteSetting>? SiteSettings { get; set; }
    }
}

