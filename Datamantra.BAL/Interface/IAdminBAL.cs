using Datamantra.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamantra.BAL.Interface
{
    public interface IAdminBAL
    {
        #region Category Methods
        List<Category> GetAllCategories();
        bool SaveCategory(Category category);
        Category GetCategoryById(long id);
        bool DeleteCategoryById(long id);

        #endregion

        #region Course
        List<Course> GetAllCourse();
        Course GetCourseDetailsByCourseId(long id);
        bool SaveCourse(Course course);
        List<Course> GetAllCourseByCategoryId(long CategoryId);
        bool DeleteCourseById(long id);
        List<Course> GetCourseListByStaffId(long id);
        #endregion

        #region roles
        List<Role> GetAllRoles();
        Role GetRolesById(long id);
        bool SaveRole(Role role);
        bool DeleteRoleById(long id);

        #endregion

        #region Users & Roles
        List<User> GetAllActiveUserDetails();
        bool UpdateUserRoles(long userId, long roleId);
        #endregion

    }
}
