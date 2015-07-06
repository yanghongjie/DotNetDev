using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Dev.Data.TransientErrorDetectionStrategy
{
    public class HttpTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        private readonly List<HttpStatusCode> statusCodes =
            new List<HttpStatusCode>
            {
                HttpStatusCode.GatewayTimeout,
                HttpStatusCode.RequestTimeout,
                HttpStatusCode.ServiceUnavailable,
            };

        #region ITransientErrorDetectionStrategy Members

        /// <summary>
        /// Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>
        /// true if the specified exception is considered as transient; otherwise, false.
        /// </returns>
        public bool IsTransient(Exception ex)
        {
            WebException we = ex as WebException;
            if (we == null)
                return false;

            HttpWebResponse response = we.Response as HttpWebResponse;

            bool isTransient = response != null && statusCodes.Contains(response.StatusCode);
            return isTransient;
        }

        #endregion ITransientErrorDetectionStrategy Members
    }
}