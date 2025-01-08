using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace Seds.PMAS.Web
{
    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        //EntSaiba que SessionControllerHandler apenas para conectar nosso SessionControllerHandler recém-criado ao fluxo de trabalho de SessionControllerHandler .
        //Para isso precisamos implementar o módulo que herdará a IRouteHandler IRouteHandler e no método GetHttpHandler apenas GetHttpHandler a nova instância do manipulador do controlador de sessão.


        public SessionableControllerHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }
}