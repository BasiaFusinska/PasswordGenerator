using FluentAssertions;
using NSubstitute;
using PasswordGenerator;
using Xunit;

namespace PasswordGeneratorTests
{
    public class AccessTokenManagerUnitTests
    {
        private const string UserId = "12345";
        private const string Password = "12345Password";
        private const string WrongPassword = "12345WrongPassword";

        private readonly AccessTokenManagerUnitTests when;
        private readonly AccessTokenManagerUnitTests then;
        private readonly AccessTokenManagerUnitTests and;

        private readonly IAccessTokenGenerator _accessTokenGenerator = Substitute.For<IAccessTokenGenerator>();
        private readonly IAccessTokenRepository _accessTokenRepository = Substitute.For<IAccessTokenRepository>();
        private readonly AccessTokenManager _accessTokenManager;

        private string _userPassword;
        private bool _isValid;

        public AccessTokenManagerUnitTests()
        {
            _accessTokenManager = new AccessTokenManager(_accessTokenGenerator, _accessTokenRepository);
            when = then = and = this;
        }

        [Fact]
        public void should_generate_password_when_doesnt_exist_for_user()
        {
            when.password_exist_for_user(UserId, null);
            and.requesting_for_access_token(UserId);
            then.new_password_was_generated(UserId);
            and.password_added_to_repository(UserId);
        }

        private void password_exist_for_user(string userId, string password)
        {
            _accessTokenRepository.Retrieve(userId).Returns(password);
        }

        private void requesting_for_access_token(string userId)
        {
            _userPassword = _accessTokenManager.RequestAccessToken(userId);
        }

        private void new_password_was_generated(string userId)
        {
            _accessTokenGenerator.Received().GenerateAccessToken(userId);
        }

        private void password_added_to_repository(string userId)
        {
            _accessTokenRepository.Received().Add(userId, _userPassword);
        }

        [Fact]
        public void should_retrieve_password_when_exist_for_user()
        {
            when.password_exist_for_user(UserId, Password);
            and.requesting_for_access_token(UserId);
            then.password_was_retrieved(UserId);
        }

        private void password_was_retrieved(string userId)
        {
            _accessTokenRepository.Received().Retrieve(userId);
            _userPassword.Should().Be(Password);
        }

        [Fact]
        public void should_return_false_when_user_not_in_repository()
        {
            when.user_found_in_repository(UserId, null);
            and.verifying_user_and_access_token(UserId, Password);
            then.should_be_verified(false);
        }

        private void should_be_verified(bool isValid)
        {
            _isValid.Should().Be(isValid);
        }

        private void verifying_user_and_access_token(string userId, string password)
        {
            _isValid = _accessTokenManager.Verify(userId, password);
        }

        [Theory]
        [InlineData(WrongPassword, false)]
        [InlineData(Password, true)]
        public void should_verify_when_user_in_repository(string password, bool isValid)
        {
            when.user_found_in_repository(UserId, Password);
            when.verifying_user_and_access_token(UserId, password);
            then.should_be_verified(isValid);
        }

        private void user_found_in_repository(string userId, string password)
        {
            _accessTokenRepository.Retrieve(userId).Returns(password);
        }
    }
}
