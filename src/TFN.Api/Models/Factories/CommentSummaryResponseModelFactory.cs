using System;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Factories
{
    public class CommentSummaryResponseModelFactory : ICommentSummaryResponseModelFactory
    {
        public CommentSummaryResponseModel From(CommentSummary commentSummary, Credits credits, Guid postId, string apiUrl)
        {
            return  CommentSummaryResponseModel.From(commentSummary,credits,postId,apiUrl);
        }
    }
}