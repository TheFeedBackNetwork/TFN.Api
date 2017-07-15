﻿using Microsoft.AspNetCore.Http;
using TFN.Api.Extensions;
using TFN.Domain.Models.Entities;
using TFN.Mvc.Models.Enum;

namespace TFN.Api.Models.ResponseModels
{
    public class ResourceAuthorizationResponseModel
    {
        public bool CanRead { get; private set; }
        public bool CanAdd { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }

        private ResourceAuthorizationResponseModel(bool canRead, bool canAdd, bool canEdit, bool canDelete)
        {
            CanAdd = canAdd;
            CanDelete = canDelete;
            CanEdit = canEdit;
            CanRead = canRead;
        }
        public static ResourceAuthorizationResponseModel From(Post post, HttpContext caller)
        {
            var principle = caller.GetCaller();

            if (principle.Equals(PrincipleType.Anonymous))
            {
                return new ResourceAuthorizationResponseModel(true,false,false,false);
            }
            if (principle.Equals(PrincipleType.User))
            {
                if (caller.GetUserId() == post.UserId)
                {
                    return new ResourceAuthorizationResponseModel(true,true,true,true);
                }
                return new ResourceAuthorizationResponseModel(true,false,false,false);
            }
            return new ResourceAuthorizationResponseModel(false, false, false, false);
        }

        public static ResourceAuthorizationResponseModel From(Comment comment, HttpContext caller)
        {
            var principle = caller.GetCaller();

            if (principle.Equals(Mvc.Models.Enum.PrincipleType.Anonymous))
            {
                return new ResourceAuthorizationResponseModel(true, false, false, false);
            }
            if (principle.Equals(PrincipleType.User))
            {
                if (caller.GetUserId() == comment.UserId)
                {
                    return new ResourceAuthorizationResponseModel(true, true, true, true);
                }
                return new ResourceAuthorizationResponseModel(true, false, false, false);
            }
            return new ResourceAuthorizationResponseModel(false, false, false, false);
        }
    }
}
