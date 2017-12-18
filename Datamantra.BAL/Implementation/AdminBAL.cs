using Datamantra.BAL;
using Datamantra.BAL.Interface;
using Datamantra.DAL;
using Datamantra.DAL.Interface;
using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.BAL.Implementation
{
    //public class UserBAL : DMBaseManager, IUserBAL
    public class AdminBAL : IAdminBAL
    {
        #region Declaration
        private IAdminDAL _AdminDAL;
        public AdminBAL(IAdminDAL AdminDAL)
        {
            _AdminDAL = AdminDAL;
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
            return this._AdminDAL.GetAllCategories();
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
            return this._AdminDAL.SaveCategory(category);
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
            return this._AdminDAL.GetCategoryById(id);
        }
        #endregion

        #region Delete Category By Id
        /// <summary>
        /// Delete Course By Id
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public bool DeleteCategoryById(long id)
        {
            return this._AdminDAL.DeleteCategoryById(id);
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
            return this._AdminDAL.GetAllCourse();
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
            return this._AdminDAL.GetCourseDetailsByCourseId(id);
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
            return this._AdminDAL.SaveCourse(course);
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
            return _AdminDAL.GetAllCourseByCategoryId(CategoryId);
        }
        #endregion

        #region Delete Course By Id
        /// <summary>
        /// Delete Course By Id
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public bool DeleteCourseById(long id)
        {
            return this._AdminDAL.DeleteCourseById(id);
        }
        #endregion

        #region Get Course List By Staff Id
        /// <summary>
        /// Get All Course
        /// </summary>
        /// <returns></returns>
        public List<Course> GetCourseListByStaffId(long id)
        {
            return this._AdminDAL.GetCourseListByStaffId(id);
        }
        #endregion
        #endregion

        #region roles

        #region Get All Role
        /// <summary>
        /// Get All Role
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRoles()
        {
            return this._AdminDAL.GetAllRoles();
        }
        #endregion

        #region Get Roles By Id
        /// <summary>
        /// Get Role Details By Role Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRolesById(long id)
        {
            return this._AdminDAL.GetRolesById(id);
        }
        #endregion

        #region Save Role
        /// <summary>
        /// Save Role
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool SaveRole(Role role)
        {
            return this._AdminDAL.SaveRole(role);
        }
        #endregion

        #region Delete Role By Id
        /// <summary>
        /// Delete Role By Id
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool DeleteRoleById(long id)
        {
            return this._AdminDAL.DeleteRoleById(id);
        }
        #endregion

        #endregion

        #region Users & Roles

        #region Get All Active User Details
        /// <summary>
        /// Users & Roles
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllActiveUserDetails()
        {
            return this._AdminDAL.GetAllActiveUserDetails();
        }
        #endregion

        #region Update User Roles
        /// <summary>
        ///  Update User Roles
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserRoles(long userId, long roleId)
        {
            return this._AdminDAL.UpdateUserRoles(userId, roleId);
        }
        #endregion

        #endregion


    }
}














