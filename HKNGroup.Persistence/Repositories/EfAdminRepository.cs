using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfAdminRepository : EfEntityRepositoryBase<Admin>, IAdminRepository
    {
        public EfAdminRepository(DbContext context) : base(context)
        {
        }
    }
}
