namespace GitHubApiCall.Models
{
    public class GitHubEvent
    {
        public string Type { get; set; }
        public Repo Repo { get; set; }
        public Payload Payload { get; set; }

    }
}
