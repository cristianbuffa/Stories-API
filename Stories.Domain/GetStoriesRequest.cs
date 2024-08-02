using System.ComponentModel.DataAnnotations;

namespace Stories.Domain
{
    public class GetStoriesRequest
    {
        public string OrderBy { get; set; } = string.Empty;
        [Range(1, 50, ErrorMessage = "Each Page allows between 1 and 50 registers.")]
        public int Limit { get; set; }
    }

}