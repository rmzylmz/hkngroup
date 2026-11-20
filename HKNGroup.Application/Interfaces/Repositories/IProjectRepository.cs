using System.Linq.Expressions;

namespace HKNGroup.Application.Interfaces.Repositories
{
    public interface IProjectRepository : IEntityRepository<Project>
    {
        Task<IList<Project>> GetAllOrderByDescIdAsync(Expression<Func<Project, bool>>? predicate = null, params Expression<Func<Project, object?>>[] includeProperties);
        Task<IList<Project>> Get3MostReadAsync(Expression<Func<Project, bool>>? predicate = null, params Expression<Func<Project, object?>>[] includeProperties);
    }
}

