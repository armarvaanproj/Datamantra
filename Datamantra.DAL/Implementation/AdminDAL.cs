using Datamantra.DAL.Interface;
using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Datamantra.DAL.Handlers;
using System.Configuration;

namespace Datamantra.DAL.Implementation
{
    public class AdminDAL : IAdminDAL
    {
        #region Declaration
        internal IDbConnection ACADBConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["DatamantraDB"].ConnectionString);
            }
        }
        #endregion


        #region Category Methods

        #region Get All Category
        /// <summary>
        /// Get All Category
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            List<Category> _lstcategory = new List<Category>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _lstcategory = dbConnection.Query<Category>(SqlQueryStrings.GetAllCategoriesList).ToList();
                if (_lstcategory != null && _lstcategory.Count > 0)
                {
                    foreach (var item in _lstcategory)
                    {
                        item.SubCategorylst = new List<Category>();
                        if (item != null && item.SubCategoryId != null && item.SubCategoryId > 0)
                        {
                            item.SubCategorylst = dbConnection.Query<Category>(SqlQueryStrings.GetAllSubCategoriesByCategoryId, new { SubCategoryId = item.SubCategoryId }).ToList();
                        }
                    }
                }
            }
            return _lstcategory;
        }
        #endregion

        #region Save Category
        /// <summary>
        /// Save Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool SaveCategory(Category category)
        {
            bool is_saved = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                category.ModifiedDate = DateTime.Now;
                if (category.CategoryId > 0)
                {
                    dbConnection.Query<Category>(SqlQueryStrings.UpdateCategoryById, category).SingleOrDefault();
                    is_saved = true;
                }
                else
                {
                    category.CategoryId = dbConnection.Query<int>(SqlQueryStrings.SaveCategory, category).FirstOrDefault();
                    is_saved = (category.CategoryId > 0) ? true : false;
                }
            }
            return is_saved;
        }
        #endregion

        #region Update Category
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Category UpdateCategoryById(Category category)
        {
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<Category>(SqlQueryStrings.UpdateCategoryById, category).SingleOrDefault();
            }
            return category;
        }
        #endregion

        #region Get Category By Id
        /// <summary>
        /// Get  Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(long id)
        {
            Category _category = new Category();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _category = dbConnection.Query<Category>(SqlQueryStrings.GetCategoryByCategoryId, new { CategoryId = id }).FirstOrDefault();
            }
            return _category;
        }
        #endregion

        #region  Delete Category By Id
        /// <summary>
        /// Delete Categorys By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCategoryById(long id)
        {
            bool isdeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCategoryById, new { CategoryId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCourseByCategoryId, new { CategoryId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).ToList();
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCourseMappingbyCategoryId, new { CategoryId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).ToList();
                isdeleted = true;
            }
            return isdeleted;
        }
        #endregion

        #endregion

        #region Course

        #region Get All Course
        /// <summary>
        /// Get All Course
        /// </summary>
        /// <returns></returns>
        public List<Course> GetAllCourse()
        {
            List<Course> _lstCourse = new List<Course>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _lstCourse = dbConnection.Query<Course>(SqlQueryStrings.GetAllCourse).ToList();
            }
            return _lstCourse;
        }
        #endregion

        #region Get Course Details By Course Id
        /// <summary>
        /// Get Course Details By Course Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Course GetCourseDetailsByCourseId(long id)
        {
            var course = new Course();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                course = dbConnection.Query<Course>(SqlQueryStrings.GetCourseById, new { CourseId = id }).FirstOrDefault();
            }
            return course;
        }
        #endregion

        #region Save Course
        /// <summary>
        /// Save Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool SaveCourse(Course course)
        {
            bool is_saved = false;
            course.ModifiedDate = DateTime.Now;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                if (course.CourseId > 0)
                {
                    dbConnection.Query<Course>(SqlQueryStrings.UpdateCourseById, course).SingleOrDefault();
                    is_saved = true;
                }
                else
                {
                    course.CourseId = dbConnection.Query<int>(SqlQueryStrings.SaveCourse, course).FirstOrDefault();
                    is_saved = (course.CourseId > 0) ? true : false;
                }
            }
            return is_saved;
        }
        #endregion

        #region Get All Course Details By Course Id
        /// <summary>
        /// Get Course Details By Course Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Course> GetAllCourseByCategoryId(long CategoryId)
        {
            List<Course> _lstCourse = new List<Course>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _lstCourse = dbConnection.Query<Course>(SqlQueryStrings.GetAllCourseByCategoryId, new { CategoryId = CategoryId }).ToList();
            }
            return _lstCourse;
        }
        #endregion

        #region  Delete Course By Id
        /// <summary>
        /// Delete Course By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCourseById(long id)
        {
            bool isdeleted = false;
            //category.CreatedTimeStamp = DateTime.Now;
            //category.UpdatedTimeStamp = DateTime.Now;
            //category.IsDeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCourseById, new { CourseId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCourseAuthorByCourseId, new { CourseId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                isdeleted = true;
            }
            return isdeleted;
        }
        #endregion

        #region Get Course List By Staff Id
        /// <summary>
        /// Get Course List By Staff Id
        /// </summary>
        /// <returns></returns>
        public List<Course> GetCourseListByStaffId(long id)
        {
            List<Course> _lstCourse = new List<Course>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _lstCourse = dbConnection.Query<Course>(SqlQueryStrings.GetCourseListByStaffId, new { UserId = id }).ToList();
            }
            return _lstCourse;
        }
        #endregion

        #endregion

        #region roles

        #region Get All Roles
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRoles()
        {
            List<Role> _lstCourse = new List<Role>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                _lstCourse = dbConnection.Query<Role>(SqlQueryStrings.GetAllRoles).ToList();
            }
            return _lstCourse;
        }
        #endregion

        #region Get Roles By Id
        /// <summary>
        /// Get Course Details By Course Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRolesById(long id)
        {
            var role = new Role();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                role = dbConnection.Query<Role>(SqlQueryStrings.GetRolesById, new { RoleId = id }).FirstOrDefault();
            }
            return role;
        }
        #endregion

        #region Save Role
        /// <summary>
        /// Save Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool SaveRole(Role role)
        {
            bool is_saved = false;
            role.ModifiedDate = DateTime.Now;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                if (role.RoleId > 0)
                {
                    dbConnection.Query<Category>(SqlQueryStrings.UpdateRoleById, role).SingleOrDefault();
                    is_saved = true;

                }
                else
                {
                    role.RoleId = dbConnection.Query<int>(SqlQueryStrings.SaveRole, role).FirstOrDefault();
                    is_saved = (role.RoleId > 0) ? true : false;
                }
            }
            return is_saved;
        }
        #endregion

        #region  Delete Role By Id
        /// <summary>
        /// Delete Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteRoleById(long id)
        {
            bool isdeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<bool>(SqlQueryStrings.DeleteRoleById, new { RoleId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                isdeleted = true;
            }
            return isdeleted;
        }
        #endregion

        #endregion


        #region User & Roles

        #region Get All Active User Details
        /// <summary>
        /// Get All Active User Details
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllActiveUserDetails()
        {
            var activeuserlst = new List<User>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                activeuserlst = dbConnection.Query<User>(SqlQueryStrings.GetAllActiveUserDetails).ToList();
            }
            return activeuserlst;
        }
        #endregion

        #region Get All Active User Details
        /// <summary>
        /// Get All Active User Details
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserRoles(long userId, long roleId)
        {
            bool isupdated = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<User>(SqlQueryStrings.UpdateUserRoles, new { userId = userId, roleId = roleId }).SingleOrDefault();
                isupdated = true;
            }
            return isupdated;
        }
        #endregion

        #endregion











    }
}
