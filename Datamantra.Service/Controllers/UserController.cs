using Datamantra.BAL.Interface;
using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Datamantra.Service.Controllers
{
    // [Authorize]
    public class UserController : ApiController
    {
        #region Declaration
        private IUserBAL _userBAL;

        public UserController(IUserBAL userBAL)
        {
            _userBAL = userBAL;
        }
        #endregion

        #region Login User
        /// <summary>
        ///  Login User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("User/Login")]
        public IHttpActionResult Login(User user)
        {
            var _user = new User();
            _user = _userBAL.ValidateUser(user);
            return Ok<User>(_user);
        }
        #endregion

        #region Save User Details
        /// <summary>
        /// Save User Details
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("User/SaveUser")]
        public User SaveUser(User User)
        {
            return _userBAL.SaveUser(User);
        }
        #endregion

        #region Get Staff Details By CourseId
        [HttpGet]
        [Route("User/GetStaffDetailsByCourseId")]
        public List<User> GetStaffDetailsByCourseId(long CourseId)
        {
            return _userBAL.GetStaffDetailsByCourseId(CourseId);
        }
        #endregion

        #region Get All User Details by Role and Status
        [HttpGet]
        [Route("User/GetAllUserDetailsbyRoleandStatus")]
        public List<User> GetAllUserDetailsbyRoleandStatus(long RoleId, bool? ActiveStatus)
        {
            return _userBAL.GetAllUserDetailsbyRoleandStatus(RoleId, ActiveStatus);
        }
        #endregion


        #region  Get User Details By UserId
        /// <summary>
        /// Get User Details By UserId
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="ActiveStatus"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/GetUserDetailsByUserId")]
        public User GetUserDetailsByUserId(long userId)
        {
            return _userBAL.GetUserDetailsByUserId(userId);
        }
        #endregion

        #region Delete User by Id
        /// <summary>
        /// Delete Course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/DeleteUserbyUserId")]
        public bool DeleteUserbyUserId(long id)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _userBAL.DeleteUserbyUserId(id);
            }
            return isdeleted;
        }
        #endregion

        #region Check Is Valid Email
        /// <summary>
        /// Check Is Valid Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/CheckIsValidEmail")]
        public bool CheckIsValidEmail(string email)
        {
            return _userBAL.CheckIsValidEmail(email); 
        }
        #endregion

        #region Change User Status by User Id
        /// <summary>
        /// Change User Status by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/ChangeUserStatusbyUserId")]
        public bool ChangeUserStatusbyUserId(long id, bool ActiveStatus)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _userBAL.ChangeUserStatusbyUserId(id, ActiveStatus);
            }
            return isdeleted;
        }
        #endregion

        #region Review

        #region Get Review List By Course Id
        /// <summary>
        /// Get Review List By Course Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/GetCourseReviewListByCourseId")]
        public IHttpActionResult GetCourseReviewListByCourseId(long id)
        {
            List<Review> reviewLst = new List<Review>();
            reviewLst = _userBAL.GetCourseReviewListByCourseId(id);
            return Ok<List<Review>>(reviewLst);
        }
        #endregion

        #region Delete Review by Review Id
        /// <summary>
        /// Delete Review by Review Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/DeleteReviewbyReviewId")]
        public IHttpActionResult DeleteReviewbyReviewId(long id)
        {
            bool isdeleted = false;
            if (id > 0)
            {
                isdeleted = _userBAL.DeleteReviewbyReviewId(id);
            }
            return Ok<bool>(isdeleted);
        }
        #endregion

        #region Save Course Review
        /// <summary>
        /// Save Course Review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("User/SaveCourseReview")]
        public bool SaveCourseReview(Review review)
        {
            return _userBAL.SaveCourseReview(review);
        }
        #endregion


        #endregion
    }
}
