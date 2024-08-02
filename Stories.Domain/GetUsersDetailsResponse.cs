namespace Stories.Domain
{
    public class GetUsersDetailsResponse
    {
        public string id { get; set; }
        public string Karma { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public List<string> Submitted { get; set; } = new List<string>();   
    }
}