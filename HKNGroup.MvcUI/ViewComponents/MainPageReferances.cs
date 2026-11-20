using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class MainPageReferances : ViewComponent
    {
        private readonly IReferanceService _referanceService;

        public MainPageReferances(IReferanceService referanceService)
        {
            _referanceService = referanceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var referances = await _referanceService.GetAllByNonDeleteAndActive();
            if (referances?.Data == null)
            {
                return View();
            }
            return View(referances.Data);
        }
    }
}
