using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface ICreditsResponseModelFactory
    {
        CreditsResponseModel From(Credits credits, string apiUrl);
    }
}