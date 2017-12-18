namespace Datamantra.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class SessionItemKey
    {
        public const string EmailAddress = "EmailAddress";
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string RoleID = "RoleID";
        public const string CookieSession = "CookieSession";
    }

    public class SessionObjects
    {
        //Every Property must have string data type
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessId { get; set; }
        public string ContactName { get; set; }
        public string UserTypeId { get; set; }
       
    }
}
