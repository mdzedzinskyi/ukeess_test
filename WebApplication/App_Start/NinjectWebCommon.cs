using System;
using System.Web;
using BLL;
using BLL.DataAccess;
using BLL.UnitOfWork;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Models.DataContexts;
using Ninject;
using Ninject.Web.Common;
using WebActivatorEx;
using WebApplication;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace WebApplication
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAppContext>().To<MyAppContext>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserRepositoryProxy>().To<UserRepositoryProxy>();
            kernel.Bind<AppContextCreator>().To<ConcreateAppContextCreator>();
        }
    }
}