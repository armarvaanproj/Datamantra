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
using Datamantra.Common;
namespace Datamantra.BAL.Implementation
{
    //public class UserBAL : DMBaseManager, IUserBAL
    public class UserBAL : IUserBAL
    {
        private IUserDAL _userDAL;
        public UserBAL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        #region   Get User Details By UserId
        /// <summary>
        /// Get User Details By UserId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User GetUserDetailsByUserId(long Id)
        {
            return _userDAL.GetUserDetailsByUserId(Id);
        }
        #endregion

        #region Validate User
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User ValidateUser(User user)
        {
            User userObj = null;
            if (user != null && !string.IsNullOrWhiteSpace(user.Password))
            {
                if (!string.IsNullOrEmpty(user.EmailAddress.Trim()))
                {
                    userObj = _userDAL.GetUserInfoByEmailAddress(user.EmailAddress.Trim());
                    if (userObj != null && userObj.UserId > 0)
                    {
                        string encPassword = Utility.EncryptPassword(user.Password, userObj.Salt);
                        if (encPassword.Equals(userObj.Password))
                            userObj.Status = StatusType.Success;
                        else
                            userObj.Status = StatusType.Success;
                    }
                    else
                    {
                        userObj.Status = StatusType.Success;
                    }
                }
            }
            else
            {
                //Incorrect Password
                userObj = new User();
                userObj.Status = StatusType.Failure;
            }
            return userObj;
        }
        #endregion

        #region Save User Details
        /// <summary>
        /// Save User Details
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public User SaveUser(User User)
        {
            return _userDAL.SaveUser(User);
        }
        #endregion

        #region Save User Details
        /// <summary>
        /// Save User Details
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public List<User> GetStaffDetailsByCourseId(long CourseId)
        {
            return _userDAL.GetStaffDetailsByCourseId(CourseId);
        }
        #endregion

        #region Get All User Details by Role and Status
        /// <summary>
        /// Get All User Details by Role and Status
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="ActiveStatus"></param>
        /// <returns></returns>
        public List<User> GetAllUserDetailsbyRoleandStatus(long RoleId, bool? ActiveStatus)
        {
            return _userDAL.GetAllUserDetailsbyRoleandStatus(RoleId, ActiveStatus);
        }
        #endregion

        #region Delete User by User Id
        /// <summary>
        /// Delete User by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUserbyUserId(long id)
        {
            return _userDAL.DeleteUserbyUserId(id);
        }
        #endregion

        #region Check Is Valid Email
        /// <summary>
        /// Check Is Valid Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckIsValidEmail(string email)
        {
            return _userDAL.CheckIsValidEmail(email);
        }
        #endregion

        #region Change User Status by User Id
        /// <summary>
        /// Change User Status by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeUserStatusbyUserId(long id, bool ActiveStatus)
        {
            return _userDAL.ChangeUserStatusbyUserId(id, ActiveStatus);
        }
        #endregion

        #region Review

        #region Get Course Review List By Course Id
        /// <summary>
        /// Get Course Review List By Course Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Review> GetCourseReviewListByCourseId(long id)
        {
            return _userDAL.GetCourseReviewListByCourseId(id);
        }
        #endregion

        #region Delete Review by Review Id
        /// <summary>
        /// Delete Review by Review Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteReviewbyReviewId(long id)
        {
            return _userDAL.DeleteReviewbyReviewId(id);
        }
        #endregion

        #region Save Course Review
        /// <summary>
        /// Save Course Review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public bool SaveCourseReview(Review review)
        {
            return _userDAL.SaveCourseReview(review);
        }
        #endregion

        #endregion
    }
}
