using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.Factories
{
    public class TrackResponseModelFactory : ITrackResponseModelFactory
    {
        public TrackResponseModel From(Track track, string apiUrl)
        {
            return TrackResponseModel.From(track,apiUrl);
        }
    }
}