using Microsoft.EntityFrameworkCore;

namespace HKNGroup.Data.Concrete.EntityFramework.Repositories
{
    public class EfProjectCategoryRepository : EfEntityRepositoryBase<ProjectCategory>, IProjectCategoryRepository
    {
        public EfProjectCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
