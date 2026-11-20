using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ContentsController : Controller
    {
        private readonly IPageService _pageService;

        public ContentsController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (name == null)
            {
                return View();
            }
            var pages = await _pageService.GetString(name);

            if (pages.ResultStatus == ResultStatus.Error)
            {
                ViewData["Breadcumb"] = "İçerik Bulunmadı.";
                ViewData["Error"] = "Error";
                return View();
            }
            if (pages.ResultStatus == ResultStatus.Success)
            {
                ViewData["Breadcumb"] = pages.Data.Page.PageTitle;
                return View(pages.Data);
            }
            return View();
        }
       
    }
}
