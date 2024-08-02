using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Domain
{
    public interface IStoryRepository
    {
        Task<List<GetStoryDetailsResponse?>> GetStoriesAsync(GetStoriesRequest request);
        Task<List<GetUsersDetailsResponse?>> GetUsersAsync();
        Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id);
    }

}

