using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using log4net;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using MoencoPOS.DAL.Repository;


namespace MoencoPOS.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<IAzManStorage>().To<SqlAzManStorage>().WithConstructorArgument("connectionString", System.Configuration.ConfigurationManager.
                                                                                       ConnectionStrings["SecurityContext"].ConnectionString);
            kernel.Bind<NetSqlAzManRoleProvider>().To<NetSqlAzManRoleProvider>();

            kernel.Bind<MoencoPOS.Services.ISalesInvoiceService>().To<MoencoPOS.Services.SalesInvoiceService>();

            kernel.Bind<MoencoPOS.DAL.UnitOfWork.IUnitOfWork>().To<MoencoPOS.DAL.UnitOfWork.UnitOfWork>();
        }
    }
}