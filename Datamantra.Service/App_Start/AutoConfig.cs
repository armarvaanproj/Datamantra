using Autofac;
using Autofac.Integration.WebApi;
using Datamantra.BAL.Implementation;
using Datamantra.BAL.Interface;
using Datamantra.DAL.Implementation;
using Datamantra.DAL.Interface;
using Datamantra.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Datamantra.Service.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AdminBAL>().As<IAdminBAL>();
            builder.RegisterType<AdminDAL>().As<IAdminDAL>();


            builder.RegisterType<CourseBAL>().As<ICourseBAL>();
            builder.RegisterType<CourseDAL>().As<ICourseDAL>();

            builder.RegisterType<UserBAL>().As<IUserBAL>();
            builder.RegisterType<UserDAL>().As<IUserDAL>();

            //builder.RegisterType<IAdminBAL>().As<IDynamicQuery>().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("BAL")).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("DAL")).AsImplementedInterfaces();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var resolver = new AutofacWebApiDependencyResolver(builder.Build());
            config.DependencyResolver = resolver;
        }

    }
}