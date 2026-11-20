using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public EfArticleRepository(DbContext context) : base(context)
        {
        }

        public async Task<IList<Article>> GetAllOrderByDescIdAsync(Expression<Func<Article, bool>>? predicate = null, params Expression<Func<Article, object?>>[] includeProperties)
        {
            IQueryable<Article> query = _context.Set<Article>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IList<Article>> Get3MostReadAsync(Expression<Func<Article, bool>>? predicate = null, params Expression<Func<Article, object?>>[] includeProperties)
        {
            IQueryable<Article> query = _context.Set<Article>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.OrderByDescending(x => x.ViewsCount).Take(3).ToListAsync();
        }
    }
}
