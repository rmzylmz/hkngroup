namespace HKNGroup.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAdminRepository Admin { get; }
        IOurServiceRepository OurServices { get; }
        ISliderRepository Slider { get; }
        IPopupMessagesRepository PopupMessages { get; }
        IReferanceRepository Referances { get; }
        IMessageRepository Message { get; }
        ISiteSettingsRepository SiteSettings { get; } 
        IPageRepository Page { get; }
       
        IArticleRepository Article { get; }
        IArticleCommentRepository ArticleComment { get; }
        IArticleCategoryRepository ArticleCategory { get; }
        IMenusRepository MenuCategory { get; }
        IProjectRepository Project { get; }
        IProjectCategoryRepository ProjectCategory { get; }
        IProjectImagesRepository ProjectImages { get; }

        Task<int> SaveAsync();
    }
}
