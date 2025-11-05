using PoolApp.Domain.EntitiesUsers;

namespace PoolApp.Application.Interfaces
{
    public interface IUsersRepo
    {

        Task<List<Users>> GetAllUsersInfo();

        Task<bool> UpdateUserInfo(Users user);

        Task<bool> InsertNewUser(Users user);

        Task<bool> DeleteUser(Users user);

        Task<string> SetupAuthenticatorAsync(Users user);

        bool VerifyUserAuthenticatorCode(Users user, string code);

        Task<Users?> GetByPhoneAsync(string phone);
    }
}
