using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleController(IArticleService articleService, IArticleCategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllByNonDeleteAndActive();
            if (articles.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (articles.ResultStatus == ResultStatus.Success)
            {
                if (articles.Data?.Articles != null)
                {
                    articles.Data.Articles = articles.Data.Articles.OrderByDescending(x => x.Id).ToList();
                }
                return View(articles.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAllByNonDeleteAndActive();

            ViewBag.CategoryList = categories.Data.Categories;
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (articleAddDto.CoverImages != null && articleAddDto.CoverImages.Length > 0)
                    { 
                        string folder = "uploads/makaleler/";
                        int targetWidth = 391;
                        int targetHeight = 230;

                        var result = await ImageHelper.ResizeAndUploadImage(folder, articleAddDto.CoverImages, targetWidth, targetHeight, Request);
                        articleAddDto.Thumbnail = result;
                    }
                    articleAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _articleService.Add(articleAddDto, "Ramazan YILMAZ");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Makale eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeleteAndActive();
            ViewBag.CategoryList = categories.Data?.Categories;
            return View(articleAddDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var article = await _articleService.Get(id);
            if (article.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (article.ResultStatus == ResultStatus.Success)
            {
                return View(article.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var article = await _articleService.GetUpdateDto(id);
            if (article.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (article.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetString("imagePath", article.Data.Thumbnail.ToString());
                var categories = await _categoryService.GetAllByNonDeleteAndActive();
                ViewBag.CategoryList = categories.Data.Categories;
                return View(article.Data);
            }
            return NotFound();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(ArticleUpdateDto articleUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (articleUpdateDto.CoverImages != null && articleUpdateDto.CoverImages.Length > 0)
                    {
                        string folder = "uploads/makaleler/";
                        int targetWidth = 391;
                        int targetHeight = 230;

                        var result = await ImageHelper.ResizeAndUploadImage(folder, articleUpdateDto.CoverImages, targetWidth, targetHeight, Request);
                        articleUpdateDto.Thumbnail = result;
                    }
                    articleUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _articleService.Update(articleUpdateDto, "Ramazan YILMAZ");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Makale güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeleteAndActive();
            ViewBag.CategoryList = categories.Data?.Categories;
            return View(articleUpdateDto);
        }

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _articleService.Get(id);

            if (service == null || service.ResultStatus != ResultStatus.Success || service.Data?.Article == null)
            {
                return NotFound();
            }

            var imagePath = service.Data.Article.Thumbnail;

            await _articleService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }
    }
}
