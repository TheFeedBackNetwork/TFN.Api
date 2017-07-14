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
    public class CommentResponseModelFactory : ICommentResponseModelFactory
    {
        public ICreditRepository CreditRepository { get; private set; }
        public IPostService PostService { get; private set; }
        public IResourceAuthorizationResponseModelFactory ResourceAuthorizationResponseModelFactory { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        public CommentResponseModelFactory(ICreditRepository creditRepository, IPostService postService,
            IResourceAuthorizationResponseModelFactory resourceAuthorizationResponseModelFactory, IHttpContextAccessor httpContextAccessor)
        {
            CreditRepository = creditRepository;
            PostService = postService;
            ResourceAuthorizationResponseModelFactory = resourceAuthorizationResponseModelFactory;
            HttpContextAccessor = httpContextAccessor;
        }
        public async Task<CommentResponseModel> From(Comment comment, Guid viewerUserId, string apiUrl)
        {
            var summary = await PostService.FindCommentScoreSummary(comment.Id, comment.UserId);

            var credits = await CreditRepository.FindByUserId(comment.UserId);

            var authZ = ResourceAuthorizationResponseModelFactory.From(comment, HttpContextAccessor.HttpContext);

            var model = CommentResponseModel.From(comment, summary, credits, authZ, apiUrl);

            return model;
        }
    }
}