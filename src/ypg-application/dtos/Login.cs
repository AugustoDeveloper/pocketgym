namespace YourPocketGym.Application.DTOs
{
    public record Login
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }
}