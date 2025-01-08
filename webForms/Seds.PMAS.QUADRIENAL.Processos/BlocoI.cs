using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS2013.Entidades;
using Seds.PMAS2013.Processos.PrefeituraService;

namespace Seds.PMAS2013.Processos
{
    public class BlocoI : IDisposable
    {
        public ProxyPrefeitura ProxyPrefeitura { get; set; }
        public ProxyDivisaoAdministrativa ProxyDivisaoAdministrativa { get; set; }

        public PrefeituraInfo GetPrefeitura(Int32 id)
        {
            CreateProxyPrefeitura();

            var pre = ProxyPrefeitura.Service.GetPrefeituraById(id);
            if(pre == null)
                return null;

            CreateProxyDivisaoAdministrativa();
                        
            var mun = ProxyDivisaoAdministrativa.Service.GetMunicipioById(pre.IdMunicipio);
            pre.Municipio = mun;
            //pre.Municipio.Drads = ProxyDivisaoAdministrativa.Service.GetDradsById(mun.IdDrads.Value);
            return pre;
        }

        void CreateProxyPrefeitura()
        {
            if (ProxyPrefeitura == null || ProxyPrefeitura.State == System.ServiceModel.CommunicationState.Closed)            
                ProxyPrefeitura = new ProxyPrefeitura();                
        }

        void CreateProxyDivisaoAdministrativa()
        {
            if (ProxyDivisaoAdministrativa == null || ProxyDivisaoAdministrativa.State == System.ServiceModel.CommunicationState.Closed)
                ProxyDivisaoAdministrativa = new ProxyDivisaoAdministrativa();
        }

        public void UpdatePrefeitura(PrefeituraInfo pre)
        {
            CreateProxyPrefeitura();
            ProxyPrefeitura.Service.UpdatePrefeitura(pre);
        }

        public void Dispose()
        {
            if (ProxyPrefeitura != null)
                ProxyPrefeitura.Dispose();
            if (ProxyDivisaoAdministrativa != null)
                ProxyPrefeitura.Dispose();
        }
    }
}
