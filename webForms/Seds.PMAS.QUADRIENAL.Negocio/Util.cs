using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public static class Util
    {
        public static bool ValidaString(string valorString)
        {
            try
            {
                if (valorString != null)
                    valorString = valorString.Trim();
                bool retorno = !string.IsNullOrEmpty(valorString);

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidaCPF(string strCpf)
        {
            try
            {
                int d1, d2;
                int digito1, digito2, resto;
                int digitoCPF;
                string nDigResult;

                if (strCpf.Length != 11 ||
                    Convert.ToInt64(strCpf) == 0)
                    return false;

                d1 = d2 = 0;
                digito1 = digito2 = resto = 0;

                for (int nCount = 1; nCount < strCpf.Length - 1; nCount++)
                {
                    digitoCPF = Convert.ToInt32(strCpf.Substring(nCount - 1, 1));

                    //multiplique a ultima casa por 2 a seguinte por 3 a seguinte por 4 e assim por diante.
                    d1 = d1 + (11 - nCount) * digitoCPF;

                    //para o segundo digito repita o procedimento incluindo o primeiro digito calculado no passo anterior.
                    d2 = d2 + (12 - nCount) * digitoCPF;
                };

                //Primeiro resto da divisão por 11.
                resto = (d1 % 11);

                //Se o resultado for 0 ou 1 o digito é 0 caso contrário o digito é 11 menos o resultado anterior.
                if (resto < 2)
                    digito1 = 0;
                else
                    digito1 = 11 - resto;

                d2 += 2 * digito1;

                //Segundo resto da divisão por 11.
                resto = (d2 % 11);

                //Se o resultado for 0 ou 1 o digito é 0 caso contrário o digito é 11 menos o resultado anterior.
                if (resto < 2)
                    digito2 = 0;
                else
                    digito2 = 11 - resto;

                //Digito verificador do CPF que está sendo validado.
                String nDigVerific = strCpf.Substring(strCpf.Length - 2, 2);

                //Concatenando o primeiro resto com o segundo.
                nDigResult = digito1.ToString() + digito2.ToString();

                //comparar o digito verificador do cpf com o primeiro resto + o segundo resto.
                return nDigVerific.Equals(nDigResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidarEmail(string _email)
        {
            try
            {
                string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                return Regex.IsMatch(_email, pattern);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool ValidaCNPJ(string strCnpj)
        {
            try
            {
                int soma = 0, dig;

                if (strCnpj.Length != 14)
                    return false;

                string cnpj_calc = strCnpj.Substring(0, 12);

                char[] chr_cnpj = strCnpj.ToCharArray();

                /* Primeira parte */
                for (int i = 0; i < 4; i++)
                    if (chr_cnpj[i] - 48 >= 0 && chr_cnpj[i] - 48 <= 9)
                        soma += (chr_cnpj[i] - 48) * (6 - (i + 1));
                for (int i = 0; i < 8; i++)
                    if (chr_cnpj[i + 4] - 48 >= 0 && chr_cnpj[i + 4] - 48 <= 9)
                        soma += (chr_cnpj[i + 4] - 48) * (10 - (i + 1));
                dig = 11 - (soma % 11);

                cnpj_calc += (dig == 10 || dig == 11) ?
                    "0" : dig.ToString();

                /* Segunda parte */
                soma = 0;
                for (int i = 0; i < 5; i++)
                    if (chr_cnpj[i] - 48 >= 0 && chr_cnpj[i] - 48 <= 9)
                        soma += (chr_cnpj[i] - 48) * (7 - (i + 1));
                for (int i = 0; i < 8; i++)
                    if (chr_cnpj[i + 5] - 48 >= 0 && chr_cnpj[i + 5] - 48 <= 9)
                        soma += (chr_cnpj[i + 5] - 48) * (10 - (i + 1));
                dig = 11 - (soma % 11);
                cnpj_calc += (dig == 10 || dig == 11) ?
                    "0" : dig.ToString();

                return strCnpj.Equals(cnpj_calc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Concat(this IList list, string separator)
        {
            var s = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                s.Append(item);
                if(i < list.Count - 1)
                    s.Append(separator);
            }
            return s.ToString();
        }

        public static SecurityToken GetDelegatedToken(SecurityToken callerToken, String appliesToUrl)
        {
            SecurityTokenHandlerCollectionManager securityTokenManager = new SecurityTokenHandlerCollectionManager(string.Empty);
            securityTokenManager["ActAs"] = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
            securityTokenManager[string.Empty] = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();

            X509Certificate2 clientCertificate = GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=SEDSToken");

            var trustChannelFactory = new WSTrustChannelFactory("WS2007HttpBinding_IWSTrust13Sync");

            trustChannelFactory.TrustVersion = TrustVersion.WSTrust13;
            trustChannelFactory.Credentials.SupportInteractive = false;
            trustChannelFactory.SecurityTokenHandlerCollectionManager = securityTokenManager;
            trustChannelFactory.Credentials.ClientCertificate.Certificate = clientCertificate;

            trustChannelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
            trustChannelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            try
            {
                // Creating the SecurityToken request
                RequestSecurityToken rst = new RequestSecurityToken(WSTrust13Constants.RequestTypes.Issue);

                // Inject the Session SecurityToken in the ActAs property of the request.
                // Then the ActAs service will use it in order to obtain the appropiate Claims of the original caller
                //rst.ActAs = new SecurityTokenElement(callerToken);
                rst.ActAs = new SecurityTokenElement(callerToken);
                rst.AppliesTo = new EndpointAddress(new Uri(appliesToUrl));

                WSTrustChannel channel = (WSTrustChannel)trustChannelFactory.CreateChannel();
                SecurityToken delegatedToken = channel.Issue(rst);

                return delegatedToken;
            }
            finally
            {
                trustChannelFactory.Close();
            }
        }

        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            X509Store store = new X509Store(name, location);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);

            try
            {
                X509Certificate2 result = null;

                // Every time we call store.Certificates property, a new collection will be returned.
                certificates = store.Certificates;

                for (int i = 0; i < certificates.Count; i++)
                {
                    X509Certificate2 cert = certificates[i];

                    if (cert.SubjectName.Name.ToLower(CultureInfo.CurrentCulture) == subjectName.ToLower(CultureInfo.CurrentCulture))
                    {
                        if (result != null)
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "There is more than one certificate found for subject Name {0}", subjectName));
                        }

                        result = new X509Certificate2(cert);
                    }
                }

                if (result == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "No certificate was found for subject Name {0}", subjectName));
                }

                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    for (int i = 0; i < certificates.Count; i++)
                    {
                        X509Certificate2 cert = certificates[i];
                        cert.Reset();
                    }
                }

                store.Close();
            }
        }
        public static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

    }
}
