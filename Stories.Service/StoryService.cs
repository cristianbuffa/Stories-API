using Microsoft.Extensions.Caching.Memory;
using Stories.Domain;
using Stories.Domain.Exceptions;
using Stories.Domain.Validation;
using System;

namespace Stories.Service;
public class StoryService : IStoryService
{
    private readonly IStoryRepository _storyRepository;
    private readonly IMemoryCache _cache;
    private readonly IDomainModelValidator _domainModelValidator;

    public StoryService(IStoryRepository storyRepository, IMemoryCache cache, IDomainModelValidator domainModelValidator)
    {
        _cache = cache;
        _storyRepository = storyRepository;
        _domainModelValidator = domainModelValidator;   
    }

    public async Task<List<GetStoryDetailsResponse?>> GetStoriesAsync(GetStoriesRequest request)
    {
        // solo las url activas
        this._domainModelValidator.ValidateRecursive(request);
        var dataCached = _cache.Get<List<GetStoryDetailsResponse>>("storiesList");
        if (dataCached is not null) 
            return dataCached!;

        var stories = await _cache.GetOrCreateAsync("storiesList", async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            var storiesList = await _storyRepository.GetStoriesAsync(request);
            return storiesList;
        });
        if (stories == null)
            throw new ApiValidationException(ValidationMessage.
                General("StoriesNotFound", "The stories have not been recovered.", null));

        return stories!;
    }

    public async Task<List<GetUsersDetailsResponse?>> GetUsersAsync()
    {
       return await _storyRepository.GetUsersAsync();
    }

    public async Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id)
    {
        var story = await _storyRepository.GetStoryDetailsAsync(id);

        if (story == null)
            throw new ApiLegacy404Exception(ValidationMessage.
                General("StoryNotFound", "The story have not been recovered.", null));
        return story;
    }

}


