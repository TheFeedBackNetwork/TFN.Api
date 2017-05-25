using System;
using Microsoft.AspNetCore.Http;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Mvc.Models.Enum;

namespace TFN.Api.Models.Factories
{
    public class ResourceAuthorizationResponseModelFactory : IResourceAuthorizationResponseModelFactory
    {
        public ResourceAuthorizationResponseModel From(Post post, HttpContext caller)
        {
            return ResourceAuthorizationResponseModel.From(post,caller);
        }

        public ResourceAuthorizationResponseModel From(Comment comment, HttpContext caller)
        {
            return ResourceAuthorizationResponseModel.From(comment,caller);
        }
    }
}