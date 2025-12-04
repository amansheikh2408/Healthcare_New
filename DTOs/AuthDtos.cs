namespace Healthcare.DTOs
{
    public class AuthDtos
    {
        public record RegisterRequest(string Email, string Password, string? Role);
        public record LoginRequest(string Email, string Password);
        public record VerifyOtpRequest(string Email, string Otp);
        public record AuthResponse(string Token, string Role, string Email);
    }
}
