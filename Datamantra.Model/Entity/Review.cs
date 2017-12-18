namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Datamantra.Review")]
    public partial class Review:EntityBase
    {
        public long ReviewId { get; set; }

       

        public long CourseId { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }

        public decimal Rating { get; set; }
    }
}
