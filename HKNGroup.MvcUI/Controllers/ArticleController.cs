using HKNGroup.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ISiteSettingsService _siteSettingsService;

        public ArticleController(IArticleService articleService, ISiteSettingsService siteSettingsService)
        {
            _articleService = articleService;
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Breadcumb"] = "Bizden Haberler";
            var articles = await _articleService.GetAllByNonDeleteAndActive();
            var siteSettings = await _siteSettingsService.GetUpdateDto(1);

            if (articles.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (articles.ResultStatus == ResultStatus.Success)
            {
                ViewBag.Haberler = "active";

                var modelView = new ArticlesViewModel()
                {
                    Articles = articles.Data.Articles,
                    SiteSettings = siteSettings.Data
                };

                return View(modelView);
            }
            return NotFound();


        }
        public async Task<IActionResult> Detail(string name)
        {
            ViewData["Breadcumb"] = "Haber Detay";

            TempData["Tel"] = HttpContext.Session.GetString("Tel");
            TempData["Email"] = HttpContext.Session.GetString("Email");
            TempData["Adres"] = HttpContext.Session.GetString("Adres");

            var articles = await _articleService.GetString(name);
            var topArticleList = await _articleService.Get3MostReadByNonDeleteAndActive();

            if (articles.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (articles.ResultStatus == ResultStatus.Success)
            {
              
            
                var modelView = new ArticlesViewModel()
                {
                    ArticleDetay = articles.Data.Article,
                    Articles = topArticleList.Data.Articles,                   
                };
                await _articleService.AddViews(articles.Data.Article.Id);
                ViewData["Breadcumb"] = modelView.ArticleDetay.Title;

                ViewBag.SeoTitle = articles.Data.Article.Title;
                ViewBag.SeoDescription = articles.Data.Article.SeoTags;
                ViewBag.Keywords = articles.Data.Article.SeoDescription;

                return View(modelView);
            }
            return NotFound();
        }

    }
}
