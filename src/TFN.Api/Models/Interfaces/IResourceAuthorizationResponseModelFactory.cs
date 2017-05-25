using Microsoft.AspNetCore.Http;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface IResourceAuthorizationResponseModelFactory
    {
        ResourceAuthorizationResponseModel From(Post post, HttpContext caller);
        ResourceAuthorizationResponseModel From(Comment comment, HttpContext caller);
    }
}