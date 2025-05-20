namespace GitHubApiCall.Models
{
    public class Payload
    {
        public List<Commit> Commits { get; set; }
        public string Action { get; set; }
    }
}