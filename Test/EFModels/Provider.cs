using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.EFModels
{
    public partial class Provider
    {
        public Provider()
        {
            Receipt = new HashSet<Receipt>();
        }

        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("status")]
        public long Status { get; set; }

        [InverseProperty("Provider")]
        public virtual ICollection<Receipt> Receipt { get; set; }
    }
}
