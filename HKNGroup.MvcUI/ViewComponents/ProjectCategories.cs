using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class ProjectCategories : ViewComponent
    {
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectCategories(IProjectCategoryService categoryService)
        {
            _projectCategoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            return View(categories?.Data);
        }
    }
}
