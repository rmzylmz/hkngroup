using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class NavbarMiddle : ViewComponent
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public NavbarMiddle(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var identity = await _siteSettingsService.GetUpdateDto(1);
            return View(identity?.Data);
        }
    }
}
