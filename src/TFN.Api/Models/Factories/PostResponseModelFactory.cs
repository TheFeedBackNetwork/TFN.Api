using System;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Factories
{
    public class PostResponseModelFactory : IPostResponseModelFactory
    {
        public PostResponseModel From(Post post, PostSummary summary, Credits credits, ResourceAuthorizationResponseModel authZmodel, string apiUrl)
        {
            throw new NotImplementedException();
        }
    }
}