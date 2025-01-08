using Ninject.Activation;
using Ninject.Components;

namespace Seds.PMAS.WebApi.Ninject
{
    /// <summary>
    /// O provedor para o escopo da solicitação.
    /// </summary>
    public class IWebApiRequestScopeProvider : INinjectComponent
    {
        /// <summary>
        /// Obtém o escopo de solicitação para o contexto de ativação atual.
        /// </summary>
        /// <param name="context">O Contexto</param>
        /// <returns>O escopo da solicitação</returns>
        object GetRequestScope(IContext context);
    }
}
