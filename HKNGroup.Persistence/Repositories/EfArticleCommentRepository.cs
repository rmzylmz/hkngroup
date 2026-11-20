using Microsoft.EntityFrameworkCore;
namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfArticleCommentRepository : EfEntityRepositoryBase<ArticleComment>, IArticleCommentRepository
    {
        public EfArticleCommentRepository(DbContext context) : base(context)
        {
        }
    }
}
