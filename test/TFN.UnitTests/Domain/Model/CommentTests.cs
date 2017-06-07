using System;
using System.Collections.Generic;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class CommentTests
    {
        const string Category = "Comment";

        private static Guid CommentIdDefaault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid PostIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        private static IReadOnlyList<Score> ScoresDefault = new List<Score> { Score.Hydrate(new Guid("0d7e16cb-372e-4819-add2-79b3095625dc"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6"),"FooBar", DateTime.UtcNow) , Score.Hydrate(new Guid("e614380f-547c-4422-acb7-5a8020a16553"), new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836"), new Guid("3d17d22b-9b76-4b2a-aecd-5937f01fcda6"), "FooBarBaz", DateTime.UtcNow) };
        private static Guid UserIdDefault = new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6");
        private static string UsernameDefault = "FooBar";
        private static string TextDefault = "This foo is my bar";
        private static bool IsActiveDefault = true;
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        private static DateTime ModifiedDefault = new DateTime(2016, 4, 4, 5, 5, 5);

        public Comment make_Comment(Guid id, Guid userId, Guid postId,string username, string text, IReadOnlyList<Score> scores,bool isActive, DateTime created, DateTime modified)
        {
            return Comment.Hydrate(id, userId, postId,username, text,isActive, created, modified);
        }

        public Comment make_Comment(string text)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault,UsernameDefault, text, ScoresDefault,IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(IReadOnlyList<Score> scores)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, UsernameDefault, TextDefault, scores, IsActiveDefault, CreatedDefault, ModifiedDefault);
        }

        public Comment make_Comment(DateTime created)
        {
            return make_Comment(CommentIdDefaault, UserIdDefault, PostIdDefault, UsernameDefault, TextDefault, ScoresDefault, IsActiveDefault, created, ModifiedDefault);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(1)]
        [Trait("Category", Category)]
        public void Constructor_InvalidCreated_ArgumentExceptionThrown(int extraSeconds)
        {
            var time = DateTime.UtcNow.AddMinutes(extraSeconds);

            this.Invoking(x => x.make_Comment(time))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidText_ArgumentNullExceptionThrown(string text)
        {
            this.Invoking(x => x.make_Comment(text))
                .ShouldThrow<ArgumentNullException>();
        }
    }
}
