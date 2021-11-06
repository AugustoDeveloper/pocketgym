namespace YourPocketGym.Application.DTOs
{
    public record RegistrationUser
    {
        public string Name { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string ConfirmationPassword { get; init; }
    }
}