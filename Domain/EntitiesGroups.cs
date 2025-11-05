using System;
using PoolApp.Domain.EntitiesTeams;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolApp.Domain.EntitiesGroups
{
    [Table("MatchesGroups")]
    public class GamesGroups
    {
        public int id { get; set; }
        public string? GroupID { get; set; }  
        public int HomeTeamID { get; set; }           
        public int AwayTeamID { get; set; }
        public DateTime MatchDateTime { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public virtual UsersGuessGroups? Usersguess { get; set; }
        public virtual Teams? HomeTeam { get; set; }
        public virtual Teams? AwayTeam { get; set; }


    }

    [Table("UsersGuessGroups")]
    public class UsersGuessGroups
    {
        public int id { get; set; }
        public int MatchID { get; set; }
        public int UserID { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
       // public virtual Games? Game { get; set; }
    }

  

  

    [Table("Groups")]
    public class Groups
    {
        public int id { get; set; }
        public string? Name { get; set; }
    }
}
