namespace PasswordGenerator
{
    public class Authentication
    {
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IAccessTokenRepository _accessTokenRepository;

        public Authentication(IAccessTokenGenerator accessTokenGenerator, IAccessTokenRepository accessTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _accessTokenRepository = accessTokenRepository;
        }

        public string RequestAccessToken(string userId)
        {
            var accessToken = _accessTokenRepository.Retrieve(userId);

            if (accessToken != null) return accessToken;
            
            accessToken = _accessTokenGenerator.GenerateAccessToken(userId);
            _accessTokenRepository.Add(userId, accessToken);

            return accessToken;
        }

        public bool Verify(string userId, string accessToken)
        {
            var savedAccessToken = _accessTokenRepository.Retrieve(userId);
            if (savedAccessToken == null)
                return false;

            return savedAccessToken == accessToken;
        }
    }
}
