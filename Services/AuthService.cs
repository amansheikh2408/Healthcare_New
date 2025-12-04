using Healthcare.Data;
using Healthcare.Models;
using Healthcare.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using static Healthcare.DTOs.AuthDtos;
using static Healthcare.DTOs.AuthResponse;


namespace Healthcare.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IOtpService _otpService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository users,
            IOtpService otpService,
            ITokenService tokenService,
            IPasswordHasher<User> hasher)
        {
            _users = users;
            _otpService = otpService;
            _tokenService = tokenService;
            _hasher = hasher;
        }

        public async Task RegisterAsync(RegisterRequest req)
        {
            if (await _users.AnyByEmailAsync(req.Email))
                throw new InvalidOperationException("Email already registered");

            var user = new User
            {
                Email = req.Email,
                Role = string.IsNullOrWhiteSpace(req.Role) ? "User" : req.Role
            };

            user.PasswordHash = _hasher.HashPassword(user, req.Password);
            await _users.AddAsync(user);
        }

        public async Task<string> SendLoginOtpAsync(LoginRequest req)
        {
            var user = await _users.GetByEmailAsync(req.Email)
                ?? throw new InvalidOperationException("Invalid credentials");

            var verify = _hasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
            if (verify == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid credentials");

            // generate and email OTP
            await _otpService.GenerateAndStoreOtpAsync(user.Email);

            return $"OTP sent to {user.Email}";
        }

        public async Task<AuthResponse> VerifyOtpAndIssueTokenAsync(VerifyOtpRequest req)
        {
            var isValid = await _otpService.ValidateOtpAsync(req.Email, req.Otp);
            if (!isValid) throw new InvalidOperationException("Invalid or expired OTP");

            var user = await _users.GetByEmailAsync(req.Email)
                ?? throw new InvalidOperationException("User not found");

            var token = _tokenService.CreateToken(user.Email, user.Role);

            return new AuthResponse(token, user.Role, user.Email);
        }
    }
}
