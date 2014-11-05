using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    class InMemoryTokenCache : ITokenCache
    {
        internal OAuthToken token;

        public OAuthToken LoadToken()
        {
            if (this.token == null)
            {
                throw new InvalidTokenCacheException("Tried to load a token that hasn't been saved yet.");
            }
            return this.token;
        }

        public void SaveToken(OAuthToken token)
        {
            this.token = token;
        }
    }
}
