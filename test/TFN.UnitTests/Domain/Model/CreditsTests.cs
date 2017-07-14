using System;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class CreditsTests
    {
        const string Category = "Credits";

        private static Guid CreditsIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        private static string UsernameDefault = "FooBar";
        private static string NormalizedUsernameDefault = "FOOBAR";
        private static int TotalCreditsDefault = 10;
        private static DateTime CreatedDefault = new DateTime(2016, 6, 6, 6, 6, 6);
        private static DateTime ModifiedDefault = new DateTime(2016, 6, 6, 6, 6, 6);
        private static bool IsActiveDefault = true;

        public Credits make_Credits(Guid id, Guid userId, string username, string normalizedUsername, int totalCredits, DateTime created, DateTime modified, bool isActive)
        {
            return Credits.Hydrate(id,userId,username, normalizedUsername,totalCredits,created,modified,isActive);
        }

        public Credits make_Credits(string username)
        {
            return make_Credits(CreditsIdDefault,UserIdDefault,username,username?.ToUpperInvariant(),TotalCreditsDefault,CreatedDefault,ModifiedDefault,IsActiveDefault);
        }

        public Credits make_Credits(int totalCredits)
        {
            return make_Credits(CreditsIdDefault,UserIdDefault,UsernameDefault,NormalizedUsernameDefault,totalCredits,CreatedDefault,ModifiedDefault,IsActiveDefault);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentNullExceptionThrown(string username)
        {

            this.Invoking(x => x.make_Credits(username))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foobarfoobarfoobar")]
        [InlineData("fo")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentExceptionThrown(string username)
        {
            this.Invoking(x => x.make_Credits(username))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData(-11)]
        [InlineData(Int32.MinValue)]
        [Trait("Category", Category)]
        public void Constructor_InvalidCredits_ArgumentExceptionThrown(int credits)
        {
            this.Invoking(x => x.make_Credits(credits))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        [Trait("Category", Category)]
        public void ReduceCredits_NotEnoughCredits_InvalidOperationExceptionThrown()
        {
            var credits = make_Credits(UsernameDefault);

            this.Invoking(x => credits.ChangeTotalCredits(-(credits.TotalCredits + 1)))
                .ShouldThrow<InvalidOperationException>();
        }
    }
}
