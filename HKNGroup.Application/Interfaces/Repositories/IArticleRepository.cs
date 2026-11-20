using System.Linq.Expressions;

namespace HKNGroup.Application.Interfaces.Repositories
{
    public interface IArticleRepository : IEntityRepository<Article>
    {
        Task<IList<Article>> GetAllOrderByDescIdAsync(Expression<Func<Article, bool>>? predicate = null, params Expression<Func<Article, object?>>[] includeProperties);
        Task<IList<Article>> Get3MostReadAsync(Expression<Func<Article, bool>>? predicate = null, params Expression<Func<Article, object?>>[] includeProperties);
    }
}

