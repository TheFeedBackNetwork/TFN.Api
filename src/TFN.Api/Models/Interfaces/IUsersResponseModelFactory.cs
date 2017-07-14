using System.Threading.Tasks;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface IUsersResponseModelFactory
    {
        Task<UserResponseModel> From(UserAccount user, string apiUrl);
    }
}