using System.Net;
using System.Net.Http;
using System.Web;
using Dev.Common.Develop;

namespace Dev.Common.Extensions
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
            CookieCollection collection = new CookieCollection();
            for (int j = 0; j < cookies.Count; j++)
            {
                HttpCookie cookie = cookies.Get(j);
                Cookie oC = new Cookie();

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
    }
}