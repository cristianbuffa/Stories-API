namespace Stories.Domain
{
    public interface IStoryService
    {
        Task<List<GetStoryDetailsResponse?>> GetStoriesAsync(GetStoriesRequest request);

        Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id);

        Task<List<GetUsersDetailsResponse?>> GetUsersAsync();
    }
}