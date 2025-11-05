
using System.Security.Policy;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PoolApp.Application.Interfaces;
using PoolApp.Domain.EntitiesGroups;

namespace PoolApp.Infrastructure.Data
{
    public class GamesGroupsRepo : IGamesGroupsRepo
    {
        private readonly AppDbContext _context;

        public GamesGroupsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteUsersGuessAsync(int userid)
        {
            var userGuess = _context.UsersGuessGroups.Where(x => x.UserID == userid).ToList();
            if (userGuess == null)
                return false;
            _context.UsersGuessGroups.RemoveRange(userGuess);
            var saved = await _context.SaveChangesAsync();
            if (saved <= 0)
                return false;
            return true;
        }

        public IQueryable<GamesGroups> GetAllGames()
        {
            return _context.GamesGroups.AsQueryable();
        }

        public IQueryable<UsersGuessGroups> GetAllUsersGuess()
        {
            return _context.UsersGuessGroups.AsQueryable();
        }


        public async Task<bool> UpdateGamesResultsAsync(GamesGroups games)
        {
            var gamesToUpdate = await _context.GamesGroups.FirstOrDefaultAsync(c => c.id == games.id);

            if (gamesToUpdate == null)
                return false;

            gamesToUpdate.HomeScore = games.HomeScore ?? 0;
            gamesToUpdate.AwayScore = games.AwayScore ?? 0;

            var saved = await _context.SaveChangesAsync();


            if (saved <= 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateUsersGuessAsync(UsersGuessGroups usersGuess, int UserID)
        {
            // Check if a guess already exists for this match and user
            var existingGuess = await _context.UsersGuessGroups
                .FirstOrDefaultAsync(c => c.MatchID == usersGuess.MatchID && c.UserID == UserID);

            if (existingGuess != null)
            {
                // Update existing guess
                existingGuess.HomeScore = usersGuess.HomeScore?? 0;
                existingGuess.AwayScore = usersGuess.AwayScore?? 0;
                // EF Core tracks changes automatically
            }
            else
            {
                // No existing guess, insert new
                usersGuess.UserID = UserID;
                usersGuess.HomeScore  = usersGuess.HomeScore ?? 0;
                usersGuess.AwayScore = usersGuess.AwayScore ?? 0;
                await _context.UsersGuessGroups.AddAsync(usersGuess);
            }

            var saved = await _context.SaveChangesAsync();

            if (saved <= 0)
                return false;

            return true;

        }
    }

}
