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
    public class LikeAuthorizationHandlerTests
    {
        private const string Category = "LikeAuthorizationHandler";

        private static Guid OtherUserIdDefault => new Guid("38c6ba6a-ac08-4389-8112-727a7825b159");
        private static Guid LikeIdDefault = new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6");
        private static Guid PostIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        public string UsernameDefault = "FooMusic";
        public static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);

        public Like make_Like(Guid id, Guid postId, Guid userId, string username, DateTime created)
        {
            return Like.Hydrate(id, postId, userId, username, created);
        }

        public Like make_LikeDefault()
        {
            return make_Like(LikeIdDefault, PostIdDefault, UserIdDefault, UsernameDefault, CreatedDefault);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwnerAsNonCommentOwner_ShouldSucceed()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, OtherUserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = LikeOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwnerAsCommentOwner_ShouldFail()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, UserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = LikeOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, OtherUserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", OtherUserIdDefault.ToString()) }));
            var requirement = LikeOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwnerAsNonCommentOwner_ShouldSucceed()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, OtherUserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = LikeOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwnerAsCommentOwner_ShouldFail()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, UserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = LikeOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_LikeDefault();
            var authorizationModel = LikeAuthorizationModel.From(resource, OtherUserIdDefault);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", OtherUserIdDefault.ToString()) }));
            var requirement = LikeOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new LikeAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }
    }
}