﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;

namespace Seds.PMAS.Web
{

    /// <summary>
    /// Summary description for TrustedIssuerNameRegistry
    /// </summary>

    public class TrustedIssuerNameRegistry : Microsoft.IdentityModel.Tokens.IssuerNameRegistry
    {
        /// <summary>
        /// Gets the issuer name of the given security token,
        /// if it is the X509SecurityToken of 'localhost'.
        /// </summary>
        /// <param name="securityToken">The issuer's security token</param>
        /// <returns>A string that represents the issuer name</returns>
        /// <exception cref="SecurityTokenException">If the issuer is not trusted.</exception>
        public override string GetIssuerName(SecurityToken securityToken)
        {
            X509SecurityToken x509Token = securityToken as X509SecurityToken;
            if (x509Token != null)
            {
                if (String.Equals(x509Token.Certificate.SubjectName.Name, "CN=SEDSToken"))
                {
                    return x509Token.Certificate.SubjectName.Name;
                }
            }

            throw new SecurityTokenException("Untrusted issuer.");
        }
    }
}