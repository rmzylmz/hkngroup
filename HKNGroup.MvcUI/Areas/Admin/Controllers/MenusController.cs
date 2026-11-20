using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly IMenusService _menusService;
        private readonly IPageService _pageService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IArticleService _articleService;
         private readonly IOurServicesService _ourServicesService;
        private readonly IProjectCategoryService _projectCategoryService;
        private readonly IProjectService _projectService;

        public MenusController(IMenusService menusService, IPageService pageService, IArticleCategoryService articleCategoryService, IArticleService articleService, IOurServicesService ourServicesService, IProjectCategoryService projectCategoryService, IProjectService projectService)
        {
            _menusService = menusService;
            _pageService = pageService;
            _articleCategoryService = articleCategoryService;
            _articleService = articleService;
            _ourServicesService = ourServicesService;
            _projectCategoryService = projectCategoryService;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var menuCategories = await _menusService.GetAllByNonDeleteAndActive();

            List<MenuItemDto> menuItems = new List<MenuItemDto>();

            if (menuCategories.ResultStatus == ResultStatus.Success && menuCategories.Data?.Menus != null)
            {
                foreach (var category in menuCategories.Data.Menus.Where(x => x.ParentId == 0))
            {

                MenuItemDto menuItem = new MenuItemDto
                {
                    Id = category.Id,
                    ParentId = category.ParentId,
                    MenuRow = category.MenuRow,
                    Name = category.Name.ToString(),
                    Children = GetSubMenuItems(category.Id, menuCategories.Data)
                };

                menuItems.Add(menuItem);
            }
            }
            return View(menuItems.OrderBy(x => x.MenuRow).ToList());
        }
        private List<MenuItemDto> GetSubMenuItems(int? parentId, MenusListDto menuCategories)
        {
            if (parentId == null || menuCategories?.Menus == null)
            {
                return new List<MenuItemDto>();
            }

            List<MenuItemDto> subMenuItems = new List<MenuItemDto>();

            foreach (var category in menuCategories.Menus.Where(x => x.ParentId == parentId))
            {
                MenuItemDto subMenuItem = new MenuItemDto
                {
                    Id = category.Id,
                    ParentId = category.ParentId,
                    Name = category.Name.ToString(),
                    MenuRow = category.MenuRow,
                    Children = GetSubMenuItems(category.Id, menuCategories)
                };

                subMenuItems.Add(subMenuItem);
            }

            return subMenuItems;
        }
        public async Task<IActionResult> Add()
        {

            var pages = await _pageService.GetAllByNonDeleteAndActive();
            var articleCategories = await _articleCategoryService.GetAllByNonDeleteAndActiveSite();
            var articles = await _articleService.GetAllByNonDeleteAndActiveSite();
            var menuList = await _menusService.GetAllByNonDeleteAndActive();
            var ourServices = await _ourServicesService.GetAllByNonDeleteAndActive();
            var projectcategory = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var project = await _projectService.GetAllByNonDeleteAndActive();

            var viewModel = new MyMenusViewModel
            {
                PageList = pages.Data.Page,
                ArticleCategories = articleCategories.Data.Categories,
                Articles = articles.Data.Articles,
                MenuList = menuList.Data.Menus,
                OurServices = ourServices.Data.OurServices,
                Projects = project.Data.Projects,
                ProjectCategories = projectcategory.Data.ProjectCategories,
            };
            return View(viewModel);
        }

                [HttpPost]
        public async Task<IActionResult> Add(MyMenusViewModel menuCategoryAddDto)
        {
            try
            {
                if (menuCategoryAddDto.MenuAddDto != null)
                {
                    var result = await _menusService.Add(menuCategoryAddDto.MenuAddDto, "Ramazan Yılmaz");
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result.Message ?? "Menü eklenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var pages = await _pageService.GetAllByNonDeleteAndActive();
            var articleCategories = await _articleCategoryService.GetAllByNonDeleteAndActiveSite();
            var articles = await _articleService.GetAllByNonDeleteAndActiveSite();
            var menuList = await _menusService.GetAllByNonDeleteAndActive();
            var ourServices = await _ourServicesService.GetAllByNonDeleteAndActive();
            var projectcategory = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var project = await _projectService.GetAllByNonDeleteAndActive();

            menuCategoryAddDto.PageList = pages.Data?.Page;
            menuCategoryAddDto.ArticleCategories = articleCategories.Data?.Categories;
            menuCategoryAddDto.Articles = articles.Data?.Articles;
            menuCategoryAddDto.MenuList = menuList.Data?.Menus;
            menuCategoryAddDto.OurServices = ourServices.Data?.OurServices;
            menuCategoryAddDto.Projects = project.Data?.Projects;
            menuCategoryAddDto.ProjectCategories = projectcategory.Data?.ProjectCategories;

            return View(menuCategoryAddDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var pages = await _pageService.GetAllByNonDeleteAndActive();
            var articleCategories = await _articleCategoryService.GetAllByNonDeleteAndActiveSite();
            var articles = await _articleService.GetAllByNonDeleteAndActiveSite();
            var menuList = await _menusService.GetAllByNonDeleteAndActive();
            var ourServices = await _ourServicesService.GetAllByNonDeleteAndActive();
            var projectcategory = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var project = await _projectService.GetAllByNonDeleteAndActive();
            var menuUpdateData = await _menusService.GetUpdateDto(id);

            var viewModel = new MyMenusViewModel
            {
                PageList = pages.Data.Page,
                ArticleCategories = articleCategories.Data.Categories,
                Articles = articles.Data.Articles,
                MenuList = menuList.Data.Menus,
                OurServices = ourServices.Data.OurServices,
                Projects = project.Data.Projects,
                ProjectCategories = projectcategory.Data.ProjectCategories,
                MenuUpdateData = menuUpdateData.Data
            };

            return View(viewModel);
        }


                [HttpPost]
        public async Task<IActionResult> Edit(MyMenusViewModel menuCategoryUpdateDto)
        {
            try
            {
                if (menuCategoryUpdateDto.MenuUpdateData != null)
                {
                    var result = await _menusService.Update(menuCategoryUpdateDto.MenuUpdateData, "Ramazan Yılmaz");
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result.Message ?? "Menü güncellenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var pages = await _pageService.GetAllByNonDeleteAndActive();
            var articleCategories = await _articleCategoryService.GetAllByNonDeleteAndActiveSite();
            var articles = await _articleService.GetAllByNonDeleteAndActiveSite();
            var menuList = await _menusService.GetAllByNonDeleteAndActive();
            var ourServices = await _ourServicesService.GetAllByNonDeleteAndActive();
            var projectcategory = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var project = await _projectService.GetAllByNonDeleteAndActive();

            menuCategoryUpdateDto.PageList = pages.Data?.Page;
            menuCategoryUpdateDto.ArticleCategories = articleCategories.Data?.Categories;
            menuCategoryUpdateDto.Articles = articles.Data?.Articles;
            menuCategoryUpdateDto.MenuList = menuList.Data?.Menus;
            menuCategoryUpdateDto.OurServices = ourServices.Data?.OurServices;
            menuCategoryUpdateDto.Projects = project.Data?.Projects;
            menuCategoryUpdateDto.ProjectCategories = projectcategory.Data?.ProjectCategories;

            return View(menuCategoryUpdateDto);
        }


        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            await _menusService.HardDelete(id);
            return Json(new { success = true });
        }


    }
}
