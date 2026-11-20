using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class MainPageProjects : ViewComponent
    {
        private readonly IProjectService _projectService;

        public MainPageProjects(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _projectService.GetAllByNonDeleteAndActive();
            if (projects?.Data == null)
            {
                return View();
            }
            return View(projects.Data);
        }
    }
}
