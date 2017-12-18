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
    public class CourseDAL : ICourseDAL
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
    }
}
