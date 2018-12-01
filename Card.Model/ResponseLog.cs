namespace Sample.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResponseLog")]
    public partial class ResponseLog
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string HostIP { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }

        public long Author { get; set; }

        public DateTime Modified { get; set; }

        public long Editor { get; set; }
    }
}
