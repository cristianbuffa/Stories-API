namespace Stories.Domain
{
    public class Story
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;       
    }
}