using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {
        /// <summary>
        /// Get New client credential and save it in tokefile.
        /// </summary>
        /// <returns>OAuthToken.</returns>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <exception cref="ArgumentException">Thrown tokenFilePath is invalid.</exception>
        [Obsolete("Please pass a tokenFilePath to your RequestFactory constructor instead of using this method.")]
        public OAuthToken GetNewClientCredential(String tokenFilePath)
        {
            Debug.WriteLine("[Obsolete] GetNewClientCredential(String tokenFilePath) called");

            OAuthToken token = GetNewClientCredential();
            if (string.IsNullOrEmpty(tokenFilePath))
            {
                throw new ArgumentException("Empty tokenFilePath");
            }
            FileTokenCache.WriteTokenToFile(token, tokenFilePath);
            PushTokenIntoTokenCache(TokenCache, token, tokenFilePath);
            return token;
        }

        /// <summary>
        /// Load client credential from tokefile.
        /// </summary>
        /// <returns>OAuthToken.</returns>
        /// <exception cref="ArgumentException">Thrown if tokenFilePath is null or empty.</exception>
        [Obsolete("Please pass a tokenFilePath to your RequestFactory constructor instead of using this method.")]
        public Boolean loadClientCredentialToken(String tokenFilePath)
        {
            Debug.WriteLine("[Obsolete] loadClientCredentialToken called");

            if (string.IsNullOrEmpty(tokenFilePath))
            {
                throw new ArgumentException("Empty tokenFilePath");
            }
            try
            {
                this.ClientCredential = FileTokenCache.ReadTokenFromFile(tokenFilePath);
                return true;
            }
            catch (InvalidTokenCacheException)
            {
                return false;
            }
        }

        // ugly manual type checking and internals access to make this
        // legacy method work until RequestFactoryGetNewClientCredential(path)
        // is removed.
        private static void PushTokenIntoTokenCache(ITokenCache tokenCache, OAuthToken token, string tokenFilePath)
        {
            if (tokenCache is InMemoryTokenCache)
            {
                (tokenCache as InMemoryTokenCache).token = token;
            }
            else if (tokenCache is FileTokenCache)
            {
                (tokenCache as FileTokenCache).tokenFilePath = tokenFilePath;
            }
        }
    }
}
