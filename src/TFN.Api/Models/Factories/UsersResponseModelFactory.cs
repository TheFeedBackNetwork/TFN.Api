using System;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class UsersResponseModelFactory : IUsersResponseModelFactory
    {
        public UserResponseModel From(User user, Credits credits, string apiUrl)
        {
            throw new NotImplementedException();
        }
    }
}