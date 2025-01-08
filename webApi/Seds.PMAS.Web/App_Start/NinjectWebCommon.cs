[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Seds.PMAS.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Seds.PMAS.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Seds.PMAS.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Seds.PMAS.Aplicacao.Interface;
    using Seds.PMAS.Dominio.Interfaces.Services;
    using Seds.PMAS.Dominio.Interfaces.Repositories;
    using Seds.PMAS.Infra.Data.Repositories;
    using Seds.PMAS.Dominio.Services;
    using Seds.PMAS.Aplicacao;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //    //kernel.Bind(typeof(IAppServicoBase<>)).To(typeof(AppServiceBase<>));
            //    kernel.Bind<IRecursoAppService>().To<RecursoAppService>();
            //    kernel.Bind<IPrefeitoAppService>().To<PrefeitoAppService>();
            //    kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();

            //    kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            //    kernel.Bind<IRecursoServico>().To<RecursoServico>();
            //    kernel.Bind<IPrefeitoService>().To<PrefeitoService>();
            //    kernel.Bind<IUsuarioService>().To<UsuarioService>();

            //    kernel.Bind(typeof(IEntityBaseRepository<>)).To(typeof(EntityBaseRepository<>));
            //    kernel.Bind<IRecursoRepository>().To<RecursoRepository>();
            //    kernel.Bind<IPrefeitoRepository>().To<PrefeitoRepository>();
            //    kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();


        }
    }
}
