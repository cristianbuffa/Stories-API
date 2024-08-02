using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Domain;


namespace Stories.Repository 
{

    public class StoryRepository : IStoryRepository
    {
        private readonly IHnClient _hnClient;
        public const string ClientName = "StoriesApiClient";
        public StoryRepository(IHnClient hnClient)
        {
            _hnClient = hnClient;
        }

        public async Task<List<GetStoryDetailsResponse?>> GetStoriesAsync(GetStoriesRequest request)
        {
            List<GetStoryDetailsResponse?> stories = new();
            var storiesIds = await _hnClient.GetNewestStoriesAsync(request);

            try
            {
               
                if (storiesIds != null)
                {
                    stories = await _hnClient.GetNewestStoriesDetailsAsync(storiesIds);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying to get best stories: " + ex.Message);
            }
            return stories;
        }

        public async Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id)
        {
            //41059354
            GetStoryDetailsResponse? storyDetails = null;
            try
            {
                storyDetails = await _hnClient.GetStoryDetailsByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying to story details.");
            }

            return storyDetails;
        }

        public async Task<List<GetUsersDetailsResponse?>> GetUsersAsync()
        {
            List<GetUsersDetailsResponse?> usersDetails = new();
            try
            {
                usersDetails = await _hnClient.GetUsersDetailsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying to users details.");
            }

            return usersDetails!;
        }
    }
}
