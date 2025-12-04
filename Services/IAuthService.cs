using static Healthcare.DTOs.AuthDtos;

namespace Healthcare.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest req);
        Task<string> SendLoginOtpAsync(LoginRequest req);
        Task<AuthResponse> VerifyOtpAndIssueTokenAsync(VerifyOtpRequest req);
    }
}
