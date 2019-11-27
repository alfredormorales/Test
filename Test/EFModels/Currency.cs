using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.EFModels
{
    public partial class Currency
    {
        public Currency()
        {
            Receipt = new HashSet<Receipt>();
        }

        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("status")]
        public long Status { get; set; }

        [InverseProperty("Currency")]
        public virtual ICollection<Receipt> Receipt { get; set; }
    }
}
