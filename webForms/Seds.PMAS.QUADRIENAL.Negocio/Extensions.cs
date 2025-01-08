using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Seds.PMAS.QUADRIENAL.Negocio
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

        public static string Concat(this IList list, string separator)
        {
            var s = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                s.Append(item);
                if (i < list.Count - 1)
                    s.Append(separator);
            }
            return s.ToString();
        }

        public static string Concat(this IList list)
        {
            var s = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                s.Append(item);
                if (i < list.Count - 2)
                    s.Append(", ");
                else if (i < list.Count - 1)
                    s.Append(" e ");
            }
            return s.ToString();
        }
    }
}
