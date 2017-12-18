namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Datamantra.CourseAuthorMapping")]
    public partial class CourseAuthorMapping : EntityBase
    {
        [Key]
        public long AuthorId { get; set; }
        public long CourseId { get; set; }
        public long? UserId { get; set; }
        public long? CategoryId { get; set; }
        public List<SelectedCourse> Courselst { get; set; }
    }

    public class SelectedCourse
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class SelectedCategory
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long SubCategoryId { get; set; }
    }
}
