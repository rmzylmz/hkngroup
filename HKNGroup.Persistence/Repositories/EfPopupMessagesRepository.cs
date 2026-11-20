using Microsoft.EntityFrameworkCore;

namespace HKNGroup.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfPopupMessagesRepository : EfEntityRepositoryBase<PopupMessages>, IPopupMessagesRepository
    {
        public EfPopupMessagesRepository(DbContext context) : base(context)
        {
        }
    }
}
