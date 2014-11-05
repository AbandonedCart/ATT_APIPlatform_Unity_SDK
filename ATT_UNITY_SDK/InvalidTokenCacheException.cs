using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// An exception representing any failure in an implementation
    /// of ITokenCache.
    /// </summary>
    public class InvalidTokenCacheException : Exception
    {
        /// <summary>
        /// Construct an exception indicating a token cache error.
        /// </summary>
        /// <param name="message">exception details</param>
        public InvalidTokenCacheException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Construct an exception indicating a token cache error.
        /// </summary>
        /// <param name="message">exception details</param>
        /// <param name="ex">inner exception</param>
        public InvalidTokenCacheException(string message, Exception ex) 
            : base(message, ex)
        {
        }
    }
}
