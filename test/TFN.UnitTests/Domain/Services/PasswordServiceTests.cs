using FluentAssertions;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services.Cryptography;
using Xunit;

namespace TFN.UnitTests.Domain.Services
{
    public class PasswordServiceTests
    {
        const string Category = "PasswordService";
        private IPasswordService PasswordService { get; set; }
        public PasswordServiceTests()
        {
            PasswordService = new PasswordService();
        }

        [Theory]
        [InlineData("foobar123")]
        [InlineData("f123123")]
        [InlineData("fffff1")]
        [InlineData("1     f")]
        [Trait("Category", Category)]
        public void IsPasswordValid_PasswordIsValid_ReturnsTrue(string password)
        {
            PasswordService.ValidatePassword(password).Should().BeTrue();
        }

        [Theory]
        [InlineData("foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123foobar123")]
        [InlineData("111111")]
        [InlineData("aaaaaa")]
        [InlineData("      f1")]
        [InlineData("  f1  ")]
        [InlineData("f1         ")]
        [Trait("Category", Category)]
        public void IsPasswordValid_PasswordIsInvalid_ReturnsFalse(string password)
        {
            PasswordService.ValidatePassword(password).Should().BeFalse();
        }

        [Theory]
        [InlineData("foobar123")]
        [InlineData("f123123")]
        [InlineData("fffff1")]
        [InlineData("      f1")]
        [InlineData("1     f")]
        [InlineData("      f1")]
        [InlineData("  f1  ")]
        [Trait("Category", Category)]
        public void VerifyHashedPassword_PasswordIsValid_ReturnsTrue(string password)
        {
            var hash = PasswordService.HashPassword(password);
            PasswordService.VerifyHashedPassword(hash, password).Should().BeTrue();
        }
    }
}
