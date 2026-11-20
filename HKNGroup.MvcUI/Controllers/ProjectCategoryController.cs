using HKNGroup.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ProjectCategoryController : Controller
    {
        private readonly IProjectService _projects;
        private readonly IProjectCategoryService _projectCategory;
        private readonly ISiteSettingsService _siteSettingsService;

        public ProjectCategoryController(IProjectService projects, IProjectCategoryService projectCategory, ISiteSettingsService siteSettingsService)
        {
            _projects = projects;
            _projectCategory = projectCategory;
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IActionResult> Index(string name)
        {
            ViewData["Breadcumb"] = "Projelerimiz";
            var projectCategory = await _projectCategory.GeString(name);

            if (projectCategory.ResultStatus == ResultStatus.Error)
            {
                return View();
            }

            var project = await _projects.GetAllByNonDeleteAndActiveWithProjectCategoryId(projectCategory.Data.ProjectCategory.Id);
            var siteSettings = await _siteSettingsService.GetUpdateDto(1);

            if (project.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }

            if (project.ResultStatus == ResultStatus.Success)
            {
                ViewBag.ProjeKategori = "active";

                var modelView = new ProjectViewModel()
                {
                    ProjectList = project.Data.Projects,
                    SiteSetting = siteSettings.Data
                };
                return View(modelView);
            }
            return NotFound();
        }
    }
}
