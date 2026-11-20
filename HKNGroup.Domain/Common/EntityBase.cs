namespace HKNGroup.Domain.Common
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int SiteId { get; set; } = 0;

        public virtual void MarkAsDeleted()
        {
            IsDeleted = true;
            ModifiedTime = DateTime.UtcNow;
        }

        public virtual void Restore()
        {
            IsDeleted = false;
            ModifiedTime = DateTime.UtcNow;
        }

        public virtual void SetActive(bool isActive)
        {
            IsActive = isActive;
            ModifiedTime = DateTime.UtcNow;
        }

        public virtual void Touch()
        {
            ModifiedTime = DateTime.UtcNow;
        }
    }
}
