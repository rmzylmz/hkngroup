using HKNGroup.Persistence.Contexts;
using System.Threading.Tasks;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HKNGroupContext _context;
        private EfAdminRepository? _efAdminRepository;
        private EfSliderRepository? _efSliderRepository;
        private EfReferanceRepository? _efReferanceRepository;
        private EfPopupMessagesRepository? _efPopupMessagesRepository;
        private EfMessageRepository? _efMessageRepository;
        private EfSiteSettingsRepository? _efSiteSettingsRepository;
        private EfPageRepository? _efPageRepository;
        private EfArticleRepository? _efArticleRepository;
        private EfArticleCategoryRepository? _efArticleCategoryRepository;
        private EfArticleCommentRepository? _efArticleCommentRepository;
        private EfMenusRepository? _efMenuCategoryRepository;
        private EfOurServiceRepository? _efOurServiceRepository;
        private EfProjectRepository? _efProjectRepository;
        private EfProjectImagesRepository? _efProjectImagesRepository;
        private EfProjectCategoryRepository? _efProjectCategoryRepository;

        public UnitOfWork(HKNGroupContext context)
        {
            _context = context;
        }

        public IAdminRepository Admin => _efAdminRepository ??= new EfAdminRepository(_context);
        public IOurServiceRepository OurServices => _efOurServiceRepository ??= new EfOurServiceRepository(_context);
        public ISliderRepository Slider => _efSliderRepository ??= new EfSliderRepository(_context);
        public IReferanceRepository Referances => _efReferanceRepository ??= new EfReferanceRepository(_context);
        public IPopupMessagesRepository PopupMessages => _efPopupMessagesRepository ??= new EfPopupMessagesRepository(_context);
        public IMessageRepository Message => _efMessageRepository ??= new EfMessageRepository(_context);
        public ISiteSettingsRepository SiteSettings => _efSiteSettingsRepository ??= new EfSiteSettingsRepository(_context);
        public IPageRepository Page => _efPageRepository ??= new EfPageRepository(_context);
        public IArticleRepository Article => _efArticleRepository ??= new EfArticleRepository(_context);
        public IArticleCommentRepository ArticleComment => _efArticleCommentRepository ??= new EfArticleCommentRepository(_context);
        public IArticleCategoryRepository ArticleCategory => _efArticleCategoryRepository ??= new EfArticleCategoryRepository(_context);
        public IMenusRepository MenuCategory => _efMenuCategoryRepository ??= new EfMenusRepository(_context);       
        public IProjectCategoryRepository ProjectCategory => _efProjectCategoryRepository ??= new EfProjectCategoryRepository(_context);
        public IProjectRepository Project => _efProjectRepository ??= new EfProjectRepository(_context);
        public IProjectImagesRepository ProjectImages => _efProjectImagesRepository ??= new EfProjectImagesRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
