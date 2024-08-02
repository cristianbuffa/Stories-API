using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Domain;


namespace Stories.Domain
{
    public interface IHnClient
    {
        Task<List<int>?> GetNewestStoriesAsync(GetStoriesRequest request);

        Task<List<GetStoryDetailsResponse?>> GetNewestStoriesDetailsAsync(List<int> storiesIds);

        Task<GetStoryDetailsResponse?> GetStoryDetailsByIdAsync(int id);

        Task<List<GetUsersDetailsResponse?>> GetUsersDetailsAsync();
    }
}
