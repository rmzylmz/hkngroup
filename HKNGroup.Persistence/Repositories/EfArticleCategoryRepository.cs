using Microsoft.EntityFrameworkCore;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfArticleCategoryRepository : EfEntityRepositoryBase<ArticleCategory>, IArticleCategoryRepository
    {
        public EfArticleCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
