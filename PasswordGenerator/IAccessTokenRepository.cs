namespace PasswordGenerator
{
    public interface IAccessTokenRepository
    {
        string Retrieve(string userId);
        void Add(string userId, string accessToken);
    }
}
