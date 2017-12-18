namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Datamantra.Course")]
    public partial class Course:EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CourseId { get; set; }

        [StringLength(50)]
        public string CourseName { get; set; }

        public int Rating { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        public long EnrolledCount { get; set; }

        public string Image { get; set; }

        public string ShortDescription { get; set; }
  
        public string LongDescription { get; set; }
        
        public long CategoryId { get; set; }
    }
}
