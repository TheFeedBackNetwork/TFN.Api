using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using TFN.Api.Authorization.Handlers;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTests.Libraries.Authorization
{
    public class CreditsAuthorizationHandlerTests
    {
        private const string Category = "CreditsAuthorizationHandler";

        private static Guid CreditsIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        private static Guid InvalidUserIdDefault = new Guid("bfc1a143-4208-42bc-8542-a359d18b505a");
        private static string UsernameDefault = "FooBar";
        private static string NormalizedUsernameDefault = "FOOBAR";
        private static int TotalCreditsDefault = 10;
        private static DateTime CreatedDefault = new DateTime(2016, 6, 6, 6, 6, 6);
        private static DateTime ModifiedDefault = new DateTime(2016, 6, 6, 6, 6, 6);
        private static bool IsActiveDefault = true;

        public Credits make_Credits(Guid id, Guid userId, string username, string normalizedUsername, int totalCredits, DateTime created, DateTime modified, bool isActive)
        {
            return Credits.Hydrate(id, userId, username, normalizedUsername, totalCredits, created, modified, isActive);
        }

        public Credits make_CreditsDefault()
        {
            return make_Credits(CreditsIdDefault, UserIdDefault, UsernameDefault, NormalizedUsernameDefault,
                TotalCreditsDefault, CreatedDefault, ModifiedDefault, IsActiveDefault);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleRead_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Read;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleRead_WhenCalledWithNonResourceOwner_ShouldSucceed()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Read;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithNonResourceOwner_ShouldSucceed()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_CreditsDefault();
            var authorizationModel = CreditsAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CreditsOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CreditsAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }


    }
}