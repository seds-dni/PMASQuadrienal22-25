using Microsoft.IdentityModel.Protocols.WSFederation;
using System;
using System.Web;
using System.Web.Util;

namespace Seds.PMAS.Web
{
    public class FederatedMVCRequestValidator : RequestValidator
    {

        protected override bool IsValidRequestString(HttpContext context, string value,

            RequestValidationSource requestValidationSource,

            string collectionKey, out int validationFailureIndex)
        {

            validationFailureIndex = 0;



            if (requestValidationSource == RequestValidationSource.Form &&

                collectionKey.Equals(WSFederationConstants.Parameters.Result, StringComparison.Ordinal))
            {

                var unvalidatedFormValues =

                   System.Web.Helpers.Validation.Unvalidated(context.Request).Form;



                SignInResponseMessage message =

                    WSFederationMessage.CreateFromNameValueCollection(

                       WSFederationMessage.GetBaseUrl(context.Request.Url), unvalidatedFormValues)

                          as SignInResponseMessage;

                if (message != null)
                {

                    return true;

                }

            }



            return base.IsValidRequestString(context, value, requestValidationSource,

               collectionKey, out validationFailureIndex);

        }

    }
}