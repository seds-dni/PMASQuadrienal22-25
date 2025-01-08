
namespace Seds.PMAS.WebApi.WebHost
{
    using System;
    using System.Web.Http;
    using Ninject.Modules;

    /// <summary>
    /// Define as ligações da extensão WebApi WebHost.
    /// </summary>
    public class WebApiWebHostModule : NinjectModule
    {
        /// <summary>
        /// Carrega o módulo no kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<HttpConfiguration>().ToMethod(ctx => GlobalConfiguration.Configuration);
        }

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public override void VerifyRequiredModulesAreLoaded()
        {
            if (!this.Kernel.HasModule(typeof(WebApiModule).FullName))
            {
                throw new InvalidOperationException("This module requires Ninject.Web.WebAPI extension");
            }
        }
    }
}
