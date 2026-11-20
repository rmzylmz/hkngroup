using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class MainPageArticles : ViewComponent
    {
        private readonly IArticleService _articleService;

        public MainPageArticles(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleService.GetAllByNonDeleteAndActive();
            return View(articles?.Data);
        }
    }
}
