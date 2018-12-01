namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountType")]
    public partial class AccountType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }
    }
}
