using FluentAssertions;
using System;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class ScoreTests
    {
        /*const string Category = "Score";

        private static Guid ScoreIdDefault = new Guid("3d17d22b-9b76-4b2a-aecd-5937f018cda6");
        private static Guid CommentIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        public string UsernameDefault = "FooMusic";
        public static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);

        public Score make_Score(Guid id, Guid commentId, Guid userId, string username, DateTime created)
        {
            return Score.Hydrate(id, commentId, userId,created);
        }

        public Score make_Score(string username)
        {
            return make_Score(ScoreIdDefault,CommentIdDefault,UserIdDefault,username,CreatedDefault);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidText_ArgumentNullExceptionThrown(string text)
        {
            this.Invoking(x => x.make_Score(text))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("ikdskjdkdsklndsgklndsgklndsagasfdasfasfasf")]
        [InlineData("a")]
        [Trait("Category", Category)]
        public void Constructor_InvalidText_ArgumentExceptionThrown(string text)
        {
            this.Invoking(x => x.make_Score(text))
                .ShouldThrow<ArgumentException>();
        }*/


    }
}
