using Datamantra.Model.Entity;
using Datamantra.Service.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using Datamantra.BAL.Interface;

namespace Datamantra.Service.Controllers
{
    public class AdminController : BaseApiController
    {

        #region Declaration
        private IAdminBAL _AdminBAL;

        public AdminController()
        {

        }
        public AdminController(IAdminBAL AdminBAL)
        {
            _AdminBAL = AdminBAL;
        }
        #endregion

        #region Category Methods

        #region Get All Category
        [HttpGet]
        [Route("Admin/GetAllCategories")]
        public IHttpActionResult GetAllCategories()
        {
            List<Category> _lstcategory = new List<Category>();
            _lstcategory = _AdminBAL.GetAllCategories();
            return Ok<List<Category>>(_lstcategory);
        }
        #endregion

        #region Save Course
        /// <summary>
        /// SaveCategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/SaveCategory")]
        public bool SaveCategory(Category category)
        {
            return _AdminBAL.SaveCategory(category);
        }
        #endregion

        #region Get Category By Id
        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetCategoryById")]
        public Category GetCategoryById(long id)
        {
            var category = new Category();
            if (id > 0)
            {
                category = _AdminBAL.GetCategoryById(id);
            }
            return category;
        }
        #endregion

        #region Delete Category by Id
        /// <summary>
        /// Delete Category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/DeleteCategoryById")]
        public bool DeleteCategoryById(long id)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _AdminBAL.DeleteCategoryById(id);
            }
            return isdeleted;
        }
        #endregion

        #endregion

        #region Course

        #region Get All Course
        /// <summary>
        ///  Get All Course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetAllCourse")]
        public IHttpActionResult GetAllCourse()
        {
            List<Course> _lstCourse = new List<Course>();
            _lstCourse = _AdminBAL.GetAllCourse();
            return Ok<List<Course>>(_lstCourse);
        }
        #endregion

        #region Get Course Details By CourseId
        /// <summary>
        ///  Get Course Details By CourseId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetCourseDetailsByCourseId")]
        public Course GetCourseDetailsByCourseId(long id)
        {
            var course = new Course();
            if (id > 0)
            {
                course = _AdminBAL.GetCourseDetailsByCourseId(id);
                if (course != null && course.CourseId > 0)
                    course.Status = StatusType.Success;
                else course.Status = StatusType.Failure;
            }
            return course;
        }
        #endregion

        #region Save Course
        /// <summary>
        ///  Save Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/SaveCourse")]
        public bool SaveCourse(Course course)
        {
            return _AdminBAL.SaveCourse(course);
        }
        #endregion

        #region Get All Course Details By Category Id
        /// <summary>
        /// Get All Course Details By Category Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetAllCourseByCategoryId")]
        public List<Course> GetAllCourseByCategoryId(long CategoryId)
        {
            var Courselst = new List<Course>();
            if (CategoryId > 0)
            {
                Courselst = _AdminBAL.GetAllCourseByCategoryId(CategoryId);
            }
            return Courselst;
        }
        #endregion

        #region Delete Course by Id
        /// <summary>
        /// Delete Course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/DeleteCourseById")]
        public bool DeleteCourseById(long id)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _AdminBAL.DeleteCourseById(id);
            }
            return isdeleted;
        }
        #endregion

        #region Get Course List By Staff Id
        /// <summary>
        /// Get Course List By Staff Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetCourseListByStaffId")]
        public IHttpActionResult GetCourseListByStaffId(long id)
        {
            List<Course> _lstCourse = new List<Course>();
            _lstCourse = _AdminBAL.GetCourseListByStaffId(id);
            return Ok<List<Course>>(_lstCourse);
        }
        #endregion



        #endregion

        #region roles

        #region Get All Role
        /// <summary>
        ///  Get All Role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            List<Role> _lstRole = new List<Role>();
            _lstRole = _AdminBAL.GetAllRoles();
            return Ok<List<Role>>(_lstRole);
        }
        #endregion

        #region Get Roles By Id
        /// <summary>
        ///  Get Roles By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetRolesById")]
        public Role GetRolesById(long id)
        {
            var role = new Role();
            if (id > 0)
            {
                role = _AdminBAL.GetRolesById(id);
                if (role != null && role.RoleId > 0)
                    role.Status = StatusType.Success;
                else role.Status = StatusType.Failure;
            }
            return role;
        }
        #endregion

        #region Save Role
        /// <summary>
        ///  Save role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/SaveRole")]
        public bool SaveRole(Role role)
        {
            return _AdminBAL.SaveRole(role);
        }
        #endregion

        #region Delete Role by Id
        /// <summary>
        /// Delete Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/DeleteRoleById")]
        public bool DeleteRoleById(long id)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _AdminBAL.DeleteRoleById(id);
            }
            return isdeleted;
        }
        #endregion

        #endregion

        #region Users & Roles

        #region Get All Active User Details
        /// <summary>
        /// Users & Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/GetAllActiveUserDetails")]
        public List<User> GetAllActiveUserDetails()
        {
            return _AdminBAL.GetAllActiveUserDetails();
            //return Ok<List<User>>(_activeuserlst);
        }
        #endregion

        #region Update User Roles
        /// <summary>
        /// Update User Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/UpdateUserRoles")]
        public bool UpdateUserRoles(long userId, long roleId)
        {
            return _AdminBAL.UpdateUserRoles(userId, roleId);
        }
        #endregion

        #endregion
    }
}
