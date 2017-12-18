using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datamantra.DAL.Handlers
{
    public class SqlQueryStrings
    {

        //   User Details
        public const string GetUserDetailsbyEmailAddress = "SELECT UserId UserId,UserName UserName,Password Password,Salt Salt,EmailAddress EmailAddress,RoleId RoleId FROM [Datamantra].[User] WHERE  IsDeleted=0 and LOWER(EmailAddress) = @email";
        public const string SaveUserDetails = "Insert into [Datamantra].[User] (UserName,Password,Designation,Image,Description,RoleId,IsActive,Rating,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,EmailAddress,Salt,IsDeleted) Values (@UserName,@Password,@Designation,@Image,@Description,@RoleId,@IsActive,@Rating,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,@EmailAddress,@Salt,@IsDeleted) SELECT CAST(scope_identity() AS int)";
        public const string GetUserDetailsByUserId = "select UserId UserId,UserName UserName,Designation Designation,Image Image,Description Description,Rating Rating,EmailAddress EmailAddress,RoleId RoleId from [Datamantra].[user] where  UserId=@UserId and IsDeleted=0";
        public const string GetAllActiveUserDetails = "select UserId UserId,UserName UserName,Designation Designation,Image Image,Description Description,Rating Rating,EmailAddress EmailAddress,RoleId RoleId from [Datamantra].[user] where   IsDeleted=0 and IsActive =1";
        public const string UpdateUserRoles = "update datamantra.[User]  set   RoleId=@roleId  where UserId=@userId and IsDeleted=0";
        public const string DeleteUserbyUserId = "update [Datamantra].[User] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where UserId=@UserId";
        public const string InActiveUserbyUserId = "update [Datamantra].[User] set IsActive=@IsActive,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where UserId=@UserId and IsDeleted=0";
        public const string UpdateUserbyUserId = "update datamantra.[User]  set  UserName = @UserName,Designation=@Designation,Image=@Image,Description=@Description,RoleId=@RoleId,IsActive =@IsActive,Rating=@Rating,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,EmailAddress=@EmailAddress where UserId=@UserId and IsDeleted=0";
        public const string DeleteCourseAuthorByUserId = "update [Datamantra].[CourseAuthorMapping] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where   UserId=@UserId";
        public const string ChangeUserStatusbyUserId = "update [Datamantra].[User] set IsActive=@IsActive,ModifiedDate=@ModifiedDate,ModifiedBy=@ModifiedBy where UserId=@UserId";
        //Staff
        public const string GetStaffDetailsByCourseId = "select m.AuthorId AuthorId,m.CourseId,m.UserId UserId,u.UserName UserName,u.Designation Designation,u.Image Image,u.Description Description,u.EmailAddress EmailAddress from [Datamantra].[user] u join [Datamantra].[CourseAuthorMapping] m on u.UserId=m.UserId where m.CourseId=@CourseId and u.IsDeleted= 0 and m.IsDeleted=0 and u.IsActive=1";
        public const string GetAllStaffDetails = "select UserId UserId,UserName UserName,Designation Designation,Image Image,Description Description,Rating Rating,EmailAddress EmailAddress,RoleId RoleId,IsActive IsActive  from [Datamantra].[user] where RoleId=@RoleId and IsDeleted=0";
        public const string GetAllStaffDetailsbyActiveStatus = "select UserId UserId,UserName UserName,Designation Designation,Image Image,Description Description,Rating Rating,EmailAddress EmailAddress,RoleId RoleId,IsActive IsActive from [Datamantra].[user] where RoleId=@RoleId and IsActive=@Status and IsDeleted=0";

        public const string SaveCourseStaffMapping = "Insert into [Datamantra].[CourseAuthorMapping] (CourseId,CategoryId ,UserId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted)values (@CourseId,@CategoryId,@UserId,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,0) SELECT CAST(scope_identity() AS int)";
        public const string DeleteCourseMappingbyUserId = "update [Datamantra].[CourseAuthorMapping] set IsDeleted =1 where UserId=@UserId";
        public const string CheckisCourseStaffMappingAvaliable = "select AuthorId AuthorId,CourseId CourseId,UserId UserId from [Datamantra].[CourseAuthorMapping] where CourseId=@CourseId and UserId=@UserId";
        public const string UpdateCourseMappingbyAuthorId = "update [Datamantra].[CourseAuthorMapping] set IsDeleted =0 where AuthorId=@AuthorId";
        public const string GetStaffCourseListByUserId = "select CategoryId CategoryId,CourseId CourseId,UserId UserId,AuthorId AuthorId from [Datamantra].[CourseAuthorMapping] where IsDeleted=0 and UserId=@UserId";
        public const string GetSelectedCourseLstByCategoryId = "select CourseId CourseId,CourseName CourseName from [Datamantra].[Course] where IsDeleted=0 and CategoryId=@CategoryId";
        public const string DeleteCourseMappingbyCategoryId = "update [Datamantra].[CourseAuthorMapping] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where  CategoryId=@CategoryId";
        public const string DeleteCourseAuthorByCourseId = "update [Datamantra].[CourseAuthorMapping] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where   CourseId=@CourseId";

        // Categories
        public const string GetCategoryDetails = "SELECT CategoryId CategoryId,CategoryName CategoryName FROM dbo.[Category] WHERE  Is_deleted=0 ";
        public const string GetAllCategoriesList = "select CategoryId CategoryId,CategoryName CategoryName,SubCategoryId SubCategoryId  from [Datamantra].[Category] where IsDeleted=0";
        public const string GetAllSubCategoriesByCategoryId = "select CategoryId CategoryId,CategoryName CategoryName,SubCategoryId SubCategoryId  from [Datamantra].[Category] where IsDeleted=0 and SubCategoryId=@SubCategoryId";
        public const string SaveCategory = "Insert into [Datamantra].[Category] (CategoryName,SubCategoryId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted) values(@CategoryName,@SubCategoryId,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,@IsDeleted) SELECT CAST(scope_identity() AS int)";
        public const string GetCategoryByCategoryId = "select CategoryId CategoryId,CategoryName CategoryName,SubCategoryId SubCategoryId  from [Datamantra].[Category] where IsDeleted=0 and CategoryId=@CategoryId";
        public const string UpdateCategoryById = "update [Datamantra].[Category] set CategoryName=@CategoryName,SubCategoryId=@SubCategoryId, ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where IsDeleted=0 and CategoryId=@CategoryId";
        public const string DeleteCategoryById = "update [Datamantra].[Category] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where CategoryId=@CategoryId";

        //Roles
        public const string GetAllRoles = "SELECT  RoleId RoleId ,RoleName RoleName,CreatedBy CreatedBy,CreatedDate CreatedDate,ModifiedBy ModifiedBy ,ModifiedDate ModifiedDate from [Datamantra].[Role] where IsDeleted=0";
        public const string GetRolesById = "SELECT  RoleId RoleId ,RoleName RoleName,CreatedBy CreatedBy,CreatedDate CreatedDate,ModifiedBy ModifiedBy ,ModifiedDate ModifiedDate from [Datamantra].[Role] where IsDeleted=0 and RoleId=@RoleId ";
        public const string DeleteRoleById = "update [Datamantra].[Role] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where RoleId=@RoleId";
        public const string SaveRole = "insert into [Datamantra].[Role] (RoleName,CreatedBy,CreatedDate,IsDeleted) values (@RoleName,@CreatedBy,@CreatedDate,0) SELECT CAST(scope_identity() AS int)";
        public const string UpdateRoleById = "update [Datamantra].[Role] set RoleName=@RoleName,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where RoleId=@RoleId and IsDeleted=0";

        //Course
        public const string GetCourseListByStaffId = "select c.CourseId CourseId,c.CourseName CourseName,c.Rating Rating, c.Price Price,c.DiscountedPrice DiscountedPrice,c.EnrolledCount EnrolledCount,c.Image Image,c.ShortDescription ShortDescription,c.LongDescription LongDescription from Datamantra.[User] u join Datamantra.CourseAuthorMapping ca on  u.UserId = ca.UserId join Datamantra.Course c on ca.CourseId = c.CourseId where c.IsDeleted=0 and ca.IsDeleted=0 and u.IsDeleted=0 and u.IsActive=1 and u.UserId=@UserId";
        //public const string GetCourseDetails = "SELECT Course_Id CourseId,Name Name,Category_Id CategoryId,Rating Rating,Market_Price Market_Price,Our_Price OurPrice,Visitors Visitors,Image Image,Short_Description ShortDescription ,Long_Description LongDescrption FROM dbo.[Course]where Is_Deleted=0";
        public const string GetAllCourse = "select CourseId CourseId,CourseName CourseName,Rating Rating,Price Price,DiscountedPrice DiscountedPrice,EnrolledCount EnrolledCount,Image Image,ShortDescription ShortDescription,LongDescription LongDescription,CategoryId CategoryId,CreatedBy CreatedBy from [Datamantra].[Course] where IsDeleted=0";
        public const string GetCourseById = "select CourseId CourseId,CourseName CourseName,Rating Rating,Price Price,DiscountedPrice DiscountedPrice,EnrolledCount EnrolledCount,Image Image,ShortDescription ShortDescription,LongDescription LongDescription,CategoryId CategoryId,CreatedBy CreatedBy from [Datamantra].[Course] where IsDeleted=0 and CourseId=@CourseId";
        public const string DeleteCourseById = "update [Datamantra].[Course] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where CourseId=@CourseId";
        public const string UpdateCourseById = "update [Datamantra].[Course] set CourseName=@CourseName,Rating=@Rating,Price=@Price,DiscountedPrice=@DiscountedPrice,EnrolledCount=@EnrolledCount,Image=@Image,ShortDescription=@ShortDescription,LongDescription=@LongDescription,CategoryId=@CategoryId,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate  where IsDeleted=0 and CourseId=@CourseId";
        public const string SaveCourse = "insert into [Datamantra].[Course] (CourseName,Rating,Price,DiscountedPrice,EnrolledCount,Image,ShortDescription,LongDescription,CategoryId,CreatedBy,CreatedDate) Values (@CourseName,@Rating,@Price,@DiscountedPrice,@EnrolledCount,@Image,@ShortDescription,@LongDescription,@CategoryId,@CreatedBy,@CreatedDate) SELECT CAST(scope_identity() AS int)";
        public const string GetAllCourseByCategoryId = "select CourseId CourseId,CourseName CourseName,Rating Rating,Price Price,DiscountedPrice DiscountedPrice,EnrolledCount EnrolledCount,Image Image,ShortDescription ShortDescription,LongDescription LongDescription,CategoryId CategoryId,CreatedBy CreatedBy from [Datamantra].[Course] where IsDeleted=0 and CategoryId=@CategoryId";
        public const string DeleteCourseByCategoryId = "update [Datamantra].[Course] set IsDeleted=1,ModifiedBy=@ModifiedBy ,ModifiedDate=@ModifiedDate where  CategoryId=@CategoryId";

        #region Review Queries
        public const string GetCourseReviewListByCourseId = "select u.UserName UserName,r.ReviewId  ReviewId,r.Rating Rating,r.UserId UserId,r.CourseId CourseId,r.Comments Comments,r.CreatedBy CreatedBy,r.CreatedDate CreatedDate,r.ModifiedBy ModifiedBy,r.ModifiedDate ModifiedDate from [Datamantra].[Review] r join [Datamantra].[User] u on r.UserId = u.UserId where CourseId=@CourseId and r.IsDeleted = 0 and u.IsDeleted=0 and u.IsActive=1;";
        public const string SaveCourseReview = "Insert into [Datamantra].[Review] (CourseId ,UserId,Comments,CreatedBy,CreatedDate,IsDeleted,Rating) values (@CourseId,@UserId,@Comments,@CreatedBy,@CreatedDate,0,@Rating) SELECT CAST(scope_identity() AS int)";
        public const string UpdateCourseReview = "update [Datamantra].[Review] set CourseId=@CourseId,UserId=@UserId,Rating=@Rating,Comments=@Comments,ModifiedDate=@ModifiedDate,ModifiedBy=@ModifiedBy where ReviewId=@ReviewId and IsDeleted = 0";
        public const string DeleteReviewbyReviewId = "update [Datamantra].[Review] set IsDeleted = 1,ModifiedDate=@ModifiedDate,ModifiedBy=@ModifiedBy where ReviewId=@ReviewId";
        #endregion
    }
}