using Microsoft.AspNetCore.Mvc;

using PoolApp.Domain.EntitiesGroups;

namespace PoolApp.Application.Interfaces
{
    public interface IGamesGroupsRepo
    {
        IQueryable<GamesGroups> GetAllGames();
        IQueryable<UsersGuessGroups> GetAllUsersGuess();
        Task<bool> UpdateGamesResultsAsync(GamesGroups game);
        Task<bool> UpdateUsersGuessAsync(UsersGuessGroups usersGuess, int UserID);
        Task<bool> DeleteUsersGuessAsync(int userid);
    }
}
