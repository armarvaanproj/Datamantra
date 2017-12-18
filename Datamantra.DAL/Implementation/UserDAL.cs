using Datamantra.DAL.Handlers;
using Datamantra.DAL.Interface;
using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using Datamantra.Common;
namespace Datamantra.DAL.Implementation
{
    public class UserDAL : IUserDAL
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

        public User GetUserInfoByEmailAddress(string email)
        {
            var user = new User();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                user = dbConnection.Query<User>(SqlQueryStrings.GetUserDetailsbyEmailAddress, new { email }).SingleOrDefault();
            }
            return user;
        }
        #region   Get User Details By UserId
        /// <summary>
        /// Get User Details By UserId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns> 
        public User GetUserDetailsByUserId(long id)
        {
            var user = new User();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                user = dbConnection.Query<User>(SqlQueryStrings.GetUserDetailsByUserId, new { UserId = id }).SingleOrDefault();
                user.SelectedCategorylst = dbConnection.Query<SelectedCategory>(SqlQueryStrings.GetAllCategoriesList).ToList();
                user.StaffCourseLst = dbConnection.Query<CourseAuthorMapping>(SqlQueryStrings.GetStaffCourseListByUserId, new { UserId = id }).ToList();
                if (user.StaffCourseLst.Any())
                {
                    foreach (var item in user.StaffCourseLst)
                    {
                        item.Courselst = dbConnection.Query<SelectedCourse>(SqlQueryStrings.GetSelectedCourseLstByCategoryId, new { CategoryId = item.CategoryId }).ToList();
                    }
                }
            }
            return user;
        }
        #endregion

        #region Save User Details
        public User SaveUser(User User)
        {
            using (IDbConnection dbConnection = ACADBConnection)
            {
                if (User.UserId > 0)
                {
                    User.ModifiedDate = DateTime.Now;
                    dbConnection.Query<User>(SqlQueryStrings.UpdateUserbyUserId, User).SingleOrDefault();

                }
                else
                {

                    var existinguser = dbConnection.Query<User>(SqlQueryStrings.GetUserDetailsbyEmailAddress, new { email = User.EmailAddress }).SingleOrDefault();
                    if (existinguser != null && existinguser.UserId > 0)
                    {
                        User.Status = StatusType.Duplicate;
                    }
                    else
                    {
                        User.CreatedDate = DateTime.Now;
                        User.Salt = Utility.CreateSalt(6);
                        User.RoleId = Utility.GetInt(RoleType.STUDENT);
                        User.Password = Utility.EncryptPassword(User.Password, User.Salt);
                        var userid = dbConnection.Query<int>(SqlQueryStrings.SaveUserDetails, User).FirstOrDefault();
                        User.UserId = userid;
                    }

                }
                if (User.StaffCourseLst != null && User.StaffCourseLst.Any())
                {
                    dbConnection.Query<CourseAuthorMapping>(SqlQueryStrings.DeleteCourseMappingbyUserId, new { UserId = User.UserId }).SingleOrDefault();
                    foreach (var item in User.StaffCourseLst)
                    {
                        if (item.CourseId > 0 && item.CategoryId > 0)
                        {
                            item.ModifiedDate = DateTime.Now;
                            item.CreatedBy = RoleType.ADMIN.ToString();
                            var isavailable = dbConnection.Query<CourseAuthorMapping>(SqlQueryStrings.CheckisCourseStaffMappingAvaliable, new { CourseId = item.CourseId, UserId = User.UserId }).SingleOrDefault();
                            if (isavailable != null && isavailable.AuthorId > 0)
                            {
                                dbConnection.Query<CourseAuthorMapping>(SqlQueryStrings.UpdateCourseMappingbyAuthorId, new { AuthorId = isavailable.AuthorId }).SingleOrDefault();
                            }
                            else
                            {
                                item.CreatedDate = DateTime.Now;
                                item.UserId = User.UserId;
                                dbConnection.Query<CourseAuthorMapping>(SqlQueryStrings.SaveCourseStaffMapping, item).SingleOrDefault();
                            }
                        }
                    }
                }


            }
            return User;
        }
        #endregion

        #region Save User Details
        public List<User> GetStaffDetailsByCourseId(long CourseId)
        {
            var authorlst = new List<User>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                authorlst = dbConnection.Query<User>(SqlQueryStrings.GetStaffDetailsByCourseId, new { CourseId = CourseId }).ToList();
            }
            return authorlst;
        }
        #endregion
      

        #region Get All StaffDetails
        public List<User> GetAllUserDetailsbyRoleandStatus(long RoleId, bool? ActiveStatus)
        {
            var authorlst = new List<User>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                if (ActiveStatus == null)
                {
                    authorlst = dbConnection.Query<User>(SqlQueryStrings.GetAllStaffDetails, new { RoleId = RoleId }).ToList();
                }
                else
                {
                    int Status = (ActiveStatus == true) ? 1 : 0;
                    authorlst = dbConnection.Query<User>(SqlQueryStrings.GetAllStaffDetailsbyActiveStatus, new { RoleId = RoleId, Status = Status }).ToList();
                }
            }
            return authorlst;
        }
        #endregion

        #region  Delete User By user Id
        /// <summary>
        /// Delete user By user Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUserbyUserId(long id)
        {
            bool isdeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<bool>(SqlQueryStrings.DeleteUserbyUserId, new { UserId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                dbConnection.Query<bool>(SqlQueryStrings.DeleteCourseAuthorByUserId, new { UserId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
                isdeleted = true;
            }
            return isdeleted;
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
            bool isdeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                int Status = (ActiveStatus == true) ? 0 : 1;
                dbConnection.Query<bool>(SqlQueryStrings.ChangeUserStatusbyUserId, new { IsActive = Status, UserId = id, ModifiedDate = DateTime.Now, ModifiedBy = "" }).SingleOrDefault();
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
        public bool CheckIsValidEmail(string email)
        {
            bool isduplicate = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                var existinguser = dbConnection.Query<User>(SqlQueryStrings.GetUserDetailsbyEmailAddress, new { email = email }).SingleOrDefault();
                if (existinguser != null && existinguser.UserId > 0)
                {
                    isduplicate = true;
                }
                else
                {
                    isduplicate = false;
                }
            }
            return isduplicate;
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
            var reviewlst = new List<Review>();
            using (IDbConnection dbConnection = ACADBConnection)
            {
                reviewlst = dbConnection.Query<Review>(SqlQueryStrings.GetCourseReviewListByCourseId, new { CourseId = id }).ToList();
            }
            return reviewlst;
        }
        #endregion

        #region Save Course Review
        /// <summary>
        /// Save Course Review  
        /// </summary>
        /// <param name="Review"></param>
        /// <returns></returns>
        public bool SaveCourseReview(Review review)
        {
            bool isSaved = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                if (review.ReviewId > 0)
                {
                    review.ModifiedDate = DateTime.Now;
                    review.ModifiedBy = review.UserId.ToString();
                    dbConnection.Query<Review>(SqlQueryStrings.UpdateCourseReview, review).SingleOrDefault();
                    isSaved = true;
                }
                else if (review.CourseId > 0)
                {

                    review.CreatedDate = DateTime.Now;
                    review.CreatedBy = review.UserId.ToString();
                    var reviewId = dbConnection.Query<int>(SqlQueryStrings.SaveCourseReview, review).FirstOrDefault();
                    isSaved = (reviewId > 0) ? true : false;
                }
            }
            return isSaved;
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
            bool isdeleted = false;
            using (IDbConnection dbConnection = ACADBConnection)
            {
                dbConnection.Query<bool>(SqlQueryStrings.DeleteReviewbyReviewId, new { ReviewId = id, ModifiedDate = DateTime.Now, ModifiedBy = Utility.GetUserIdFromSession() }).SingleOrDefault();
                isdeleted = true;
            }
            return isdeleted;
        }
        #endregion

        #endregion
    }
}
