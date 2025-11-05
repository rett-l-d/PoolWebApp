using System.ComponentModel.DataAnnotations.Schema;

namespace PoolApp.Domain.EntitiesUsers
{

        [Table("Users")]
        public class Users
        {
            public int id { get; set; }
            public string? Name { get; set; }
            public string? Phone { get; set; }
            public string? AuthenticatorSecret { get; set; }
            public string? RecoveryCode { get; set; }
        [NotMapped]
            public int? Points { get; set; }
        }

}
