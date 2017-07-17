using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using TFN.Api.Authorization.Handlers;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using Xunit;

namespace TFN.UnitTests.Libraries.Authorization
{
    public class PostAuthorizationHandlerTests
    {
        private const string Category = "PostAuthorizationHandler";

        private static Guid InvalidUserIdDefault => new Guid("38c6ba6a-ac08-4389-8112-727a7825b159");
        private static Guid PostIdDefault = new Guid("86bcf89b-6847-4c5d-bcc5-87b69d775e3f");
        private static string PostUserNameDefault = "FooBar";
        private static string TrackUrlDefault = "http://soundcloud.com/foo/bar";
        private static IReadOnlyList<string> TagsDefault = new List<string> { "foo", "bar" };
        private static Genre GenreDefault = Genre.Ambient;
        private static Guid UserIdDefault = new Guid("799dca00-ef0f-4f8e-9bd3-5a4cff9ee07e");
        private static IReadOnlyList<Comment> CommentsDefault = new List<Comment> { Comment.Hydrate(new Guid("60a7686c-b775-4508-b273-5e6d2cb09080"), new Guid("799dca00-ef0f-4f8e-9bd3-5a4cff9ee07e"), PostIdDefault, "foo bar baz", true, new DateTime(2016, 5, 5, 5, 5, 5), new DateTime(2016, 5, 5, 5, 5, 5)) };
        private static string TextDefault = "This bar is my foo.";
        private static bool IsActiveDefault = true;
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        private static DateTime ModifiedDefault = new DateTime(2016, 4, 4, 5, 5, 5);

        public Post make_Post(Guid id, Guid userId, string username, string trackUrl, string text, Genre genre, IReadOnlyList<string> tags, IReadOnlyList<Comment> comments, bool isActive, DateTime created, DateTime modified)
        {
            return Post.Hydrate(id, userId, trackUrl, text, genre, tags, isActive, created, modified);
        }

        public Post make_PostDefault()
        {
            return make_Post(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault, TextDefault,
                GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = PostOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = PostOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = PostOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleWrite_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = PostOperations.Write;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }


        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = PostOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = PostOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            authorizationContext.HasSucceeded.Should().BeFalse();
        }
    }
}