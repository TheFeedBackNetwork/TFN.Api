using Microsoft.AspNetCore.Http;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Mvc.Models.Enum;

namespace TFN.Api.Models.Interfaces
{
    public interface IResourceAuthorizationResponseModelFactory
    {
        ResourceAuthorizationResponseModel From(Post post, HttpContext caller, PrincipleType principleType);
        ResourceAuthorizationResponseModel From(Comment comment, HttpContext caller, PrincipleType principleType);
    }
}