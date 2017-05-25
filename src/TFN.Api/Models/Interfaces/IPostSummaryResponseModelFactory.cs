using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Interfaces
{
    public interface IPostSummaryResponseModelFactory
    {
        PostSummaryResponseModel From(PostSummary postSummary, Credits credits, string apiUrl);
    }
}