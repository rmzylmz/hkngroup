using HKNGroup.WebUI.Models;
using HomeViewModel = HKNGroup.WebUI.Models.HomeViewModel;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HKNGroup.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IPageService _pageService;

        public HomeController(ISliderService sliderService, ISiteSettingsService siteSettingsService, IPageService pageService)
        {
            _sliderService = sliderService;
            _siteSettingsService = siteSettingsService;
            _pageService = pageService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentURL = "Home";
            await Identity();

            var homePages = await _pageService.GetAllByNonDeleteAndActiveAndIsMainPage();
            var sliders = await _sliderService.GetAllByNonDeleteAndActive();
            var siteSettings = await _siteSettingsService.GetUpdateDto(1);

            if (sliders.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (sliders.ResultStatus == ResultStatus.Success)
            {
                ViewBag.Home = "active";               

                var modelView = new HomeViewModel()
                {
                    Sliders = sliders.Data.Sliders,
                    Pages = homePages.Data.Page,
                    SiteSetting= siteSettings.Data
                };
                 
                return View(modelView);
              
            }
            return NotFound();
        }

        private async Task Identity()
        {
            var identity = await _siteSettingsService.GetUpdateDto(1);

            HttpContext.Session.SetString("Tel", identity.Data.ContactNumber.ToString());
            HttpContext.Session.SetString("Email", identity.Data.SiteMail.ToString());
            HttpContext.Session.SetString("Adres", identity.Data.ContactAdress.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
