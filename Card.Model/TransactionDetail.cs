namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionDetail")]
    public partial class TransactionDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionDetail()
        {
            TransactionLogs = new HashSet<TransactionLog>();
        }

        public long Id { get; set; }

        public int TransactionTypeId { get; set; }

        public long AccountId { get; set; }

        public long AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public long CurrencyDepositId { get; set; }

        public decimal AmountInBaseCurrency { get; set; }

        public decimal CurrentBalance { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }
    }
}
