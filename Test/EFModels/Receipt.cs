using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.EFModels
{
    public partial class Receipt
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("provider_id")]
        public long ProviderId { get; set; }
        [Column("currency_id")]
        public long CurrencyId { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Required]
        [Column("date")]
        public string Date { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("status")]
        public long Status { get; set; }

        [ForeignKey("CurrencyId")]
        [InverseProperty("Receipt")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("ProviderId")]
        [InverseProperty("Receipt")]
        public virtual Provider Provider { get; set; }
    }
}
