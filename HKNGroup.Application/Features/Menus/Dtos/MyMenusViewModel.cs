namespace HKNGroup.Application.Features.Menus
{
    public class MyMenusViewModel
    {
        public IList<HKNGroup.Domain.Entities.Menus>? MenuList { get; set; }
        public MenusAddDto? MenuAddDto { get; set; }
        public MenusUpdateDto? MenuUpdateData { get; set; }
        public IList<Page>? PageList { get; set; }
        public IList<Article>? Articles { get; set; }
        public IList<ArticleCategory>? ArticleCategories { get; set; }
        public IList<OurService>? OurServices { get; set; }
        public IList<Project>? Projects { get; set; }
        public IList<ProjectCategory>? ProjectCategories { get; set; }
    }
}
