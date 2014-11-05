using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// methods for accessing any type of token cache.
    /// </summary>
    public interface ITokenCache
    {
        /// <summary>
        /// Get a client auth token from some kind of storage.
        /// </summary>
        /// <returns>client auth token</returns>
        OAuthToken LoadToken();

        /// <summary>
        /// Store a client auth token in some kind of storage.
        /// </summary>
        /// <param name="token">client auth token</param>
        void SaveToken(OAuthToken token);
    }
}
