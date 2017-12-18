namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Datamantra.Category")]
    public partial class Category:EntityBase
    {
        public long CategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public long? SubCategoryId { get; set; }

        public List<Category> SubCategorylst { get; set; }
    }
}
