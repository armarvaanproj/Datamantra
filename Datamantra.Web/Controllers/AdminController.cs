using Datamantra.Model.Entity;
using Datamantra.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Datamantra.Common;
using System.IO;
using System.Drawing;
namespace Datamantra.Web.Controllers
{
    public class AdminController : Controller
    {
        #region Admin Home Page
        /// <summary>
        ///  Admin Home Page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Category Methods

        #region All Categories
        /// <summary>
        ///  Get All Categories - Partial View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AllCategories()
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
            return PartialView(_lstCategory);
        }
        #endregion

        #region Save Category
        /// <summary>
        /// Save Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCategory(Category category)
        {
            bool is_saved = false;
            //form["editor1"]
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/SaveCategory";
                var _UserResponse = client.PostAsJsonAsync(requestUri, category).Result;
                if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                {
                    is_saved = _UserResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(is_saved, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add & Edit Category
        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AddCategory(long? id)
        {
            Category _category = new Category();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetCategoryById?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    _category = _courseResponse.Content.ReadAsAsync<Category>().Result;
                }
            }
            return PartialView(_category);
        }
        #endregion

        #region View Category
        /// <summary>
        ///  View Category - Partial View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ViewCategory(long id)
        {
            Category _category = new Category();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetCategoryById?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    _category = _courseResponse.Content.ReadAsAsync<Category>().Result;
                }
            }
            return PartialView(_category);
        }
        #endregion

        #region Delete Category
        /// <summary>
        ///  View Category - Partial View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteCategoryById(long id)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/DeleteCategoryById?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Course

        #region All Course - Partial View
        /// <summary>
        ///  Get All Course - Partial View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AllCourse()
        {
            List<Course> _lstcourse = new List<Course>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCourse";
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    _lstcourse = _courseResponse.Content.ReadAsAsync<List<Course>>().Result;
                }
            }
            return PartialView(_lstcourse);
        }
        #endregion

        #region All Course - Partial View
        /// <summary>
        ///  Get All Course - Partial View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllCourse(string searchText)
        {
            List<Course> _lstcourse = new List<Course>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCourse";
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    _lstcourse = _courseResponse.Content.ReadAsAsync<List<Course>>().Result;
                    if (_lstcourse != null && _lstcourse.Any())
                    {
                        _lstcourse = _lstcourse.Where(x => x.CourseName.ToLower().Contains(searchText)).ToList();
                    }
                }
            }
            return Json(_lstcourse, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region View Course
        /// <summary>
        ///  View Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ViewCourse(long? id)
        {
            Course course = new Course();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetCourseDetailsByCourseId?id=" + id;
                    var _courseResponse = client.GetAsync(requestUri).Result;
                    if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                    {
                        course = _courseResponse.Content.ReadAsAsync<Course>().Result;
                    }
                }
            }
            return PartialView(course);
        }
        #endregion

        #region Add Course
        /// <summary>
        ///  View Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AddCourse(long? id)
        {
            Course course = new Course();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetCourseDetailsByCourseId?id=" + id;
                    var _courseResponse = client.GetAsync(requestUri).Result;
                    if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                    {
                        course = _courseResponse.Content.ReadAsAsync<Course>().Result;
                    }
                }
            }
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCategories";
                var __lstCategoryResponse = client.GetAsync(requestUri).Result;
                if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                {
                    var Categorylst = __lstCategoryResponse.Content.ReadAsAsync<List<Category>>().Result;
                    ViewData["Categorylist"] = new SelectList(Categorylst, "CategoryId", "CategoryName");
                }
            }
            return PartialView(course);
        }
        #endregion

        #region Save Course
        /// <summary>
        ///  Save Course
        /// </summary>
        /// <param name="form"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveCourse(Course course, FormCollection form)
        {
            bool is_saved = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/SaveCourse";
                var _UserResponse = client.PostAsJsonAsync(requestUri, course).Result;
                if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                {
                    is_saved = _UserResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(is_saved, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get All Course By Category Id
        /// <summary>
        /// Get All Course Details By Category Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetAllCourseByCategoryId(long CategoryId)
        {
            List<Course> Courselst = new List<Course>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCourseByCategoryId";
                var __lstCategoryResponse = client.GetAsync(requestUri).Result;
                if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                {
                    Courselst = __lstCategoryResponse.Content.ReadAsAsync<List<Course>>().Result;
                }
            }
            return PartialView(Courselst);
        }
        #endregion

        #region Delete Course By Id
        /// <summary>
        ///  Delete Course By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteCourseById(long id)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/DeleteCourseById?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Roles

        #region All Roles
        /// <summary>
        /// All Roles - Partial View Get 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AllRoles()
        {
            List<Role> _lstRole = new List<Role>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllRoles";
                var _rolesResponse = client.GetAsync(requestUri).Result;
                if (_rolesResponse != null && _rolesResponse.IsSuccessStatusCode)
                {
                    _lstRole = _rolesResponse.Content.ReadAsAsync<List<Role>>().Result;
                }
            }
            return PartialView(_lstRole);
        }
        #endregion

        #region Add Role
        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AddRole(long? id)
        {
            Role role = new Role();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetRolesById?id=" + id;
                    var _courseResponse = client.GetAsync(requestUri).Result;
                    if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                    {
                        role = _courseResponse.Content.ReadAsAsync<Role>().Result;
                    }
                }
            }
            return PartialView(role);
        }
        #endregion

        #region View Role
        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _ViewRole(long? id)
        {
            Role role = new Role();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/GetRolesById?id=" + id;
                    var _courseResponse = client.GetAsync(requestUri).Result;
                    if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                    {
                        role = _courseResponse.Content.ReadAsAsync<Role>().Result;
                    }
                }
            }
            return PartialView(role);
        }
        #endregion

        #region Save Roles
        /// <summary>
        /// Save Roles
        /// </summary>
        /// <param name="form"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRoles(FormCollection form, Role role)
        {
            bool is_saved = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/SaveRole";
                var _UserResponse = client.PostAsJsonAsync(requestUri, role).Result;
                if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                {
                    is_saved = _UserResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(is_saved, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Role By Id
        /// <summary>
        ///  Delete Role By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteRoleById(long id)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/DeleteRoleById?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Users & Roles

        #region All User Roles
        /// <summary>
        /// All User Roles - Partial View Get 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AllUserRoles()
        {
            List<User> _activeuserlst = new List<User>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllActiveUserDetails";
                var _userResponse = client.GetAsync(requestUri).Result;
                if (_userResponse != null && _userResponse.IsSuccessStatusCode)
                {
                    _activeuserlst = _userResponse.Content.ReadAsAsync<List<User>>().Result;
                }
            }
            return PartialView(_activeuserlst);
        }
        #endregion


        #endregion

        #region User Details

        #region Get All User
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _AllUserbyRoleId(long id, bool ActiveStatus)
        {
            List<User> userlst = new List<User>();
            if (id > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "User/GetAllUserDetailsbyRoleandStatus?RoleId=" + id + "&ActiveStatus=" + ActiveStatus;
                    var userRes = client.GetAsync(requestUri).Result;
                    if (userRes != null && userRes.IsSuccessStatusCode)
                    {
                        userlst = userRes.Content.ReadAsAsync<List<User>>().Result;
                    }
                }
                ViewData["RoleId"] = id;
                ViewData["ActiveStatus"] = ActiveStatus;
            }
            return PartialView(userlst);
        }
        #endregion

        #region Add User
        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _AddUser(int? id, int? RoleId)
        {
            User user = new User();
            using (var client = new DatamantraAPIClient())
            {
                if (id > 0)
                {
                    string requestUri = "User/GetUserDetailsByUserId?userId=" + id;
                    var _courseResponse = client.GetAsync(requestUri).Result;
                    if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                    {
                        user = _courseResponse.Content.ReadAsAsync<User>().Result;
                    }
                }
                string reqUri = "Admin/GetAllCategories";
                var __lstCategoryResponse = client.GetAsync(reqUri).Result;
                if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                {
                    var Categorylst = __lstCategoryResponse.Content.ReadAsAsync<List<Category>>().Result;
                    ViewData["Categorylist"] = new SelectList(Categorylst, "CategoryId", "CategoryName");
                }
            }
            user.RoleId = RoleId ?? 0;
            return PartialView(user);
        }
        #endregion


        #region  Course list By Category Id - json
        /// <summary>
        /// Course list By Category Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllCourselistByCategoryId(long CategoryId)
        {
            List<Course> Courselst = new List<Course>();
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "Admin/GetAllCourseByCategoryId?CategoryId=" + CategoryId;
                var __lstCategoryResponse = client.GetAsync(requestUri).Result;
                if (__lstCategoryResponse != null && __lstCategoryResponse.IsSuccessStatusCode)
                {
                    Courselst = __lstCategoryResponse.Content.ReadAsAsync<List<Course>>().Result;
                }
            }
            return Json(Courselst, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save User
        /// <summary>
        /// Get All User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveUser(User user, FormCollection form)
        {
            user.StaffCourseLst = new List<CourseAuthorMapping>();
            bool is_saved = false;
            for (int i = 1; i < 10; i++)
            {
                if (form["CategoryId" + i] != null)
                {
                    CourseAuthorMapping _CourseAuthorMapping = new CourseAuthorMapping();
                    _CourseAuthorMapping.CourseId = Utility.GetLong(form["CourseId" + i]);
                    _CourseAuthorMapping.CategoryId = Utility.GetLong(form["CategoryId" + i]);
                    _CourseAuthorMapping.UserId = user.UserId;
                    user.StaffCourseLst.Add(_CourseAuthorMapping);
                }
            }

            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/SaveUser";
                var _UserResponse = client.PostAsJsonAsync(requestUri, user).Result;
                if (_UserResponse != null && _UserResponse.IsSuccessStatusCode)
                {
                    var userinfo = _UserResponse.Content.ReadAsAsync<User>().Result;
                    is_saved = (userinfo.UserId > 0) ? true : false;

                }
            }
            return Json(is_saved, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete User By User Id
        /// <summary>
        ///  Delete User By User Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteUserbyUserId(long id)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/DeleteUserbyUserId?id=" + id;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Change User Status by User Id
        /// <summary>
        /// Change User Status by User Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ChangeUserStatusbyUserId(long id, bool Status)
        {
            bool IsDeleted = false;
            using (var client = new DatamantraAPIClient())
            {
                string requestUri = "User/ChangeUserStatusbyUserId?id=" + id + "&ActiveStatus=" + Status;
                var _courseResponse = client.GetAsync(requestUri).Result;
                if (_courseResponse != null && _courseResponse.IsSuccessStatusCode)
                {
                    IsDeleted = _courseResponse.Content.ReadAsAsync<bool>().Result;
                }
            }
            return Json(IsDeleted, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region File Upload (Image) - Post
        [HttpPost]
        public JsonResult _LogoFileUpload(FormCollection form, HttpPostedFileBase file1)
        {
            long userId = Utility.GetUserIdFromSession();
            string filePath = string.Empty;
            string fileName = string.Empty;
            if (Request != null && Request.Files != null && Request.Files.Count > 0)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    int maxFileContentLength = Utility.GetInt(Utility.GetAppSettings(Constants.MaxFileContentLength));
                    int maxImageHeight = Utility.GetInt(Utility.GetAppSettings(Constants.ImageUploadMaxHeight));
                    int maxImageWidth = Utility.GetInt(Utility.GetAppSettings(Constants.ImageUploadMaxWidth));
                    var httpPostedFileBase = Request.Files["AttachFile"];
                    if (httpPostedFileBase != null && (hpf != null && httpPostedFileBase.ContentLength > 0 && httpPostedFileBase.ContentLength <= maxFileContentLength)) // If uploaded synchronously
                    {
                        var postedFileBase = Request.Files[Constants.AttachFile];
                        if (postedFileBase != null)
                        {
                            BinaryReader reader = new BinaryReader(postedFileBase.InputStream);
                            byte[] imageBinary = reader.ReadBytes((int)postedFileBase.ContentLength);

                            Image image = byteArrayToImage(imageBinary);
                            string fileNameNew = postedFileBase.FileName;

                            Bitmap bmpImage = new Bitmap(image);

                            Color tempImgColor = ImageProcessing.GetMostUsedColor(bmpImage);
                            string themeColor = "rgb(" + tempImgColor.R + "," + tempImgColor.G + "," + tempImgColor.B + ")";


                            byte[] byteimage = imageBinary;

                            string imageName = fileNameNew;
                            if (!string.IsNullOrWhiteSpace(imageName))
                            {
                                string extension = imageName.Substring(imageName.LastIndexOf("."));
                                imageName = imageName.Replace(extension, "");
                                imageName = Utility.RemoveSpecialCharacters(imageName);
                                if (imageName.Length > 100)
                                {
                                    imageName = imageName.Substring(0, 95);
                                }
                                imageName += extension;
                            }
                            fileNameNew = imageName;


                            //CustomWebsiteResponse customDomainDetails = new CustomWebsiteResponse();
                            //if (byteimage != null)
                            //{
                            //    if (userId > 0)
                            //    {
                            //        using (var client = new ACAFormsAPIClient())
                            //        {
                            //            customDomainDetails.File = byteimage;
                            //            customDomainDetails.BusinessLogoName = fileNameNew;
                            //            customDomainDetails.ColorCode = themeColor;
                            //            customDomainDetails.CustomDomainId = Utility.GetLong(form["hdnCustomDomainId"]);
                            //            customDomainDetails.DomainName = form["hdnDomainName"];
                            //            customDomainDetails.UserId = userId;
                            //            customDomainDetails.BusinessName = Utility.GetBusinessNameFromSession();

                            //            var callFrom = form["hdnCustomWebCallFrom"] != null && form["hdnCustomWebCallFrom"] != "" ? form["hdnCustomWebCallFrom"].ToString() : "";

                            //            if (!string.IsNullOrWhiteSpace(callFrom) && callFrom.ToLower() == "client")
                            //            {
                            //                customDomainDetails.BusinessId = Utility.GetBusinessIdFromSession();
                            //            }

                            //            string requestUri = "Settings/SaveCustomDomainBusinessImage";
                            //            APIRequestUtility.GenerateAuthHeader(client, requestUri, "POST");
                            //            var _response = client.PostAsJsonAsync(requestUri, customDomainDetails).Result;
                            //            if (_response.IsSuccessStatusCode)
                            //            {
                            //                customDomainDetails = _response.Content.ReadAsAsync<CustomWebsiteResponse>().Result;
                            //                customDomainDetails.ThemeColor = themeColor;
                            //                return Json(customDomainDetails, JsonRequestBehavior.AllowGet);
                            //            }
                            //        }

                            //    }
                            //}
                        }
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Byte to Image Convertor
        public Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            Image img = Image.FromStream(memstr);
            return img;
        }
        #endregion

        #region _DisplayLogoImage
        public JsonResult _DisplayLogoImage(string path, int? fixedWidth, int? fixedHeight)
        {
            string imageString = string.Empty;
            if (!string.IsNullOrWhiteSpace(path))
            {
                imageString = Utility.GetFormattedImage(path, fixedHeight ?? 150, fixedWidth ?? 150);
            }
            return Json(imageString, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Update User Roles
        /// <summary>
        ///  Update User Roles
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateUserRoles(long roleId, long userId)
        {
            if (roleId > 0 && userId > 0)
            {
                using (var client = new DatamantraAPIClient())
                {
                    string requestUri = "Admin/UpdateUserRoles?userId=" + userId + "&roleId=" + roleId;
                    var _userResponse = client.GetAsync(requestUri).Result;
                    if (_userResponse != null && _userResponse.IsSuccessStatusCode)
                    {
                         bool isupdated = _userResponse.Content.ReadAsAsync<bool>().Result;
                         if (!isupdated) roleId = 0;
                    }
                }
            }
            return Json(roleId, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}