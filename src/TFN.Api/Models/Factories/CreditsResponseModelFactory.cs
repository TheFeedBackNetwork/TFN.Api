using System;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class CreditsResponseModelFactory : ICreditsResponseModelFactory
    {
        public CreditsResponseModel From(Credits credits, string apiUrl)
        {
            return CreditsResponseModel.From(credits, apiUrl);
        }
    }
}