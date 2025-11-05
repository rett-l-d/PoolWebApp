using System;

using Microsoft.EntityFrameworkCore;

using PoolApp.Application.Interfaces;

using PoolApp.Domain.EntitiesUsers;
using PoolApp.Services;

namespace PoolApp.Infrastructure.Data
{
    public class UsersRepo: IUsersRepo
    {
        private readonly AppDbContext _context;

        public int TotalPoints { get; set; }

        public UsersRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Users>> GetAllUsersInfo()
        {
            return await _context.UsersInfo.ToListAsync();
        }

        public async Task<bool> UpdateUserInfo(Users user)
        {

            _context.UsersInfo.Update(user);

            var saved = await _context.SaveChangesAsync();

            if (saved <= 0)
                return false;

            return true;
        }

        public async Task<bool> InsertNewUser(Users user)
        {
            // Check in the database directly
            var exists = await _context.UsersInfo
                .AnyAsync(x => x.Phone == user.Phone);

            if (exists)
                return false;


            await _context.AddAsync(user);

            var saved = await _context.SaveChangesAsync();

            if (saved <= 0)
                return false;

            return true;

        }

        public async Task<string> SetupAuthenticatorAsync(Users user)
        {
            var auth = new AuthenticatorService();

            string secret;
            string otpauthUrl = auth.GenerateSetupCode("PoolApp", user.Phone?? "0", out secret);
            string qrImageBase64 = auth.GenerateQrCodeImageBase64(otpauthUrl);

            // save secret to DB
            user.AuthenticatorSecret = secret;
            _context.UsersInfo.Update(user);
            await _context.SaveChangesAsync();

            return qrImageBase64;
        }

        public bool VerifyUserAuthenticatorCode(Users user, string code)
        {
            if (string.IsNullOrEmpty(user.AuthenticatorSecret))
                return false;

            var auth = new AuthenticatorService();
            return auth.VerifyCode(user.AuthenticatorSecret, code);
        }

        public async Task<Users?> GetByPhoneAsync(string phone)
        {
            return await _context.UsersInfo.FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<bool> DeleteUser(Users user)
        {
            var existuser = _context.UsersInfo.FirstOrDefault(x=> x.id == user.id);

            if (existuser != null)
                _context.UsersInfo.Remove(existuser);

            var saved = await _context.SaveChangesAsync();

            if (saved <= 0)
                return false;

            return true;


        }
    }
}

