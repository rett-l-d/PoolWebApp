using PoolApp.Domain.EntitiesBrackets;

namespace PoolApp.Application.Interfaces
{
    public interface IGamesBracketsRepo
    {
        IQueryable<GamesBrackets> GetAllGames();
        IQueryable<UsersGuessBrackets> GetAllUsersGuess();
        Task<bool> UpdateGamesResultsAsync(GamesBrackets game);
        Task<bool> UpdateUsersGuessAsync(UsersGuessBrackets usersGuess, int UserID);
        Task<bool> DeleteUsersGuessAsync(int userid);
    }
}
