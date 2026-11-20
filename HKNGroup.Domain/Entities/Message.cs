using HKNGroup.Domain.Common;

namespace HKNGroup.Domain.Entities
{
    public class Message : EntityBase, IEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}

