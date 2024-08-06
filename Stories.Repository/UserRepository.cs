using Stories.Domain;
using Stories.Domain.Interface;

namespace Stories.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task Add(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Story> GetStoriesByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Story GetStoryById(Guid userId, Guid storyId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
