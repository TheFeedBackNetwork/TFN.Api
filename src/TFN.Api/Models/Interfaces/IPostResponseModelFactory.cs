using System;
using System.Threading.Tasks;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface IPostResponseModelFactory
    {
        Task<PostResponseModel> From(Post post, Guid viewUserId, string apiUrl);
    }
}