using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class SiteSettings : ViewComponent
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public SiteSettings(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            var identity = await _siteSettingsService.GetUpdateDto(1);
            if (identity?.Data != null)
            {
                identity.Data.SiteDefaultTitle = $"{identity.Data.SiteDefaultTitle} | {title}";
                return View(identity.Data);
            }
            return View();
        }
    }
}
