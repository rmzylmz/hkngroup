using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfReferanceRepository : EfEntityRepositoryBase<Referance>, IReferanceRepository
    {
        public EfReferanceRepository(DbContext context) : base(context)
        {
        }
    }
}
