namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            TransactionLogs = new HashSet<TransactionLog>();
        }

        public long Id { get; set; }

        public long AccountNumber { get; set; }

        public long AccountTypeId { get; set; }

        public long CustomerId { get; set; }

        public DateTime? OpenDate { get; set; }

        public bool IsBlock { get; set; }

        public bool IsVerified { get; set; }

        public long BaseCurrencyId { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }
    }
}
