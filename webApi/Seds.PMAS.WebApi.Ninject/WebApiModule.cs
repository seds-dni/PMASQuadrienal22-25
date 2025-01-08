using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.Filter;
using Ninject.Web.WebApi.Validation;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Http.Validation; 

namespace Seds.PMAS.WebApi.Ninject
{
    /// <summary>
    /// Define as ligações e os plugins da extensão Web Api.
    /// </summary>
    public class WebApiModule : NinjectModule
    {

        public override void Load()
        {

            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectWebApiHttpApplicationPlugin>();
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, DefaultWebApiRequestScopeProvider>();

            this.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();

            this.Bind<IFilterProvider>().To<DefaultFilterProvider>();
            this.Bind<IFilterProvider>().To<NinjectFilterProvider>();

            this.Bind<ModelValidatorProvider>().To<NinjectDefaultModelValidatorProvider>();
            this.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();
        }
    }
}
