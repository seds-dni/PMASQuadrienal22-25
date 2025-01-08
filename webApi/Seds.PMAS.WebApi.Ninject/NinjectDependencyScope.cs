using Ninject.Infrastructure.Disposal;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Linq;

namespace Seds.PMAS.WebApi.Ninject
{
    /// <summary>
    /// Dependency Scope implementation for ninject
    /// </summary>
    public class NinjectDependencyScope : DisposableObject, IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the classe <NinjectDependencyScop> .
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            this.ResolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// Obtém o resolution root.
        /// </summary>
        /// <value>The resolution root.</value>
        protected IResolutionRoot ResolutionRoot
        {
            get;
            private set;
        }

        /// <summary>
        /// Obtém os serviços do tipo especificio.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The service instance or <see langword="null"/> if none is configured.</returns>
        public object GetService(Type serviceType)
        {
            var request = this.ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        /// <summary>
        /// Gets the services of the specifies type.
        /// </summary>
        /// <param name="serviceType">O Tipo de serviço.</param>
        /// <returns>Todas as instâncias de serviço ou um vazio enumerable se nenhum estiver configurado.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.ResolutionRoot.GetAll(serviceType).ToList();
        }
    }
}
