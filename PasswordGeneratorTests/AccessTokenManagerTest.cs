using System;
using System.Threading;
using FluentAssertions;
using PasswordGenerator;
using Xunit;

namespace PasswordGeneratorTests
{
    public class AccessTokenManagerTest
    {
        private const string UserId = "12345";
        private const string Password = "12345Password";

        private readonly AccessTokenManagerTest when;
        private readonly AccessTokenManagerTest then;
        private readonly AccessTokenManagerTest and;

        private readonly Authentication _accessTokenManager;

        private bool _isValid;
        private string _userPassword;

        public AccessTokenManagerTest()
        {
            _accessTokenManager = new Authentication(new AccessTokenGenerator(), new AccessTokenRepository(TimeSpan.FromSeconds(10)));
            when = then = and = this;
        }

        [Fact]
        public void when_no_password_generated_should_not_be_valid()
        {
            when.verifying_user_and_access_token(UserId, Password);
            then.should_be_verified(false);
        }

        private void verifying_user_and_access_token(string userId, string password)
        {
            _isValid = _accessTokenManager.Verify(userId, password);
        }

        private void should_be_verified(bool isValid)
        {
            _isValid.Should().Be(isValid);
        }

        [Fact]
        public void when_password_generated_should_be_valid()
        {
            when.requesting_for_access_token(UserId);
            and.verifying_user_and_access_token(UserId, _userPassword);
            then.should_be_verified(true);
        }

        private void requesting_for_access_token(string userId)
        {
            _userPassword = _accessTokenManager.RequestAccessToken(userId);
        }

        [Fact]
        public void when_password_generated_and_wrong_should_not_be_valid()
        {
            when.requesting_for_access_token(UserId);
            and.verifying_user_and_access_token(UserId, Password);
            then.should_be_verified(false);
        }

        [Theory]
        [InlineData(5000, true)]
        [InlineData(10010, false)]
        public void when_password_generated_and_wait_should_be_validated(int miliseconds, bool isValid)
        {
            when.requesting_for_access_token(UserId);
            and.wait(miliseconds);
            and.verifying_user_and_access_token(UserId, _userPassword);
            then.should_be_verified(isValid);
        }

        private void wait(int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        [Theory]
        [InlineData(5000)]
        [InlineData(10010)]
        public void when_password_generated_and_wrong_and_wait_should_not_be_valid(int miliseconds)
        {
            when.requesting_for_access_token(UserId);
            and.wait(miliseconds);
            and.verifying_user_and_access_token(UserId, Password);
            then.should_be_verified(false);
        }
    }
}
