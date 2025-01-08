using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Seds.Seguranca.Token;

namespace Seds.PMAS.QUADRIENAL.Servicos
{
    public static class Extensions
    {
        public static String GetExceptionMessage(Exception ex)
        {
            var msg = ex.Message;
            if (ex.InnerException != null)
                msg += "\n" + GetExceptionMessage(ex.InnerException);
            return msg;
        }     
    }
}
