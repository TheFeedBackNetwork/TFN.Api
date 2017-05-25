using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Interfaces
{
    public interface IPostResponseModelFactory
    {
        PostResponseModel From(Post post, PostSummary summary, Credits credits, ResourceAuthorizationResponseModel authZmodel, string apiUrl);
    }
}