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
    public class CourseController : ApiController
    {
         #region Declaration
        private IAdminBAL _AdminBAL;
        
        public CourseController(IAdminBAL courseBAL)
        {
            _AdminBAL = courseBAL;
        }
        #endregion

      
       
    }
}
