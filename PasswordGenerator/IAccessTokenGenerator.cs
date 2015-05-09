namespace PasswordGenerator
{
    public interface IAccessTokenGenerator
    {
        string GenerateAccessToken(string userId);
    }
}
