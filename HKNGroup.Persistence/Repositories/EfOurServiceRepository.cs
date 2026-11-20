using Microsoft.EntityFrameworkCore;

namespace HKNGroup.Data.Concrete.EntityFramework.Repositories
{
    public class EfOurServiceRepository : EfEntityRepositoryBase<OurService>, IOurServiceRepository
    {
        public EfOurServiceRepository(DbContext context) : base(context)
        {
        }
    }
}
