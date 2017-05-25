using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Interfaces
{
    public interface ITrackResponseModelFactory
    {
        TrackResponseModel From(Track track, string apiUrl);
    }
}