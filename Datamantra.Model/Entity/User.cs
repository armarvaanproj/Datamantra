namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Datamantra.User")]
    public partial class User : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        

        [StringLength(50)]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        [StringLength(150)]
        public string Salt { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(10)]
        public string Rating { get; set; }

        [StringLength(40)]
        public string EmailAddress { get; set; }
        public long AuthorId { get; set; }
        public List<CourseAuthorMapping> StaffCourseLst { get; set; }
        public List<SelectedCategory> SelectedCategorylst { get; set; }
        public long CourseId { get; set; }

    }
}
