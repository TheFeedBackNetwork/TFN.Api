using System;
using TFN.Domain.Architecture.Events;

namespace TFN.Domain.Architecture.Models
{
    public class DomainEvent<TKey> : IDomainEvent
    {
        private int hashCode;

        private TKey uniqueId;

        public TKey Id
        {
            get
            {
                return uniqueId;
            }
        }

        protected DomainEvent(TKey id)
        {
            if (id.Equals(default(TKey)))
            {
                throw new ArgumentOutOfRangeException(nameof(id), "The identifier cannot be equal to the default value of the type.");
            }

            uniqueId = id;
            hashCode = uniqueId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var entity = obj as DomainEvent<TKey>;

            if (entity == null)
            {
                return false;
            }
            else
            {
                return uniqueId.Equals(entity.Id);
            }
        }

        public static bool operator ==(DomainEvent<TKey> x, DomainEvent<TKey> y)
        {
            if ((object)x == null)
            {
                return (object)y == null;
            }

            if ((object)y == null)
            {
                return (object)x == null;
            }

            return x.Id.Equals(y.Id);
        }

        public static bool operator !=(DomainEvent<TKey> x, DomainEvent<TKey> y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}