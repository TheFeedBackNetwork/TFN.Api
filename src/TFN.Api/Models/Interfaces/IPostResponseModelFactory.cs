using System.Threading.Tasks;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Interfaces
{
    public interface IPostResponseModelFactory
    {
        Task<PostResponseModel> From(Post post, string apiUrl);
    }
}