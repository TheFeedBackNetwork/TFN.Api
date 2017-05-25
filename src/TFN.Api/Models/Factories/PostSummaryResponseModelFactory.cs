using System;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.Factories
{
    public class PostSummaryResponseModelFactory : IPostSummaryResponseModelFactory
    {
        public PostSummaryResponseModel From(PostSummary postSummary, Credits credits, string apiUrl)
        {
            return PostSummaryResponseModel.From(postSummary,credits,apiUrl);
        }
    }
}