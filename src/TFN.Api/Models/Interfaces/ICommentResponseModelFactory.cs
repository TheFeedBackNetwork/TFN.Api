using System;
using System.Threading.Tasks;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface ICommentResponseModelFactory
    {
        Task<CommentResponseModel> From(Comment comment, Guid viewerUserId, string apiUrl);
    }
}