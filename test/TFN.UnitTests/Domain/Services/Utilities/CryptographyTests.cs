using System;
using FluentAssertions;
using TFN.Domain.Services.Cryptography;
using Xunit;

namespace TFN.UnitTests.Domain.Services.Utilities
{
    public class CryptographyTests
    {
        const string Category = "CryptographyUtility";

        [Theory]
        [InlineData(-50)]
        [InlineData(0)]
        [Trait("Category", Category)]
        public void UrlSafeId_CreateUrlSafeUniqueId_ArgumentExceptionThrown(int urllength)
        {
            Action act = () => Cryptography.CreateUrlSafeUniqueId(urllength);

            act.ShouldThrow<ArgumentException>();
        }
    }
}
