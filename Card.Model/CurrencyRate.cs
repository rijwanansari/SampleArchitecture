namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrencyRate")]
    public partial class CurrencyRate
    {
        public long Id { get; set; }

        public long CurrencyId { get; set; }

        public decimal Rate { get; set; }

        public long RefCurrencyId { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Currency Currency1 { get; set; }
    }
}
