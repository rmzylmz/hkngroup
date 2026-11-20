using HKNGroup.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectCategoryService _projectCategory;
        private readonly IProjectService _projects;
        private readonly IProjectImagesService _projectsImages;
        private readonly ISiteSettingsService _siteSettingsService;

        public ProjectController(IProjectCategoryService projectCategory, IProjectService projects, IProjectImagesService projectsImages, ISiteSettingsService siteSettingsService)
        {
            _projectCategory = projectCategory;
            _projects = projects;
            _projectsImages = projectsImages;
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IActionResult> Index()
        {

            ViewData["Breadcumb"] = "Projeler";

            var projectCategory = await _projectCategory.GetAllByNonDeleteAndActive();
            var siteSetting = await _siteSettingsService.GetUpdateDto(1);

            if (projectCategory.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (projectCategory.ResultStatus == ResultStatus.Success)
            {
                ViewBag.Projeler = "active";

                var modelView = new ProjectViewModel()
                {
                    ProjectCategories = projectCategory.Data.ProjectCategories,
                    SiteSetting = siteSetting.Data
                };
                return View(modelView);
            }
            return NotFound();
        }


        public async Task<IActionResult> Detail(string name)
        {
            ViewData["Breadcumb"] = "Projelerimiz";

            TempData["Tel"] = HttpContext.Session.GetString("Tel");
            TempData["Email"] = HttpContext.Session.GetString("Email");
            TempData["Adres"] = HttpContext.Session.GetString("Adres");

            var project = await _projects.GetString(name);
            var projectImage = await _projectsImages.GetAllProjectImagesId(project.Data.Project.Id);


            if (project.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (project.ResultStatus == ResultStatus.Success)
            {
                var modelView = new ProjectViewModel()
                {
                    ProjectDetay = project.Data.Project,
                    ProjectImages = projectImage.Data.ProjectImages
                };

                ViewData["Breadcumb"] = modelView.ProjectDetay.Title;

                return View(modelView);
            }
            return NotFound();
        }


    }
}
