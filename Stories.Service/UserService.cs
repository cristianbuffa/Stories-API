using Stories.Domain;
using Stories.Domain.Exceptions;
using Stories.Domain.Interface;
using Stories.Domain.Validation;

namespace Stories.Service
{
    public class UserService : IUserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", AccessToken="" }
            ,
            new User { Id = 2, FirstName = "cristian", LastName = "buffa", Username = "cbuffa", Password = "123456",  AccessToken="Y2J1ZmZhOjEyMzQ1Ng=="  }
        };

        

        public async Task<User> Authenticate(string username, string password)
        {

            // wrapped in "await Task.Run" to mimic fetching user from a db
            var _user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password)!);
            
            if (_user == null)
                throw new ApiUnAuthorizeException(ValidationMessage.
               General("UnAuthorize", "UnAuthorize request.", null));
            return _user;   
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _users.ToList()) ;
        }

        public async Task<User> GetAuthenticatedUser()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _users.FirstOrDefault()!);
        }

        public async Task SetAccessToken(string accessToken, string username)
        {   

            var _user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username));

            if (_user == null)
                throw new ApiUnAuthorizeException(ValidationMessage.
               General("UnAuthorize", "UnAuthorize request.", null));
            _user.AccessToken = accessToken;    
        }

    }
}