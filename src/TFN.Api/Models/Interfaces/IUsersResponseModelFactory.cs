using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface IUsersResponseModelFactory
    {
        UserResponseModel From(User user, Credits credits, string apiUrl);
    }
}