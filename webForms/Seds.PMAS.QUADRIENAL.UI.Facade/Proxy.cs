using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IdentityModel.Tokens;
using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace Seds.PMAS.QUADRIENAL.UI.Processos
{
    public class Proxy<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class
    {        
        private SecurityToken securityToken;

        protected string clientPassword;

        private bool disposed = false;

        public TChannel Service { get { return this.Channel; } }
        
        public Proxy()
        {
        }

        public Proxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {            
        }

        public Proxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Proxy(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public Proxy(Binding binding, EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
        {
        }        

        public Proxy(SecurityToken securityToken)
        {
            this.securityToken = securityToken;
        }

        public Proxy(SecurityToken securityToken, string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            this.securityToken = securityToken;
        }

        public Proxy(SecurityToken securityToken, string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            this.securityToken = securityToken;
        }

        public Proxy(SecurityToken securityToken, string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            this.securityToken = securityToken;
        }

        public Proxy(SecurityToken securityToken, Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            this.securityToken = securityToken;
        }

        protected override TChannel CreateChannel()
        {
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault();
            var login = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault();
            var role = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/role" && c.Value.Contains("PMAS 2017@")).FirstOrDefault();
            this.ClientCredentials.UserName.UserName = Genericos.clsCrypto.Encrypt(id.Value + ";" + login.Value + ";" + role.Value);
            this.ClientCredentials.UserName.Password = Genericos.clsCrypto.Encrypt(clientPassword);

            lock (this.ChannelFactory)
            {
                return this.ChannelFactory.CreateChannel();
            }
        }

        ~Proxy()
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.State == CommunicationState.Faulted)
                    {
                        Abort();
                    }
                    else
                    {
                        Close();
                    }
                }
            }

            this.disposed = true;
        }
    }
}
