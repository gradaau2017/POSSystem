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

            kernel.Bind<MoencoPos.Sales.Services.ISalesInvoiceService>().To<MoencoPos.Sales.Services.SalesInvoiceService>();

            kernel.Bind<MoencoPos.Sales.Services.ICustomerService>().To<MoencoPos.Sales.Services.CustomerService>();

            kernel.Bind<MoencoPos.Product.Services.ICategoryService>().To<MoencoPos.Product.Services.CategoryService>();

            kernel.Bind<MoencoPos.Product.Services.IBranchService>().To<MoencoPos.Product.Services.BranchService>();

            kernel.Bind<MoencoPos.Product.Services.IProductService>().To<MoencoPos.Product.Services.ProductService>();            

            kernel.Bind<MoencoPOS.DAL.UnitOfWork.IUnitOfWork>().To<MoencoPOS.DAL.UnitOfWork.UnitOfWork>();
        }
    }
}