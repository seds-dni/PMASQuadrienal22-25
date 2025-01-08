using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using Seds.PMAS.Aplicacao;
using Seds.PMAS.Aplicacao.Interface;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Dominio.Services;
using Seds.PMAS.Infra.Data.Repositories;
using SimpleInjector;

namespace Seds.PMAS.Infra.CrossCutting.IoC
{
    public class SimpleInjectorBootStrapper
    {
        /// <summary>
        /// Install-package SimpleInject
        /// Install-package CommonServiceLocator
        /// Install-Package CommonServiceLocator.SimpleInjectorAdapter -Version 2.6.0
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterServices(Container container)
        {
            container.Register(typeof(IAppServiceBase<>), typeof(AppServiceBase<>), Lifestyle.Scoped);
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>), Lifestyle.Scoped);
            container.Register(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>), Lifestyle.Scoped);
            container.Register<IRecursoRepository, RecursoRepository>();
            container.Register<IPrefeitoService, PrefeitoService>();
            container.Register<IPrefeituraService, PrefeituraService>();
            container.Register<IUsuarioService, UsuarioService>();

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(container));
        }
    }
}
