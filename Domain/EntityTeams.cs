using System.ComponentModel.DataAnnotations.Schema;

using PoolApp.Domain.EntitiesGroups;

namespace PoolApp.Domain.EntitiesTeams
{
    [Table("Teams")]
    public class Teams
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public int GroupId { get; set; }
        public virtual Groups? Group { get; set; }
    }
}
