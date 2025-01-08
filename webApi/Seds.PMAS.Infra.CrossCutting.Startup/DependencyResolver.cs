using Microsoft.Practices.Unity;
using Seds.PMAS.Aplicacao;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Dominio.Services;
using Seds.PMAS.Infra.Data.Context;
using Seds.PMAS.Infra.Data.Repositories;

namespace Seds.PMAS.Infra.CrossCutting.Startup
{
    public class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {

            container.RegisterType<DBPMASContext, DBPMASContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuarioRepository, UsuarioRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuarioService, UsuarioAppService>(new HierarchicalLifetimeManager());

            //container.RegisterType<IRecursoRepository, RecursoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRecursoService, RecursoService>(new HierarchicalLifetimeManager());

            container.RegisterType<IPrefeitoRepository, PrefeitoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPrefeitoService, PrefeitoService>(new HierarchicalLifetimeManager());
            

            container.RegisterType<RecursoEntity, RecursoEntity>(new HierarchicalLifetimeManager());
            container.RegisterType<UsuarioEntity, UsuarioEntity>(new HierarchicalLifetimeManager());
            container.RegisterType<StatusEntity, StatusEntity>(new HierarchicalLifetimeManager());
            container.RegisterType<PrefeitoEntity, PrefeitoEntity>(new HierarchicalLifetimeManager());
            container.RegisterType<PrefeituraEntity, PrefeituraEntity>(new HierarchicalLifetimeManager());
            container.RegisterType<SituacaoEntity, SituacaoEntity>(new HierarchicalLifetimeManager());




        }
    }
}
