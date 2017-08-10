using System;
using System.Collections.Generic;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class PostTests
    {
        const string Category = "Post";
   
        private static Guid PostIdDefault = new Guid("86bcf89b-6847-4c5d-bcc5-87b69d775e3f");
        private static string PostUserNameDefault = "FooBar";
        private static string TrackUrlDefault = "www.soundcloud.com/foo/bar";
        private static string TrackNameDefault = "fooName";
        private static IReadOnlyList<string> TagsDefault = new List<string> { "foo", "bar" };
        private static Genre GenreDefault = Genre.Ambient;
        private static Guid UserIdDefault = new Guid("799dca00-ef0f-4f8e-9bd3-5a4cff9ee07e");
        private static IReadOnlyList<Comment> CommentsDefault = new List<Comment> {Comment.Hydrate(new Guid("60a7686c-b775-4508-b273-5e6d2cb09080"), new Guid("799dca00-ef0f-4f8e-9bd3-5a4cff9ee07e"),PostIdDefault,"foo bar baz",true,new DateTime(2016,5,5,5,5, 5), new DateTime(2016, 5, 5, 5, 5,5)) };
        private static string TextDefault = "This bar is my foo.";
        private static bool IsActiveDefault = true;
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        private static DateTime ModifiedDefault = new DateTime(2016, 4, 4, 5, 5, 5);

        public Post make_Post(Guid id, Guid userId,string username, string trackUrl, string trackName, string text, Genre genre, IReadOnlyList<string> tags,IReadOnlyList<Comment> comments,bool isActive,DateTime created, DateTime modified)
        {
            return Post.Hydrate(id, userId, trackUrl, trackName, text, genre, tags,isActive, created, modified);
        }

        public Post make_PostByTrackUrl(string trackUrl)
        {
            return make_Post(PostIdDefault, UserIdDefault,PostUserNameDefault, trackUrl, TrackNameDefault, TextDefault, GenreDefault, TagsDefault,CommentsDefault,IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_PostByText(string text)
        {
            return make_Post(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault,TrackNameDefault, text, GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_Post(int likes)
        {
            return make_Post(PostIdDefault, UserIdDefault, PostUserNameDefault, TrackUrlDefault, TrackNameDefault, TextDefault, GenreDefault, TagsDefault, CommentsDefault, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Post make_Post(DateTime created)
        {
            return Post.Hydrate(PostIdDefault, UserIdDefault, TrackUrlDefault, TrackNameDefault, TextDefault, GenreDefault, TagsDefault, IsActiveDefault, created, ModifiedDefault);
        }

        [Theory]
        [InlineData(10020)]
        [InlineData(1)]
        [Trait("Category", Category)]
        public void Constructor_InvalidCreated_ArgumentExceptionThrown(int extraSeconds)
        {
            var time = DateTime.UtcNow.AddMinutes(extraSeconds);

            this.Invoking(x => x.make_Post(time))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidText_ArgumentNullExceptionThrown(string text)
        {
            this.Invoking(x => x.make_PostByText(text))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foob")]
        [InlineData("foo")]
        [InlineData("a")]
        [Trait("Category", Category)]
        public void Constructor_InvalidText_ArgumentExceptionThrown(string text)
        {
            this.Invoking(x => x.make_PostByText(text))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5000)]
        [Trait("Category", Category)]
        public void Constructor_InvalidLikes_ArgumentExceptionThrown(int likes)
        {
            this.Invoking(x => x.make_Post(likes))
                .ShouldThrow<ArgumentException>();
        }
    }
}
