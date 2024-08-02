
namespace Stories.Domain
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task SetAccessToken(string accessToken, string username);
        Task<User> GetAuthenticatedUser();
    }
}
