using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ChurchServiceApi.Resources
{
    public class AppSettings
    {
        public static string JwtIssuer { get { return GetStringValue("JwtIssuer", "https://localhost:44353/"); } }
        public static string JwtAudience { get { return GetStringValue("JwtAudience", "http://localhost:4200/"); } }
        public static string JwtBase64Secret { get { return GetStringValue("JwtBase64Secret", "aKrej2Mdl541lkm9kAk52zUj32le4lkv98Tldsn_k43"); } }
        public static string siteUrl { get { return GetStringValue("siteUrl", "localhost:4200"); } }
        public static int EmailServerPort { get { return GetIntValue("EmailServerPort", 25); } }
        public static bool EmailServerUseSsl { get { return GetBooleanValue("EmailServerUseSsl", false); } }
        public static string EmailUserName { get { return GetStringValue("EmailUserName", null); } }
        public static string EmailPassword { get { return GetStringValue("EmailPassword", null); } }
        public static string EmailSendAs { get { return GetStringValue("EmailSendAs", "rjmealin@gmail.com"); } }

        public static double EmailVerificationExpiration { get { return GetDoubleValue("EmailVerificationExpiration", 0.25); } }

        private static string GetStringValue(string key, string defaultValue = "")
        {
            return System.Configuration.ConfigurationManager.AppSettings[key] == null ? defaultValue : System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }


        private static string[] GetStringArrayValue(string key)
        {
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[key]))
            {
                return System.Configuration.ConfigurationManager.AppSettings[key].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                return new string[] { };
        }


        private static double GetDoubleValue(string key, double defaultValue = 0)
        {
            double output = defaultValue;

            if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
                double.TryParse(System.Configuration.ConfigurationManager.AppSettings[key].ToString(), out output);

            return output;
        }


        private static int GetIntValue(string key, int defaultValue = 0)
        {
            int output = defaultValue;

            if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings[key].ToString(), out output);

            return output;
        }


        private static bool GetBooleanValue(string key, bool defaultValue = false)
        {
            if (bool.TryParse(System.Configuration.ConfigurationManager.AppSettings[key], out bool output))
                return output;
            else
                return defaultValue;
        }
    }
}
