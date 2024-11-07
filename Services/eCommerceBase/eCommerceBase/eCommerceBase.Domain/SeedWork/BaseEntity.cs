using Newtonsoft.Json;

namespace eCommerceBase.Domain.SeedWork
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string? LanguageCode { get; set; } = "tr";
        int? _requestedHashCode;
        private readonly List<IObjectNotification> _domainEvents = [];
        [SwaggerIgnore]
        public IReadOnlyCollection<IObjectNotification>? DomainEvents => _domainEvents?.AsReadOnly();
        public void AddDomainEvent(IObjectNotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvents(IObjectNotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public bool IsTransient()
        {
            return Id == default;
        }
        public void SetId(Guid id)
        {
            Id = id;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not BaseEntity)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            BaseEntity item = (BaseEntity)obj;
            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }
        public static bool operator ==(BaseEntity? left, BaseEntity? right)
        {
            if (Object.Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }
        public static bool operator !=(BaseEntity? left, BaseEntity? right)
        {
            return !(left == right);
        }
    }
}
