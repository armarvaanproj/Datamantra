using Datamantra.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datamantra.Model.Entity;
using System.Net.Http;
namespace Datamantra.Web.Controllers
{
    public class DatamantraController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [Route("Courses")]
        public ViewResult Courses()
        {
            List<Category> _lstCategory = new List<Category>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCategories";
                var __lstCategoryResponse = client.GetAsync(requestUri).Result;
                if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                {
                    _lstCategory = __lstCategoryResponse.Content.ReadAsAsync<List<Category>>().Result;
                }
            }
            return View(_lstCategory);
        }

        [Route("ShowCourses")]
        public PartialViewResult _ShowCourses(long CategoryId)
        {
            List<Course> Courselst = new List<Course>();
            if (CategoryId > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetAllCourseByCategoryId?CategoryId=" + CategoryId;
                    var __lstCategoryResponse = client.GetAsync(requestUri).Result;
                    if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                    {
                        Courselst = __lstCategoryResponse.Content.ReadAsAsync<List<Course>>().Result;
                    }
                }
            }
            return PartialView(Courselst);
        }

        [Route("CourseDetail")]
        public ViewResult CourseDetail(long? id)
        {
            Course course = new Course();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetCourseDetailsByCourseId?id=" + id;
                    var courseRes = client.GetAsync(requestUri).Result;
                    if (courseRes != null && courseRes.IsSuccessStatusCode)
                    {
                        course = courseRes.Content.ReadAsAsync<Course>().Result;
                    }
                }
            }
            return View(course);
        }

        [Route("_CourseDetail")]
        public PartialViewResult _CourseDetail(long? id)
        {
            Course course = new Course();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetCourseDetailsByCourseId?id=" + id;
                    var courseRes = client.GetAsync(requestUri).Result;
                    if (courseRes != null && courseRes.IsSuccessStatusCode)
                    {
                        course = courseRes.Content.ReadAsAsync<Course>().Result;
                    }
                }
            }
            return PartialView(course);
        }


        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _StaffList(long? CourseId)
        {
            //id= course id
            List<User> userlst = new List<User>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/GetStaffDetailsByCourseId?CourseId=" + CourseId;
                var userRes = client.GetAsync(requestUri).Result;
                if (userRes != null && userRes.IsSuccessStatusCode)
                {
                    userlst = userRes.Content.ReadAsAsync<List<User>>().Result;
                }
            }
            return PartialView(userlst);
        }
        #endregion

        #region Get All Staffs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Staffs")]
        public ActionResult Staffs()
        {
            //id= course id
            List<User> userlst = new List<User>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/GetAllUserDetailsbyRoleandStatus?RoleId=2&ActiveStatus=true";
                var userRes = client.GetAsync(requestUri).Result;
                if (userRes != null && userRes.IsSuccessStatusCode)
                {
                    userlst = userRes.Content.ReadAsAsync<List<User>>().Result;
                }
            }
            return View(userlst);
        }
        #endregion

        #region Get All Staffs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("StaffDetail")]
        public ActionResult StaffDetail(long id)
        {
            //id= course id
            User user = new User();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/GetUserDetailsByUserId?userId=" + id;
                var userRes = client.GetAsync(requestUri).Result;
                if (userRes != null && userRes.IsSuccessStatusCode)
                {
                    user = userRes.Content.ReadAsAsync<User>().Result;
                }
            }
            return View(user);
        }
        #endregion

        #region _Course List By Staff Id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _CourseListByStaffId(long? id)
        {
            //id= course id
            List<Course> courselst = new List<Course>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetCourseListByStaffId?id=" + id;
                var userRes = client.GetAsync(requestUri).Result;
                if (userRes != null && userRes.IsSuccessStatusCode)
                {
                    courselst = userRes.Content.ReadAsAsync<List<Course>>().Result;
                }
            }
            return PartialView(courselst);
        }
        #endregion

        #region  Course Review List By Course Id
        /// <summary>
        ///   Course Review List By Course Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _CourseReviewList(long? id)
        {
            //id= course id
            List<Review> reviewLst = new List<Review>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/GetCourseReviewListByCourseId?id=" + id;
                var userRes = client.GetAsync(requestUri).Result;
                if (userRes != null && userRes.IsSuccessStatusCode)
                {
                    reviewLst = userRes.Content.ReadAsAsync<List<Review>>().Result;
                }
            }
            ViewData["CourseId"] = id ?? 0;
            return PartialView(reviewLst);
        }
        #endregion
    }
}