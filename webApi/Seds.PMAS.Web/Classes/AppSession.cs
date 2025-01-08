using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Seds.PMAS.Web.Classes
{
    public class AppSession
    {
        public string PMASSessionToken = "";
        public string Token = "";
        public Guid PMASUserId = Guid.Empty;
        public string Server = "";
        public string Localizacao = "";

        public static AppSession LoadAppSession(string token)
        {

            string fileName = System.IO.Path.Combine(AppSettings.TokenStorage, token);
            string fileContent = "";

            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.StreamReader r = new System.IO.StreamReader(fileName))
                {
                    fileContent = r.ReadToEnd();
                    r.Close();
                }
                return JsonConvert.DeserializeObject<AppSession>(fileContent);
            }
            else
            {
                AppSession newSession = new AppSession()
                {
                    Token = token
                };
                newSession.AuthSession();
                AppSession.SaveAppSession(token, newSession);
                return newSession;
            }
        }

        public static void SaveAppSession(string token, AppSession appsession)
        {
            string fileName = System.IO.Path.Combine(AppSettings.TokenStorage, token);

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, appsession);
            }
        }


        public void AuthSession()
        {
            PMASSession currentSession = null;
            //first check if the session is active
            if (this.PMASSessionToken != "" && this.PMASSessionToken != null)
            {
                try
                {
                    string output = GetAPIData(AppSettings.LinnworksAPIUrl + "/Auth/GetSession", "token=" + this.PMASSessionToken, this.PMASSessionToken);
                    currentSession = JsonConvert.DeserializeObject<PMASSession>(output);
                }
                catch (Exception ex)
                {

                }
            }
            if (currentSession == null)
            {
                try
                {
                    string myParameters = string.Format("applicationId={0}&applicationSecret={1}&token={2}", AppSettings.ApplicationId, AppSettings.ApplicationSecret, this.Token);
                    string output = GetAPIData(AppSettings.LinnworksAPIUrl + "/Auth/AuthorizeByApplication", myParameters, null);
                    currentSession = JsonConvert.DeserializeObject<PMASSession>(output);
                }
                catch (WebException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            this.Localizacao = currentSession.Localizacao;
            this.Server = currentSession.Server;
            this.PMASUserId = new Guid(currentSession.Id);
            this.PMASSessionToken = currentSession.Token;
        }

        static string GetAPIData(string URL, string data, string sessionId)
        {
            string HtmlResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.Accept] = "application/json, text/javascript, */*; q=0.01";
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded; charset=UTF-8";
                if (sessionId != null)
                {
                    wc.Headers[HttpRequestHeader.Authorization] = sessionId;
                }
                HtmlResult = wc.UploadString(URL, data);
            }
            return HtmlResult;
        }

    }
}