namespace Perka.Apply.Client.Models
{
    public class ApplicationSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PositionId { get; set; }
        public string Explanation { get; set; }
        public string Source { get; set; }
        public Endpoint GithubApi { get; set; }
        public Endpoint PerkaApi { get; set; }
        public Resume Resume { get; set; }
    }

    public class Endpoint
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }

    public class Resume
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Extension { get; set; }
    }
}