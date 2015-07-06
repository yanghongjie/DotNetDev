using System;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Dev.Data.TransientErrorDetectionStrategy
{
    /// <summary>
    /// Implements a strategy that ignores any transient errors.
    /// </summary>
    public sealed class TransientErrorIgnoreStrategy : ITransientErrorDetectionStrategy
    {
        #region ITransientErrorDetectionStrategy Members

        /// <summary>
        /// Always returns false.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>Always false.</returns>
        public bool IsTransient(Exception ex)
        {
            return false;
        }

        #endregion ITransientErrorDetectionStrategy Members
    }
}