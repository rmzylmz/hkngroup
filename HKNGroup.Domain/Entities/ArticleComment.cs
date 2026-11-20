using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class ArticleComment : EntityBase, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}

