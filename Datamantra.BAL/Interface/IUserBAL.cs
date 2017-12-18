using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.BAL.Interface
{
    public interface IUserBAL
    {
        User ValidateUser(User user);
        User GetUserDetailsByUserId(long Id);
        User SaveUser(User User);
        List<User> GetStaffDetailsByCourseId(long CourseId);
        List<User> GetAllUserDetailsbyRoleandStatus(long RoleId, bool? ActiveStatus);
        bool DeleteUserbyUserId(long id);
        bool ChangeUserStatusbyUserId(long id, bool ActiveStatus);
        #region Review
        List<Review> GetCourseReviewListByCourseId(long id);
        bool DeleteReviewbyReviewId(long id);
        bool SaveCourseReview(Review review);
        bool CheckIsValidEmail(string email);
        #endregion
    }
}
