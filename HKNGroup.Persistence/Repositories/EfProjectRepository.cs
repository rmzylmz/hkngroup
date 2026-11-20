using HKNGroup.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HKNGroup.Data.Concrete.EntityFramework.Repositories
{
    public class EfProjectRepository : EfEntityRepositoryBase<Project>, IProjectRepository
    {
        public EfProjectRepository(DbContext context) : base(context)
        {
        }

        public async Task<IList<Project>> GetAllOrderByDescIdAsync(Expression<Func<Project, bool>>? predicate = null, params Expression<Func<Project, object?>>[] includeProperties)
        {
            IQueryable<Project> query = _context.Set<Project>();
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

        public async Task<IList<Project>> Get3MostReadAsync(Expression<Func<Project, bool>>? predicate = null, params Expression<Func<Project, object?>>[] includeProperties)
        {
            IQueryable<Project> query = _context.Set<Project>();
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

        private HKNGroupContext HKNGroupContext
        {
            get
            {
                return (_context as HKNGroupContext)!;
            }
        }
    }
}
