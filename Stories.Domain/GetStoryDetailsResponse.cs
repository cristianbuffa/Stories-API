using System.ComponentModel.DataAnnotations;

namespace Stories.Domain
{
    public class GetStoryDetailsResponse
    {
        public int? id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
    }
}