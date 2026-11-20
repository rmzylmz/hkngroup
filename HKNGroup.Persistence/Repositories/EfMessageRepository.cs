using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMessageRepository : EfEntityRepositoryBase<Message>, IMessageRepository
    {
        public EfMessageRepository(DbContext context) : base(context)
        {
        }
    }
}
