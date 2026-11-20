using HKNGroup.WebUI.Models;
using HKNGroup.WebUI.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ReferancesController : Controller
    {
        private readonly IReferanceService _referanceService;

        public ReferancesController(IReferanceService referanceService)
        {
            _referanceService = referanceService;
        }

        public async Task<IActionResult> Index()
        {

            ViewData["Breadcumb"] = "Referanslarımız";
            var referances = await _referanceService.GetAllByNonDeleteAndActive();


            if (referances.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (referances.ResultStatus == ResultStatus.Success)
            {
                ViewBag.Referances = "active";

                var modelView = new ReferanceViewModel()
                {
                    ReferanceList = referances.Data.Referances 
                };
                return View(modelView);
            }
            return NotFound();
        }
    }
}
