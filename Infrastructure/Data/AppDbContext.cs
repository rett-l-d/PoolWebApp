

using Microsoft.EntityFrameworkCore;

using PoolApp.Domain.EntitiesGroups;
using PoolApp.Domain.EntitiesBrackets;
using PoolApp.Domain.EntitiesUsers;

namespace PoolApp.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<GamesGroups> GamesGroups => Set<GamesGroups>();
        public DbSet<UsersGuessGroups> UsersGuessGroups => Set<UsersGuessGroups>();

        public DbSet<GamesBrackets> GamesBrackets => Set<GamesBrackets>();
        public DbSet<UsersGuessBrackets> UsersGuessBrackets => Set<UsersGuessBrackets>();

        public DbSet<Users> UsersInfo => Set<Users>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Games → UsersGuess relationship
            modelBuilder.Entity<GamesGroups>()
                .HasOne(g => g.Usersguess)
                .WithOne() // or .WithMany() if multiple guesses per match
                .HasForeignKey<UsersGuessGroups>(ug => ug.MatchID);

            // Optional: configure Games → Teams relationships
            modelBuilder.Entity<GamesGroups>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey(g => g.HomeTeamID);

            modelBuilder.Entity<GamesGroups>()
               .HasOne(g => g.AwayTeam)
               .WithMany()
               .HasForeignKey(g => g.AwayTeamID);


            // Configure Games → UsersGuess relationship
            modelBuilder.Entity<GamesBrackets>()
                .HasOne(g => g.Usersguess)
                .WithOne() // or .WithMany() if multiple guesses per match
                .HasForeignKey<UsersGuessBrackets>(ug => ug.MatchID);

            // Optional: configure Games → Teams relationships
            modelBuilder.Entity<GamesBrackets>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey(g => g.HomeTeamID);

            modelBuilder.Entity<GamesBrackets>()
               .HasOne(g => g.AwayTeam)
               .WithMany()
               .HasForeignKey(g => g.AwayTeamID);
        }


    }
}
