using Healthcare.Data;
using Healthcare.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Services
{
    public class OtpService: IOtpService
    {

        private readonly AppDbContext _db;
        private readonly IEmailService _emailService;
        public OtpService(AppDbContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

        public async Task<string> GenerateAndStoreOtpAsync(string email)
        {
            var rng = new Random();
            var code = rng.Next(100000, 999999).ToString();
            var entry = new OtpEntry
            {
                Email = email,
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                Used = false,
                CreatedAt = DateTime.UtcNow
            };

            _db.Otps.Add(entry);
            await _db.SaveChangesAsync();

            await _emailService.SendOtpAsync(email, code);
            return code;
        }

        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var entry = await _db.Otps
                .Where(x => x.Email == email && !x.Used)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (entry == null) return false;
            if (entry.ExpiresAt < DateTime.UtcNow) return false;
            if (entry.Code != otp) return false;

            entry.Used = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

