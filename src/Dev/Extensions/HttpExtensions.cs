﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using Dev.Develop;

namespace Dev.Extensions
{
    public static class HttpExtensions
    {
        private const string HttpContextBaseKey = "MS_HttpContext";

        public static HttpContext ToHttpContext(this HttpRequestMessage request)
        {
            return HttpUtils.ToHttpContext(request.ToHttpContextBase());
        }

        private static HttpContextBase ToHttpContextBase(this HttpRequestMessage request)
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

        public static bool IsDev(this HttpContextBase httpContext)
        {
            return HttpUtils.IsDev(httpContext);
        }

        public static bool IsLocalhost(this HttpContextBase httpContext)
        {
            return HttpUtils.IsLocalhost(httpContext);
        }

        public static CookieCollection ToCookieCollection(this HttpCookieCollection cookies, string domain)
        {
            var collection = new CookieCollection();
            for (int j = 0; j < cookies.Count; j++)
            {
                HttpCookie cookie = cookies.Get(j);
                var oC = new Cookie();

                if (cookie != null)
                {
                    // Convert between the System.Net.Cookie to a System.Web.HttpCookie...
                    oC.Domain = domain;
                    oC.Expires = cookie.Expires;
                    oC.Name = cookie.Name;
                    oC.Path = cookie.Path;
                    oC.Secure = cookie.Secure;
                    oC.Value = cookie.Value;

                    collection.Add(oC);
                }
            }
            return collection;
        }

        public static string ToLogString(this HttpRequest request)
        {
            var writer = new StringWriter();

            WriteStartLine(request, writer);
            WriteHeaders(request, writer);
            WriteBody(request, writer);

            return writer.ToString();
        }

        private static void WriteStartLine(HttpRequest request, StringWriter writer)
        {
            const string SPACE = " ";

            writer.Write(request.HttpMethod);
            writer.Write(SPACE + request.Url);
            writer.WriteLine(SPACE + request.ServerVariables["SERVER_PROTOCOL"]);
        }

        private static void WriteHeaders(HttpRequest request, StringWriter writer)
        {
            foreach (string key in request.Headers.AllKeys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, request.Headers[key]));
            }
            writer.WriteLine();
        }

        private static void WriteBody(HttpRequest request, StringWriter writer)
        {
            var reader = new StreamReader(request.InputStream);
            try
            {
                string body = reader.ReadToEnd();
                writer.WriteLine(body);
            }
            finally
            {
                reader.BaseStream.Position = 0;
            }
        }
    }
}