

namespace Stories.Domain.Interface
{
    public interface IUserRepository
    {
        Task Add(User user);
        void Update(User user);
        User GetById(Guid id);
        Story GetStoryById(Guid userId, Guid storyId);
        IEnumerable<Story> GetStoriesByUserId(Guid userId);
        void SaveChanges();
    }
}
