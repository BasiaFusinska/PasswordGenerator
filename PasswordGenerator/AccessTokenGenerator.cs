using System;

namespace PasswordGenerator
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        public string GenerateAccessToken(string userId)
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
