namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionLog")]
    public partial class TransactionLog
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string HostIP { get; set; }

        public long? TransactionId { get; set; }

        public long AccountId { get; set; }

        public long AccountNumber { get; set; }

        public int TransactionTypeId { get; set; }

        [StringLength(5)]
        public string TransactionCurrency { get; set; }

        public decimal? CurrencyRateWithBase { get; set; }

        [StringLength(5)]
        public string BaseCurrency { get; set; }

        public DateTime? TransactionDate { get; set; }

        public decimal? BalanceAmountBaseCurrency { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }

        public virtual Account Account { get; set; }

        public virtual TransactionDetail TransactionDetail { get; set; }

        public virtual TransactionType TransactionType { get; set; }
    }
}
