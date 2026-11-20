using Microsoft.EntityFrameworkCore;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfSiteSettingsRepository : EfEntityRepositoryBase<SiteSetting>, ISiteSettingsRepository
    {
        public EfSiteSettingsRepository(DbContext context) : base(context)
        {
        }
    }
}
