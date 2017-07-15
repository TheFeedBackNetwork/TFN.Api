using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class PostResponseModelFactory : IPostResponseModelFactory
    {
        public ICreditRepository CreditRepository { get; private set; }
        public IPostService PostService { get; private set; }
        public IResourceAuthorizationResponseModelFactory ResourceAuthorizationResponseModelFactory { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }

        public PostResponseModelFactory(ICreditRepository creditRepository, IPostService postService,
            IResourceAuthorizationResponseModelFactory resourceAuthorizationResponseModelFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            CreditRepository = creditRepository;
            PostService = postService;
            ResourceAuthorizationResponseModelFactory = resourceAuthorizationResponseModelFactory;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<PostResponseModel> From(Post post, Guid viewUserId, string apiUrl)
        {
            var summary = await PostService.FindPostLikeSummary(post.Id,viewUserId);

            var credits = await CreditRepository.FindByUserId(post.UserId);

            var authZ = ResourceAuthorizationResponseModelFactory.From(post, HttpContextAccessor.HttpContext);

            var model = PostResponseModel.From(post, summary, credits, authZ, apiUrl);

            return model;
        }
    }
}