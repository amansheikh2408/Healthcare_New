namespace Healthcare.DTOs
{
    public class AuthResponse
    {

        public string Token { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        // Constructor
        public AuthResponse(string token, string role, string email)
        {
            Token = token;
            Role = role;
            Email = email;
        }
    }
}
