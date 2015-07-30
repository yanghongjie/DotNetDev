using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Dev.Develop
{
    public static class HttpUtils
    {
        #region Private Fields

        private const string HttpContextBaseKey = "MS_HttpContext";

        #endregion Private Fields

        #region Public Methods

        public static void CopyRequestHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> source,
            HttpRequestHeaders destination)
        {
            foreach (var header in source)
            {
                switch (header.Key)
                {
                    case "User-Agent":
                        destination.Add(header.Key, "dev/1.0");
                        break;

                    case "Via":
                        break;

                    default:
                        destination.Add(header.Key, String.Join(",", header.Value));
                        break;
                }
            }
        }

        public static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            HttpContextBase contextBase = GetHttpContextBase(request);

            if (contextBase == null)
            {
                return null;
            }

            return ToHttpContext(contextBase);
        }

        public static string GetUserAgent(HttpRequestMessage request)
        {
            HttpContext context = GetHttpContext(request);
            return context == null ? "" : context.Request.UserAgent;
        }

        public static string GetUserHostAddress(HttpRequestMessage request)
        {
            HttpContext context = GetHttpContext(request);
            return context == null ? "" : context.Request.UserHostAddress;
        }

        public static string GetUserHostAddress(HttpContextBase contextBase)
        {
            HttpContext context = ToHttpContext(contextBase);
            return context == null ? "" : context.Request.UserHostAddress;
        }

        public static bool IsMobileDevice(HttpRequestMessage request)
        {
            HttpContext context = GetHttpContext(request);
            return context != null && context.Request.Browser.IsMobileDevice;
        }

        #endregion Public Methods

        #region Private Methods

        public static bool IsDev(HttpContextBase httpContext)
        {
            string ip = GetUserHostAddress(httpContext);
            return !String.IsNullOrEmpty(ip) && ip.StartsWith("10.1.10");
        }

        public static bool IsLocalhost(HttpContextBase httpContext)
        {
            return httpContext.Request.IsLocal;
        }

        public static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }

        public static bool IsIphone(HttpRequestMessage request)
        {
            HttpContext context = GetHttpContext(request);
            if (context != null)
            {
                string StrContext = context.Request.Browser.Browser.ToUpper();
                return StrContext.Contains("IPHONE") || StrContext.Contains("IPAD") || StrContext.Contains("IPOD");
            }
            return false;
        }

        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            object value;

            if (!request.Properties.TryGetValue(HttpContextBaseKey, out value))
            {
                return null;
            }

            return value as HttpContextBase;
        }

        #endregion Private Methods
    }
}