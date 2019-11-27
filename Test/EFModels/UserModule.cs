using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.EFModels
{
    public partial class UserModule
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("module_id")]
        public long ModuleId { get; set; }
        [Column("status")]
        public long Status { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserModule")]
        public virtual User User { get; set; }
    }
}
