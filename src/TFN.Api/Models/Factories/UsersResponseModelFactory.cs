using System.Threading.Tasks;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class UsersResponseModelFactory : IUsersResponseModelFactory
    {
        public ICreditRepository CreditRepository { get; private set; }
        public UsersResponseModelFactory(ICreditRepository creditRepository)
        {
            CreditRepository = creditRepository;
        }
        public async  Task<UserResponseModel> From(UserAccount user, string apiUrl)
        {
            var credits = await CreditRepository.FindByUsername(user.Username);

            var model = UserResponseModel.From(user, credits, apiUrl);

            return model;
        }
    }
}