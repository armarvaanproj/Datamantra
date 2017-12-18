namespace Datamantra.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Model.Entity;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAuthorMapping> CourseAuthorMappings { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Category>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Course>()
                .Property(e => e.DiscountedPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Course>()
                .Property(e => e.Image)
                .IsFixedLength();

            modelBuilder.Entity<Course>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Course>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            //modelBuilder.Entity<Course>()
            //    .Property(e => e.CategoryId)
            //    .IsFixedLength();

            modelBuilder.Entity<CourseAuthorMapping>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<CourseAuthorMapping>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Review>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Review>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Rating)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();
        }

        public static implicit operator DataContext(Lazy<IDmDataContext> v)
        {
            throw new NotImplementedException();
        }
    }
}
