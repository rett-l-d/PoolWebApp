using System.ComponentModel.DataAnnotations.Schema;
using PoolApp.Domain.EntitiesTeams;

namespace PoolApp.Domain.EntitiesBrackets
{
    [Table("MatchesBrackets")]
    public class GamesBrackets
    {
        public int id { get; set; }
        public string? BracketID { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public DateTime? MatchDateTime { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public virtual UsersGuessBrackets? Usersguess { get; set; }
        public virtual Teams? HomeTeam { get; set; }
        public virtual Teams? AwayTeam { get; set; }


    }

    [Table("UsersGuessBrackets")]
    public class UsersGuessBrackets
    {
        public int id { get; set; }
        public int MatchID { get; set; }
        public int UserID { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        // public virtual Games? Game { get; set; }
    }



    [Table("Brackets")]
    public class Brackets
    {
        public int id { get; set; }
        public string? Name { get; set; }
    }

}
