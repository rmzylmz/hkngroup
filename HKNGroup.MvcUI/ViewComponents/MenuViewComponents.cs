using HKNGroup.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class MenuViewComponents : ViewComponent
    {
        private readonly HKNGroupContext _dbContext;

        public MenuViewComponents(HKNGroupContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            List<MenuItemDto> menuItems = GetMenuItems();
            return View(menuItems.OrderBy(x => x.MenuRow).ToList());
        }

        public List<MenuItemDto> GetMenuItems()
        {
            List<Menus> menuCategories = _dbContext.Menus.ToList();
            List<ProjectCategory> projectCategories = _dbContext.ProjectCategory.ToList();
            List<OurService> services = _dbContext.OurServices.ToList();
            List<ArticleCategory> articleCategories = _dbContext.ArticleCategories.ToList();

            List<MenuItemDto> menuItems = new List<MenuItemDto>();

            foreach (var category in menuCategories.Where(x => x.ParentId == 0).OrderBy(x => x.MenuRow))
            {
                MenuItemDto menuItem = new MenuItemDto
                {
                    Id = category.Id,
                    ParentId = category.ParentId,
                    Name = category.Name ?? string.Empty,
                    Url = category.LinkPage ?? string.Empty,
                    YanSekme = category.YanSekme,
                    AutomaticListing = category.AutomaticListing,
                    Children = GetSubMenuItems(category.Id, menuCategories)
                };

                menuItems.Add(menuItem);
            }

            var menuWithAutoListing = menuItems.FirstOrDefault(x => x.AutomaticListing == 3);

            if (menuWithAutoListing != null)
            {
                menuWithAutoListing.Children ??= new List<MenuItemDto>();
                foreach (var projectCategory in projectCategories)
                {
                    MenuItemDto projectMenuItem = new MenuItemDto
                    {
                        Id = projectCategory.Id,
                        ParentId = menuWithAutoListing.Id,
                        Name = projectCategory.Name,
                        YanSekme = menuWithAutoListing.YanSekme,
                        Url = RoutingEnum.ProjeKategori + "/" + SeoHelper.ToSeoUrl(projectCategory.Name)
                    };

                    menuWithAutoListing.Children.Add(projectMenuItem);
                }
            }

            menuWithAutoListing = menuItems.FirstOrDefault(x => x.AutomaticListing == 5);
            if (menuWithAutoListing != null)
            {
                menuWithAutoListing.Children ??= new List<MenuItemDto>();
                foreach (var itemServices in services)
                {
                    MenuItemDto ServicesMenuItem = new MenuItemDto
                    {
                        Id = itemServices.Id,
                        ParentId = menuWithAutoListing.Id,
                        Name = itemServices.Title,
                        YanSekme = menuWithAutoListing.YanSekme,
                        Url = RoutingEnum.Hizmet + "/" + SeoHelper.ToSeoUrl(itemServices.Title)
                    };

                    menuWithAutoListing.Children.Add(ServicesMenuItem);
                }
            }

            menuWithAutoListing = menuItems.FirstOrDefault(x => x.AutomaticListing == 7);
            if (menuWithAutoListing != null)
            {
                menuWithAutoListing.Children ??= new List<MenuItemDto>();
                foreach (var itemArticle in articleCategories)
                {
                    MenuItemDto ArticleMenuItem = new MenuItemDto
                    {
                        Id = itemArticle.Id,
                        ParentId = menuWithAutoListing.Id,
                        Name = itemArticle.Name,
                        YanSekme = menuWithAutoListing.YanSekme,
                        Url = RoutingEnum.HaberKategori + "/" + SeoHelper.ToSeoUrl(itemArticle.Name)
                    };

                    menuWithAutoListing.Children.Add(ArticleMenuItem);
                }
            }

            return menuItems;
        }

        private List<MenuItemDto> GetSubMenuItems(int? parentId, List<Menus> menuCategories)
        {
            if (parentId == null)
            {
                return new List<MenuItemDto>();
            }

            List<MenuItemDto> subMenuItems = new List<MenuItemDto>();

            foreach (var category in menuCategories.Where(x => x.ParentId == parentId))
            {
                MenuItemDto subMenuItem = new MenuItemDto
                {
                    Id = category.Id,
                    ParentId = category.ParentId,
                    Name = category.Name ?? string.Empty,
                    Url = category.LinkPage ?? string.Empty,
                    YanSekme = category.YanSekme,
                    Children = GetSubMenuItems(category.Id, menuCategories)
                };

                subMenuItems.Add(subMenuItem);
            }

            return subMenuItems;
        }
    }
}
