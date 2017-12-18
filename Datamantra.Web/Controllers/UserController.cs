using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using Datamantra.Model.Entity;
using Datamantra.Common;

using System.Web.Security;
using System.Net.Http;
using Datamantra.Web.Extensions;
namespace Datamantra.Web.Controllers
{
    public class UserController : Controller
    {
        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Login
        /// <summary>
        /// _Login
        /// </summary>
        /// <returns></returns>
        public ActionResult _Login()
        {
            return PartialView();
        }
        #endregion

        #region Login - post
        /// <summary>
        /// Login - post
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User user)
        {
            User userRes = new User();
            bool isSignedin = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/Login";
                var _loginResponse = client.PostAsJsonAsync(requestUri, user).Result;
                if (_loginResponse != null && _loginResponse.IsSuccessStatusCode)
                {
                    userRes = _loginResponse.Content.ReadAsAsync<User>().Result;
                    if (userRes != null && userRes.Status == StatusType.Success)
                    {
                        //2nd Param for Remember Me 
                        Session[SessionItemKey.UserId] = userRes.UserId;
                        Session[SessionItemKey.EmailAddress] = userRes.EmailAddress.ToString();
                        Session[SessionItemKey.UserName] = userRes.UserName;
                        Session[SessionItemKey.RoleID] = Utility.GetInt(userRes.RoleId);
                        FormsAuthentication.SetAuthCookie(userRes.EmailAddress, false);
                        isSignedin = true;
                        //return RedirectToAction("Courses", "Datamantra");
                        //return Json(isSignedin, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(isSignedin, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Sign Up
        /// <summary>
        ///  Sign Up
        /// </summary>
        /// <returns></returns>
        public ActionResult _SignUp()
        {
            return PartialView();
        }
        #endregion

        #region Sign Up-post
        /// <summary>
        /// SignUp - post
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            User userRes = new User();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/SaveUser";
                var _UserResponse = client.PostAsJsonAsync(requestUri, user).Result;
                if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                {
                    userRes = _UserResponse.Content.ReadAsAsync<User>().Result;
                    if (userRes != null && userRes.Status == StatusType.Success)
                    {
                        SessionEncryption.SetSessionValue(SessionItemKey.RoleID, userRes.RoleId);
                        SessionEncryption.SetSessionValue(SessionItemKey.UserId, userRes.UserId);
                        SessionEncryption.SetSessionValue(SessionItemKey.UserName, userRes.UserName);
                        SessionEncryption.SetSessionValue(SessionItemKey.EmailAddress, userRes.EmailAddress);
                    }
                }
            }
            return Json(userRes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Log Off
        [HttpGet]
        public ActionResult LogOff()
        {

            //Log out from sub user redirect to sub user log in
            //long subUserId = Utility.GetLong(SessionEncryption.GetValueFromSession(SessionItemKey.SubUserId));

            if (Session != null)
            {
                string queryString = string.Empty;
                SessionEncryption.SetSessionValue(SessionItemKey.EmailAddress, null);
                SessionEncryption.SetSessionValue(SessionItemKey.UserId, null);
                SessionEncryption.SetSessionValue(SessionItemKey.UserName, null);
            }
            FormsAuthentication.SignOut();
            if (Session != null)
            {
                Session.Abandon();
                Session.Clear();
            }
            return Redirect(Url.RouteUrl(new { Controller = "Datamantra", Action = "Index" }));
        }
        #endregion

        public ActionResult _ForgotPassword()
        {
            return PartialView();
        }


        #region Check Is Valid Email
        /// <summary>
        ///  Check Is Valid Email
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckIsValidEmail(string email)
        {
            bool isduplicate = false;

            try
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "User/CheckIsValidEmail?email=" + email;
                    var _UserResponse = client.GetAsync(requestUri).Result;

                    TelemetryLogger.TrackTrace("Got User Res");
                    if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                    {
                        var userRes = _UserResponse.Content.ReadAsAsync<string>().Result;
                        if (userRes != null && userRes == "True")
                        {
                            isduplicate = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TelemetryLogger.TrackException(ex, "CheckIsValidEmail");
            }
            
            return Json(isduplicate, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  Save Review - post
        /// <summary>
        /// Save Review - post
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveReview(string comments, long courseId, long? reviewId, decimal? rating)
        {
            bool issaved = false;
            Review review = new Review();
            review.Comments = comments;
            review.UserId = Utility.GetUserIdFromSession();
            review.CourseId = courseId;
            review.ReviewId = reviewId ?? 0;
            review.Rating = rating ?? 0;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/SaveCourseReview";
                var _ReviewResponse = client.PostAsJsonAsync(requestUri, review).Result;
                if (_ReviewResponse != null && _ReviewResponse.IsSuccessStatusCode)
                {
                    issaved = _ReviewResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(issaved, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Delete Review by Review Id
        /// <summary>
        /// Delete Review by Review Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteReviewbyReviewId(long id)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/DeleteReviewbyReviewId?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

