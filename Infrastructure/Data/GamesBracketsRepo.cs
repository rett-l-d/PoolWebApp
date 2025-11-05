
using Microsoft.EntityFrameworkCore;

using PoolApp.Application.Interfaces;
using PoolApp.Domain.EntitiesBrackets;
using PoolApp.Domain.EntitiesUsers;

namespace PoolApp.Infrastructure.Data
{
    public class GamesBracketsRepo : IGamesBracketsRepo
    {
        private readonly AppDbContext _context;

        public GamesBracketsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteUsersGuessAsync(int userid)
        {
            var userGuess = _context.UsersGuessBrackets.Where(x => x.UserID == userid).ToList();
            if (userGuess == null)
                return false;
            _context.UsersGuessBrackets.RemoveRange(userGuess);
            var saved = await _context.SaveChangesAsync();
            if (saved <= 0)
                return false;
            return true;
        }

        public IQueryable<GamesBrackets> GetAllGames()
        {
            return _context.GamesBrackets.AsQueryable();
        }

        public IQueryable<UsersGuessBrackets> GetAllUsersGuess()
        {
            return _context.UsersGuessBrackets.AsQueryable();
        }

        public async Task<bool> UpdateGamesResultsAsync(GamesBrackets games)
        {
            var gamesToUpdate = await _context.GamesBrackets.FirstOrDefaultAsync(c => c.id == games.id);

            if (gamesToUpdate == null)
                return false;

            gamesToUpdate.HomeScore = games.HomeScore??0;
            gamesToUpdate.AwayScore = games.AwayScore??0;
            var saved = await _context.SaveChangesAsync();


            if (saved <= 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateUsersGuessAsync(UsersGuessBrackets usersGuess, int UserID)
        {
            // Check if a guess already exists for this match and user
            var existingGuess = await _context.UsersGuessBrackets
                .FirstOrDefaultAsync(c => c.MatchID == usersGuess.MatchID && c.UserID == UserID);

            if (existingGuess != null)
            {
                // Update existing guess
                existingGuess.HomeScore = usersGuess.HomeScore ?? 0;
                existingGuess.AwayScore = usersGuess.AwayScore ?? 0;
                // EF Core tracks changes automatically
            }
            else
            {
                // No existing guess, insert new
                usersGuess.UserID = UserID;
                usersGuess.HomeScore = usersGuess.HomeScore ?? 0;
                usersGuess.AwayScore = usersGuess.AwayScore ?? 0;
                await _context.UsersGuessBrackets.AddAsync(usersGuess);
            }

            var saved = await _context.SaveChangesAsync();

            if (saved <= 0)
                return false;

            return true;
        }
    }
}
