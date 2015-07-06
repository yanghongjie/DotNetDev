using System;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.Data;

namespace Dev.Data.TransientErrorDetectionStrategy
{
    /// <summary>
    ///     Provides the transient error detection logic for transient faults that are specific to SQL Database.
    /// </summary>
    public sealed class SqlTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        #region ITransientErrorDetectionStrategy Members

        /// <summary>
        ///     Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>
        ///     true if the specified exception is considered as transient; otherwise, false.
        /// </returns>
        public bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                SqlException sqlException;
                if ((sqlException = ex as SqlException) != null)
                {
                    foreach (SqlError error in sqlException.Errors)
                    {
                        switch (error.Number)
                        {
                            case 40501:
                                ThrottlingCondition throttlingCondition = ThrottlingCondition.FromError(error);
                                sqlException.Data[throttlingCondition.ThrottlingMode.GetType().Name] = ((object)throttlingCondition.ThrottlingMode).ToString();
                                sqlException.Data[throttlingCondition.GetType().Name] = throttlingCondition;
                                return true;

                            case 40540:
                            case 40613:
                            case 10928:
                            case 10929:
                            case 40143:
                            case 40197:
                            case 233:
                            case 10053:
                            case 10054:
                            case 10060:
                            case 20:
                            case 64:
                                return true;

                            default:
                                continue;
                        }
                    }
                }
                else
                {
                    if (ex is TimeoutException)
                        return true;
                    EntityException entityException;
                    if ((entityException = ex as EntityException) != null)
                        return IsTransient(entityException.InnerException);
                }
            }
            return false;
        }

        #endregion ITransientErrorDetectionStrategy Members

        #region Nested type: ProcessNetLibErrorCode

        /// <summary>
        ///     Error codes reported by the DBNETLIB module.
        /// </summary>
        private enum ProcessNetLibErrorCode
        {
            ZeroBytes = -3,
            Timeout = -2,
            Unknown = -1,
            InsufficientMemory = 1,
            AccessDenied = 2,
            ConnectionBusy = 3,
            ConnectionBroken = 4,
            ConnectionLimit = 5,
            ServerNotFound = 6,
            NetworkNotFound = 7,
            InsufficientResources = 8,
            NetworkBusy = 9,
            NetworkAccessDenied = 10,
            GeneralError = 11,
            IncorrectMode = 12,
            NameNotFound = 13,
            InvalidConnection = 14,
            ReadWriteError = 15,
            TooManyHandles = 16,
            ServerError = 17,
            SSLError = 18,
            EncryptionError = 19,
            EncryptionNotSupported = 20,
        }

        #endregion Nested type: ProcessNetLibErrorCode
    }
}