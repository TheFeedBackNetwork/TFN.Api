
using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class LikeAuthorizationModel : LikeAuthorizationModel<Guid>
    {
        private LikeAuthorizationModel(Guid ownerId, Guid resourceId, Guid postOwnerId)
            : base(ownerId,resourceId,postOwnerId)
        {
            
        }
        public static LikeAuthorizationModel From(Like resource, Guid postOwnerId)
        {
            return new LikeAuthorizationModel(resource.UserId, resource.Id, postOwnerId);
        }
    }

    public class LikeAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey PostOwnerId { get; private set; }

        public LikeAuthorizationModel(TKey ownerId, TKey resourceId, TKey postOwnerId)
            : base(ownerId, resourceId)
        {
            PostOwnerId = postOwnerId;
        }
    }
}
