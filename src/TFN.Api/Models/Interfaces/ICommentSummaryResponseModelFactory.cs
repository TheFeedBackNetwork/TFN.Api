using System;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Interfaces
{
    public interface ICommentSummaryResponseModelFactory
    {
        CommentSummaryResponseModel From(CommentSummary commentSummary, Credits credits, Guid postId, string apiUrl);
    }
}