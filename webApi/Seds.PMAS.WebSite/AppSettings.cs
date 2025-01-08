﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;

namespace Seds.PMAS.WebSite
{
    public class AppSettings
    {
        public static string TokenStorage
        {
            get
            {
                return Setting<string>("TokenStorage");
            }
        }


        public static string DataStoragePath
        {
            get
            {
                return Setting<string>("DataStoragePath");
            }
        }

        public static string ApplicationId
        {
            get
            {
                return Setting<string>("ApplicationId");
            }
        }


        public static string ApplicationSecret
        {
            get
            {
                return Setting<string>("ApplicationSecret");
            }
        }

        public static string LinnworksAPIUrl
        {
            get
            {
                return Setting<string>("PMASAPIUrl");
            }
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

    }
}