using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Seds.PMAS.QUADRIENAL.UI.Web.Helper
{
    public static class WebHelper
    {
        /// <summary>
        /// Recursively searches for a server control with the given ID.
        /// </summary>
        /// <param name="id">ID of control to find</param>
        /// <returns>The matching control or null if no match was found</returns>
        public static Control FindControlRecursive(this Control control, string id)
        {
            foreach (Control ctl in control.Controls)
            {
                if (ctl.ID == id)
                    return ctl;

                Control child = FindControlRecursive(ctl, id);
                if (child != null)
                    return child;
            }
            return null;
        }
    }
}