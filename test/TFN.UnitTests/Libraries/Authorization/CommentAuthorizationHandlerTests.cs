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
    public class CommentAuthorizationHandlerTests
    {
        private const string Category = "CommentAuthorizationHandler";

        private static Guid CommentIdDefaault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid PostIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        private static IReadOnlyList<Score> ScoresDefault = new List<Score> { Score.Hydrate(new Guid("0d7e16cb-372e-4819-add2-79b3095625dc"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"), DateTime.UtcNow), Score.Hydrate(new Guid("e614380f-547c-4422-acb7-5a8020a16553"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f01fcda6"), DateTime.UtcNow) };
        private static Guid UserIdDefault = new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6");
        private static Guid InvalidUserIdDefault = new Guid("bfc1a143-4208-42bc-8542-a359d18b505a");
        private static string UsernameDefault = "FooBar";
        private static string TextDefault = "This foo is my bar";
        private static bool IsActiveDefault = true;
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        private static DateTime ModifiedDefault = new DateTime(2016, 4, 4, 5, 5, 5);

        public Comment make_Comment(Guid id, Guid userId, Guid postId, string username, string text, IReadOnlyList<Score> scores, bool isActive, DateTime created, DateTime modified)
        {
            return Comment.Hydrate(id, userId, postId, text, isActive, created, modified);
        }

        public Comment make_CommentDefault()
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, UsernameDefault, TextDefault, ScoresDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {new Claim("sub", UserIdDefault.ToString())}));
            var requirement = CommentOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement>{requirement}, user,authorizationModel );
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CommentOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = CommentOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CommentOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = CommentOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithNonResourceOwner_ShouldSucceed()
        {
            var resource = make_CommentDefault();
            var authorizationModel = CommentAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = CommentOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new CommentAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }
    }
}