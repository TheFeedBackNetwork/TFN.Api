using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TFN.Api.Extensions;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Factories
{
    public class PostResponseModelFactory : IPostResponseModelFactory
    {
        public ICreditRepository CreditRepository { get; private set; }
        public IPostRepository PostRepository { get; private set; }
        public IResourceAuthorizationResponseModelFactory ResourceAuthorizationResponseModelFactory { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }

        public PostResponseModelFactory(ICreditRepository creditRepository, IPostRepository postRepository,
            IResourceAuthorizationResponseModelFactory resourceAuthorizationResponseModelFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            CreditRepository = creditRepository;
            PostRepository = postRepository;
            ResourceAuthorizationResponseModelFactory = resourceAuthorizationResponseModelFactory;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<PostResponseModel> From(Post post, string apiUrl)
        {
            var summary = await PostRepository.GetPostLikeSummaryAsync(post.Id, 5,post.Username);

            var credits = await CreditRepository.FindByUserId(post.UserId);

            var authZ = ResourceAuthorizationResponseModelFactory.From(post, HttpContextAccessor.HttpContext);

            var model = PostResponseModel.From(post, summary, credits, authZ, apiUrl);

            return model;
        }
    }
}