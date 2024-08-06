using Microsoft.Extensions.Logging;
using Stories.Domain.Exceptions;
using Stories.Domain.Interface;
using Stories.Domain.Request;
using Stories.Domain.Response;
using Stories.Domain.Validation;


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
            catch 
            {
                throw new ApiLegacy404Exception(ValidationMessage.
               General("ExternalServiceNotFound", "Story Repository fails.", ""));
            }
            return stories;
        }

        public async Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id)
        {
            GetStoryDetailsResponse? storyDetails = null;
            try
            {
                storyDetails = await _hnClient.GetStoryDetailsByIdAsync(id);
            }
            catch
            {
                throw new ApiLegacy404Exception(ValidationMessage.
               General("ExternalServiceNotFound", "Story Repository fails.", ""));
            }

            return storyDetails;
        }
    }
}
