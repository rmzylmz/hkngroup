using Microsoft.EntityFrameworkCore;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfPageRepository : EfEntityRepositoryBase<Page>, IPageRepository
    {
        public EfPageRepository(DbContext context) : base(context)
        {
        }
    }


}
