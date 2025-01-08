
using Ninject.Syntax;
using Ninject.Web.WebApi;
using System.Web.Http.Dependencies;
namespace Seds.PMAS.WebApi.Ninject
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <Ninject.Web.WebApi.NinjectDependencyResolver/>.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectDependencyResolver(IResolutionRoot resolutionRoot)
            : base(resolutionRoot)
        {
        }

        /// <summary>
        /// Inicia o Scope
        /// </summary>
        /// <returns>Novo scope</returns>
        public virtual IDependencyScope BeginScope()
        {
            return this;
        }
    }
}
