using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class CommentResponseModelFactory : ICommentResponseModelFactory
    {
        public ICreditRepository CreditRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IResourceAuthorizationResponseModelFactory ResourceAuthorizationResponseModelFactory { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        public CommentResponseModelFactory(ICreditRepository creditRepository, ICommentRepository commentRepository,
            IResourceAuthorizationResponseModelFactory resourceAuthorizationResponseModelFactory, IHttpContextAccessor httpContextAccessor)
        {
            CreditRepository = creditRepository;
            CommentRepository = commentRepository;
            ResourceAuthorizationResponseModelFactory = resourceAuthorizationResponseModelFactory;
            HttpContextAccessor = httpContextAccessor;
        }
        public async Task<CommentResponseModel> From(Comment comment, string apiUrl)
        {
            var summary = await CommentRepository.GetCommentScoreSummaryAsync(comment.Id, 5,comment.Username);

            var credits = await CreditRepository.GetByUserId(comment.UserId);

            var authZ = ResourceAuthorizationResponseModelFactory.From(comment, HttpContextAccessor.HttpContext);

            var model = CommentResponseModel.From(comment, summary, credits, authZ, apiUrl);

            return model;
        }
    }
}