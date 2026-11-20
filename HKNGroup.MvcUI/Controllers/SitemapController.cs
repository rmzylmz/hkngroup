using HKNGroup.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HKNGroup.WebUI.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IArticleService _articles;
        private readonly IArticleCategoryService _articleCategory;
        private readonly IProjectCategoryService _projectCategory;
        private readonly IProjectService _projects;
        private readonly IOurServicesService _services;

        public SitemapController(IPageService pageService, IArticleService articles, IArticleCategoryService articleCategory, IProjectCategoryService projectCategory, IProjectService projects, IOurServicesService services)
        {
            _pageService = pageService;
            _articles = articles;
            _articleCategory = articleCategory;
            _projectCategory = projectCategory;
            _projects = projects;
            _services = services;
        }

        public async Task<ActionResult> Index()
        {
            var sitemapItems = GetSitemapItemsAsync();

            var sitemap = "<urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>";

            foreach (var item in await sitemapItems)
            {
                sitemap += $"<url><loc>{item.Url}</loc></url>";
            }

            sitemap += "</urlset>";

            return Content(sitemap, "application/xml");
        }

        private async Task<List<SitemapItem>> GetSitemapItemsAsync()
        {
            List<SitemapItem> sitemapItems = new List<SitemapItem>();

            var projectCategory = await _projectCategory.GetAllByNonDeleteAndActive();
            var project = await _projects.GetAllByNonDeleteAndActive();
            var articles = await _articles.GetAllByNonDeleteAndActive();
            var articleCategory = await _articleCategory.GetAllByNonDeleteAndActive();
            var pages = await _pageService.GetAllByNonDeleteAndActive();
            var services = await _services.GetAllByNonDeleteAndActive();

            foreach (var item in projectCategory.Data.ProjectCategories)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.ProjeKategori + "/" + item.Url, LastModified = DateTime.Now });
            }

            foreach (var item in project.Data.Projects)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.Proje + "/" + item.Url, LastModified = DateTime.Now });
            }

            foreach (var item in articles.Data.Articles)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.HaberDetay + "/" + item.Url, LastModified = DateTime.Now });
            }

            foreach (var item in articleCategory.Data.Categories)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.HaberKategori + "/" + item.Url, LastModified = DateTime.Now });
            }

            foreach (var item in pages.Data.Page)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.Icerik + "/" + item.Url, LastModified = DateTime.Now });
            }

            foreach (var item in services.Data.OurServices)
            {
                sitemapItems.Add(new SitemapItem { Url = RoutingEnum.Hizmet + "/" + item.Url, LastModified = DateTime.Now });
            }

            return sitemapItems;
        }
    }
}
