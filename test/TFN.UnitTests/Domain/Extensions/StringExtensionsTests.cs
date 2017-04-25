﻿using FluentAssertions;
using TFN.Domain.Models.Extensions;
using Xunit;

namespace TFN.UnitTests.Domain.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(" ")]
        [InlineData(".com")]
        [InlineData("google.")]
        [InlineData("foobar")]
        [InlineData("foo.bar")]
        [InlineData("http://.com")]
        public void Url_CheckInvalidUrl_ReturnsFalse(string testValue)
        {
            testValue.IsUrl().Should().Be(false);
        }

        [Theory]
        [InlineData("http://www.foo.com")]
        [InlineData("http://foo.ac.za")]
        [InlineData("https://foo.ac.za")]
        [InlineData("https://lol.com/this.png")]
        [InlineData("http://i.imgur.com/Rum0zSz.gifv")]
        public void Url_CheckValidUrl_ReturnsTrue(string testValue)
        {
            testValue.IsUrl().Should().Be(true);
        }

        [Theory]
        [InlineData("foo@bar.com")]
        [InlineData("foo@bar")]
        public void Email_CheckValidEmail_ReturnsTrue(string testValue)
        {
            testValue.IsEmail().Should().Be(true);
        }

        [Theory]
        [InlineData("@bar.com")]
        [InlineData(" @  ")]
        [InlineData("foo@")]
        public void Email_CheckValidEmail_ReturnsFalse(string testValue)
        {
            testValue.IsEmail().Should().Be(false);
        }
    }
}
