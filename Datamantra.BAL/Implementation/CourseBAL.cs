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
    //public class UserBAL : DMBaseManager,  
    public class CourseBAL : ICourseBAL
    {
        private ICourseBAL _courseBAL;
        public CourseBAL(ICourseBAL courseBAL)
        {
            _courseBAL = courseBAL;
        }

         
    }
}
