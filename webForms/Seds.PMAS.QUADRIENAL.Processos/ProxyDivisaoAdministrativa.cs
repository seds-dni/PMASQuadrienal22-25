using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seds.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class ProxyDivisaoAdministrativa : IDisposable
    {
        public Seds.WebApiClient.DivisaoAdministrativaClient Service { get; set; }
        public ProxyDivisaoAdministrativa()
        {
            Service = new Seds.WebApiClient.DivisaoAdministrativaClient();
        }

        ~ProxyDivisaoAdministrativa()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Service = null;
        }

        public static List<DradsInfo> Drads
        {
            get
            {
                var lst = (List<DradsInfo>)HttpContext.Current.Cache["Drads"];
                if (lst == null)
                {
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lst = proxy.Service.GetDrads().OrderBy(c => c.Nome).ToList();
                        HttpContext.Current.Cache["Drads"] = lst;
                    }
                }
                return lst;
            }
        }

        public static List<MunicipioInfo> MunicipiosEstaduais
        {
            get
            {
                var lst = (List<MunicipioInfo>)HttpContext.Current.Cache["MunicipiosEstaduais"];
                if (lst == null)
                {
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lst = proxy.Service.GetMunicipiosByUF(26).ToList();
                        HttpContext.Current.Cache["MunicipiosEstaduais"] = lst;
                    }
                }
                return lst;
            }
        }
    }
}
