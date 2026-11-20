using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PageController(IPageService pageService, IHttpContextAccessor httpContextAccessor)
        {
            _pageService = pageService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            //var pages = await _pageService.GetAll();
            var pages = await _pageService.GetAllByNonDeleteAndActive();
            if (pages.ResultStatus == ResultStatus.Success)
            {
                if (pages.Data?.Page != null)
                {
                    pages.Data.Page = pages.Data.Page.OrderByDescending(x => x.Id).ToList();
                }
                return View(pages.Data);
            }
            if (pages.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View("");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var page = await _pageService.Get(id);
            if (page.ResultStatus == ResultStatus.Success)
            {
                return View(page.Data);
            }
            if (page.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var page = await _pageService.GetUpdateDto(id);
            if (page.ResultStatus == ResultStatus.Success)
            {
                return View(page.Data);
            }
            if (page.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(PageUpdateDto pageUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updateResult = await _pageService.Update(pageUpdateDto, "Ramazan Yılmaz");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Sayfa güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(pageUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _pageService.Get(id);

            if (service == null || service.ResultStatus != ResultStatus.Success)
            {
                return NotFound();
            } 

            var page = service.Data?.Page; // Use null-conditional operator to check for null
            if (page != null)
            {
                var imagePath = page.ListingImage;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    ImageHelper.DeleteImage(imagePath);
                }
            }

            await _pageService.HardDelete(id);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(PageAddDto pageAddDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addResult = await _pageService.Add(pageAddDto, "Ramazan Yılmaz");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Sayfa eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(pageAddDto);
        }


    }
}
