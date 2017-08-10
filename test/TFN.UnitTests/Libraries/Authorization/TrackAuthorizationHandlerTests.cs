using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using TFN.Api.Authorization.Handlers;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using Xunit;

namespace TFN.UnitTests.Libraries.Authorization
{
    public class TrackAuthorizationHandlerTests
    {
        private const string Category = "TrackAuthorizationHandler";

        private static Guid InvalidUserIdDefault => new Guid("38c6ba6a-ac08-4389-8112-727a7825b159");
        private static Uri LocationDefault = new Uri("http://lol.com/lol");
        private static Guid TrackIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static string TrackNameDefault = "foo";
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        public static TrackMetaData TrackMetaDataDefault = TrackMetaData.From(1, 1, 1, 1, 1, 1, 1);
        private static IReadOnlyList<int> SoundWaveDefault = Enumerable.Range(1, 4000).ToList();
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);

        public Track make_Track(Guid id, Guid userId, Uri location, string trackName, IReadOnlyList<int> soundWave, TrackMetaData metaData,
            DateTime created)
        {
            return Track.Hydrate(id, userId, location, soundWave, metaData, created);
        }

        public Track make_TrackDefault()
        {
            return make_Track(TrackIdDefault, UserIdDefault, LocationDefault, TrackNameDefault, SoundWaveDefault, TrackMetaDataDefault,
                CreatedDefault);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_TrackDefault();
            var authorizationModel = TrackAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = TrackOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new TrackAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_TrackDefault();
            var authorizationModel = TrackAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = TrackOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new TrackAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_TrackDefault();
            var authorizationModel = TrackAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = TrackOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new TrackAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_TrackDefault();
            var authorizationModel = TrackAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = TrackOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new TrackAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }
    }
}