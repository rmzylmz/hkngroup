using HKNGroup.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IOurServicesService _services;
        private readonly ISiteSettingsService _siteSettings;
        public ServicesController(IOurServicesService services, ISiteSettingsService siteSettings)
        {
            _services = services;
            _siteSettings = siteSettings;
        }

        public async Task<IActionResult> Index()
        {

            ViewData["Breadcumb"] = "Hizmetler";
            var services = await _services.GetAllByNonDeleteAndActive();
            var siteSettings = await _siteSettings.GetUpdateDto(1);

            if (services.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (services.ResultStatus == ResultStatus.Success)
            {
                ViewBag.Hizmetler = "active";

                var modelView = new OurServicesViewModel()
                {
                    OurServiceList = services.Data.OurServices,
                    SiteSetting = siteSettings.Data
                };
                return View(modelView);
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return NotFound();
            }


            var serviceList = await _services.GetAllByNonDeleteAndActive();
            var serviceDetails = await _services.GetString(name);
            var siteSettings = await _siteSettings.GetUpdateDto(1);

            if (serviceDetails.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (serviceDetails.ResultStatus == ResultStatus.Success)
            {
                var modelView = new OurServicesViewModel()
                {
                    OurServiceDetay = serviceDetails.Data.OurServices,
                    OurServiceList = serviceList.Data.OurServices,
                    SiteSetting = siteSettings.Data
                };

                ViewData["Breadcumb"] = modelView.OurServiceDetay.Title;



                return View(modelView);
            }
            return NotFound();
        }
    }
}
