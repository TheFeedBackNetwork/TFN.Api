﻿using System;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class TransientUserTests
    {
        const string Category = "TransientUsers";

        private static Guid TransientUserIdDefault = new Guid("0d7e16cb-372e-4819-add2-79b3095625dc");
        private static string UsernameDefault = "foomusic";
        private static string NormalizedUsernameDefault = "FOOMUSIC";
        private static string EmailDefault = "foo@bar.com";
        private static string NormalizedEmailDefault = "FOO@BAR.COM";
        private static string VerificationKeyDefault = "abcdefgABCDEFG";
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);
        private static DateTime ModifiedDefault = new DateTime(2016, 4, 4, 5, 5, 5);

        public TransientUserAccount make_TransientUser(Guid id, string username, string normalizedUsername, string email,
            string normalizedEmail, string emailVerificationKey, DateTime created, DateTime modifed)
        {
            return TransientUserAccount.Hydrate(id, username, normalizedUsername, email, normalizedEmail, emailVerificationKey, created,modifed);
        }

        public TransientUserAccount make_TransientUser(string username)
        {
            return make_TransientUser(TransientUserIdDefault,username,username?.ToUpperInvariant(),EmailDefault, NormalizedEmailDefault,VerificationKeyDefault, CreatedDefault, ModifiedDefault);
        }

        public TransientUserAccount make_TransientUserByNormalizedUsername(string normalizedUsername)
        {
            return make_TransientUser(TransientUserIdDefault,UsernameDefault,normalizedUsername,EmailDefault,NormalizedUsernameDefault,VerificationKeyDefault, CreatedDefault, ModifiedDefault);
        }

        public TransientUserAccount make_TransientUserByEmail(string email)
        {
            return make_TransientUser(TransientUserIdDefault,UsernameDefault,NormalizedUsernameDefault,email,email?.ToUpperInvariant(),VerificationKeyDefault, CreatedDefault, ModifiedDefault);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentNullExceptionThrown(string username)
        {

            this.Invoking(x => x.make_TransientUser(username))
                .ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData("foobarfoobarfoobar")]
        [InlineData("fo")]
        [Trait("Category", Category)]
        public void Constructor_InvalidUserName_ArgumentExceptionThrown(string username)
        {
            this.Invoking(x => x.make_TransientUser(username))
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
            this.Invoking(x => x.make_TransientUserByEmail(email))
                .ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("flknIOFD")]
        [InlineData("alkngfjlkan")]
        [InlineData("sglksdngsd")]
        [Trait("Category", Category)]
        public void Constructor_InvalidNormalizedUsername_ArgumentExceptionThrown(string normalizedUsername)
        {
            this.Invoking(x => x.make_TransientUserByNormalizedUsername(normalizedUsername))
                .ShouldThrow<ArgumentException>();
        }
    }
}