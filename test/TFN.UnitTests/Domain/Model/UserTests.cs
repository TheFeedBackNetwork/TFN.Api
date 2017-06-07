using System;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class UserTests
    {
        const string Category = "Users";

        private static Guid UserIdDefault = new Guid("0d7e16cb-372e-4819-add2-79b3095625dc");
        private static string UsernameDefault = "foomusic";
        private static string NormalizedUsernameDefault = "FOOMUSIC";
        private static string ProfilePictureUrlDefault = "tfn.foo.bar/picture/foo.png";
        private static string EmailDefault = "foo@bar.com";
        private static string NormalizedEmailDefault  = "FOO@BAR.COM";
        private static string GivenNameDefault = "foo";
        private static string FamilyNameDefault = "bar";
        public static Biography BiographyDefault = Biography.From("FooBar", "www.instagram.com/foo", "www.soundcloud.com/bar", "www.twitter.com/baz", "www.youtube.com/bo", "www.facebook.com/bing", "yourmomshouse");
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        public static bool IsActiveDefault = true;

        public User make_User(Guid id, string username, string normalizedUsername, string profilePictureUrl, string email,string normalizedEmail, string givenName, string familyName, Biography biography, DateTime created)
        {
            return User.Hydrate(id, username,normalizedUsername, profilePictureUrl, email, normalizedEmail, givenName, biography, created, IsActiveDefault);
        }

        public User make_UserByUsername(string username)
        {
            return make_User(UserIdDefault, username, username?.ToUpperInvariant(), ProfilePictureUrlDefault, EmailDefault, NormalizedEmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByNormalizedUsername(string normalizedUsername)
        {
            return make_User(UserIdDefault, UsernameDefault, normalizedUsername, ProfilePictureUrlDefault, EmailDefault, NormalizedEmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByProfilePictureUrl(string profilePictureUrl)
        {
            return make_User(UserIdDefault, UsernameDefault, NormalizedUsernameDefault, profilePictureUrl, EmailDefault,NormalizedEmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByEmail(string email)
        {
            return make_User(UserIdDefault, UsernameDefault, NormalizedUsernameDefault, ProfilePictureUrlDefault, email, NormalizedEmailDefault, GivenNameDefault, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByGivenName(string givenName)
        {
            return make_User(UserIdDefault, UsernameDefault,NormalizedUsernameDefault, ProfilePictureUrlDefault, EmailDefault, NormalizedEmailDefault , givenName, FamilyNameDefault, BiographyDefault, CreatedDefault);
        }

        public User make_UserByFamilyName(string familyName)
        {
            return make_User(UserIdDefault, UsernameDefault, NormalizedUsernameDefault,  ProfilePictureUrlDefault, EmailDefault, NormalizedEmailDefault, GivenNameDefault, familyName, BiographyDefault, CreatedDefault);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentNullExceptionThrown(string username)
        {

            this.Invoking(x => x.make_UserByUsername(username))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foobarfoobarfoobar")]
        [InlineData("fo")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentExceptionThrown(string username)
        {
            this.Invoking(x => x.make_UserByUsername(username))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("@foo")]
        [InlineData("bar@")]
        [InlineData("@bar.com")]
        [InlineData(" @ ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidEmail_ArgumentExceptionThrown(string email)
        {
            this.Invoking(x => x.make_UserByEmail(email))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("flknIOFD")]
        [InlineData("alkngfjlkan")]
        [InlineData("sglksdngsd")]
        [Trait("Category", Category)]
        public void Constructor_InvalidNormalizedUsername_ArgumentExceptionThrown(string normalizedUsername)
        {
            this.Invoking(x => x.make_UserByNormalizedUsername(normalizedUsername))
                .ShouldThrow<ArgumentException>();
        }

    }
}
