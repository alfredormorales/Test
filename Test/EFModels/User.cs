using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.EFModels
{
    public partial class User
    {
        public User()
        {
            UserModule = new HashSet<UserModule>();
        }

        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("user_name")]
        public string UserName { get; set; }
        [Required]
        [Column("create_date")]
        public string CreateDate { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Column("status")]
        public long Status { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserModule> UserModule { get; set; }
    }
}
