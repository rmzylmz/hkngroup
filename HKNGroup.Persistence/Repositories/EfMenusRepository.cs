using Microsoft.EntityFrameworkCore;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMenusRepository : EfEntityRepositoryBase<Menus>, IMenusRepository
    {
        public EfMenusRepository(DbContext context) : base(context)
        {
        }
    }
}
