using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleCategoryController(IArticleCategoryService articleCategoryService, IHttpContextAccessor httpContextAccessor)
        {
            _articleCategoryService = articleCategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _articleCategoryService.GetAllByNonDeleteAndActiveSite();
            if (categories.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (categories.ResultStatus == ResultStatus.Success)
            {
                if (categories.Data?.Categories != null)
                {
                    categories.Data.Categories = categories.Data.Categories.OrderByDescending(x => x.Id).ToList();
                }
                return View(categories.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(ArticleCategoryAddDto articleCategoryAddDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (articleCategoryAddDto.CoverImages != null)
                    {
                        string folder = "uploads/makale-kategori/";
                        int targetWidth = 391;
                        int targetHeight = 230;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, articleCategoryAddDto.CoverImages, targetWidth, targetHeight, Request);
                        articleCategoryAddDto.CategoryFA = result;
                    }
                    articleCategoryAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _articleCategoryService.Add(articleCategoryAddDto, "Ramazan YILMAZ");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Kategori eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(articleCategoryAddDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var articleCategory = await _articleCategoryService.GetUpdateDto(id);
            if (articleCategory.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (articleCategory.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetString("imagePath", articleCategory.Data.CategoryFA.ToString());
                return View(articleCategory.Data);
            }
            return NotFound();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(ArticleCategoryUpdateDto articleCategoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (articleCategoryUpdateDto.CoverImages != null)
                    { 
                        string folder = "uploads/makale-kategori/";
                        int targetWidth = 391;
                        int targetHeight = 230;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, articleCategoryUpdateDto.CoverImages, targetWidth, targetHeight, Request);
                        articleCategoryUpdateDto.CategoryFA = result;
                    }
                    articleCategoryUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _articleCategoryService.Update(articleCategoryUpdateDto, "Ramazan YILMAZ");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Kategori güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(articleCategoryUpdateDto);
        }

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _articleCategoryService.Get(id);

            if (service == null || service.ResultStatus != ResultStatus.Success || service.Data?.Category == null)
            {
                return NotFound();
            }

            var imagePath = service.Data.Category.CategoryFA;

            await _articleCategoryService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }
    }
}
