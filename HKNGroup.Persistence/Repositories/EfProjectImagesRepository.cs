using Microsoft.EntityFrameworkCore;

namespace HKNGroup.Data.Concrete.EntityFramework.Repositories
{
    public class EfProjectImagesRepository : EfEntityRepositoryBase<ProjectImages>, IProjectImagesRepository
    {
        public EfProjectImagesRepository(DbContext context) : base(context)
        {
        }
    }
}
