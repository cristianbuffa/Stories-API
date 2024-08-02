using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Domain
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
