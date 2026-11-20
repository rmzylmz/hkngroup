using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfSliderRepository : EfEntityRepositoryBase<Slider>, ISliderRepository
    {
        public EfSliderRepository(DbContext context) : base(context)
        {
        }
    }
}
