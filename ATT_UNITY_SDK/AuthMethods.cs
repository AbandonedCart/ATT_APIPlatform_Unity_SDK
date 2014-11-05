using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

namespace ATT_UNITY_SDK
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {
        /// <summary>
        /// Reset the current credentials and force new credentials to be generated on the next request.
        /// </summary>
        public void ResetCredentials()
        {
            Debug.WriteLine("ResetCredentials called");

            this.ClientCredential = null;
            this.AuthorizeCredential = null;
        }

        /// <summary>
        /// Get the Access Token associated with an Authorization Code
        /// </summary>
        /// <param name="authorizationCode">The Authorization Code returned by the OAuth Process.</param>
        public void GetAuthorizeCredentials(string authorizationCode)
        {
            Debug.WriteLine("GetAuthorizeCredentials(string authorizationCode) called");
            authorizationCode = Validate("authorizationCode", authorizationCode);

            if (!(this.Scopes.Contains(ScopeTypes.MIM) || this.Scopes.Contains(ScopeTypes.DeviceCapability) || this.Scopes.Contains(ScopeTypes.TerminalLocation) || this.Scopes.Contains(ScopeTypes.MOBO) || this.Scopes.Contains(ScopeTypes.IMMN)))
            {
                string errorMessage = "Missing Device Capability or Terminal Location or MOBO/IMMN or MIM scope";
                Debug.WriteLine(errorMessage);
                throw new InvalidScopeException(errorMessage);
            }

            if (this.AuthorizeCredential != null && !this.AuthorizeCredential.IsExpired())
            {
                Debug.WriteLine("we already have authorization credentials; use them instead of fetching new ones");
                return;
            }

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("client_id", this.ClientId);
            parameters.Add("client_secret", this.ClientSecret);
            parameters.Add("code", authorizationCode);

            string body = this.FormEncode(parameters);

            string resp = this.Send(HTTPMethods.POST, "/oauth/v4/token", parameters);

            this.AuthorizeCredential = OAuthToken.ParseJSON(resp);
        }

        /// <summary>
        /// Gets the access token
        /// </summary>
        private void GetAuthorizeCredentials()
        {
            Debug.WriteLine("GetAuthorizeCredentials() called");

            if (this.AuthorizeCredential == null)
            {
                throw new UnauthorizedRequest();
            }

            if (this.AuthorizeCredential.IsExpired())
            {
                // If Refresh Token is not expired, use the refresh token to get new access token
                if (this.AuthorizeCredential.CreationTime > DateTime.Now.AddSeconds(-(TokenConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_TIME)))
                {
                    this.AuthorizeCredential = this.GetRefreshedAuthorizeCredential();
                }
                else
                {
                    throw new TokenExpiredException("Access Token & Refresh Token expired");
                }

                if (this.AuthorizeCredential == null)
                {
                    throw new UnauthorizedRequest();
                }
            }
        }

        /// <summary>
        /// Get the redirect Uri to initiate the OAuth redirect process
        /// </summary>
        /// <param name="customParam">Pass a string with values "bypass_onnetwork_auth","suppress_landing_page" separated by comma
        /// corresponding to "Force offnet flow" and "Remember Me" features. 
        /// Default vlaue is null resulting in no custom params being sent during oauth redirection </param>
        /// <returns>The string containing the OAuth Redirect that must be passed back to the browser.</returns>
        public Uri GetOAuthRedirect(string customParam)
        {
            Debug.WriteLine("GetOAuthRedirect called");

            if (!(this.Scopes.Contains(ScopeTypes.DeviceCapability) || this.Scopes.Contains(ScopeTypes.TerminalLocation)
                || this.Scopes.Contains(ScopeTypes.MOBO) || this.Scopes.Contains(ScopeTypes.MIM) || this.Scopes.Contains(ScopeTypes.IMMN)))
            {
                throw new InvalidScopeException("Missing Device Capability or Terminal Location or MOBO/IMMN or MIM scope");
            }

            String custom_Param = "";
            if (customParam != null)
            {
                if (customParam.Contains("bypass_onnetwork_auth"))
                    custom_Param = "bypass_onnetwork_auth";

                if (customParam.Contains("suppress_landing_page"))
                    custom_Param = "suppress_landing_page";

                if (customParam.Contains("bypass_onnetwork_auth") && customParam.Contains("suppress_landing_page"))
                    custom_Param = "bypass_onnetwork_auth,suppress_landing_page ";
            }

            String relativeUrl = "";
            if (string.IsNullOrEmpty(customParam))
                relativeUrl = string.Format("/oauth/v4/authorize?client_id={0}&scope={1}&redirect_uri={2}", this.ClientId, ScopesList(), this.RedirectUri);
            else
                relativeUrl = string.Format("/oauth/v4/authorize?client_id={0}&scope={1}&redirect_uri={2}&custom_param={3}", this.ClientId, ScopesList(), this.RedirectUri, custom_Param);

            return new Uri(this.EndPoint, relativeUrl);
        }


        /// <summary>
        /// Fetches access token using refresh token and stores to ClientCredential Object
        /// <exception cref="InvalidResponseException"> thrown if there is an error</exception>
        /// </summary>
        public void RefreshClientCredentials()
        {
            Debug.WriteLine("RefreshClientCredentials called");

            if (!(this.Scopes.Contains(ScopeTypes.DeviceCapability) || this.Scopes.Contains(ScopeTypes.TerminalLocation)
                || this.Scopes.Contains(ScopeTypes.MOBO) || this.Scopes.Contains(ScopeTypes.MIM) || this.Scopes.Contains(ScopeTypes.IMMN)))
            {
                throw new InvalidScopeException("Missing Device Capability or Terminal Location or MOBO/IMMN or MIM scope");
            }

            // If Existing credentials are active, return.
            if (this.ClientCredential != null && !this.ClientCredential.IsExpired())
            {
                return;
            }

            // If there is a client credential and it is less than  DEFAULT_REFRESH_TOKEN_EXPIRY_TIME seconds hold,
            // then fetch new access token using refresh token.
            if (this.ClientCredential != null && this.ClientCredential.CreationTime > DateTime.Now.AddSeconds(-(TokenConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_TIME)))
            {
                try
                {
                    this.ClientCredential = this.GetRefreshedClientCredential();
                    return;
                }
                catch
                {
                    throw new InvalidResponseException("Failed");
                }
            }

            throw new InvalidResponseException("Failed");
        }

        /// <summary>
        /// Contact the OAuth endpoint to get new client credentials. 
        /// </summary>
        /// <returns>The <see cref="OAuthToken"/> returned by the server.</returns>
        public OAuthToken GetNewClientCredential()
        {
            Debug.WriteLine("GetNewClientCredential() called");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", this.ClientId);
            parameters.Add("client_secret", this.ClientSecret);
            parameters.Add("grant_type", "client_credentials");
            parameters.Add("scope", ScopesList());
            string jsonResponse = this.DoPost("/oauth/v4/token", parameters);
            OAuthToken token;
            try
            {
                token = OAuthToken.ParseJSON(jsonResponse);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidResponseException("response was not valid json or OAuth token: " + jsonResponse, ex);
            }
            this.TokenCache.SaveToken(token);
            return token;
        }

        /// <summary>
        /// Contact the OAuth endpoint to refresh existing client credentials.
        /// </summary>
        /// <returns>The <see cref="OAuthToken"/> returned by the server.</returns>
        public OAuthToken GetRefreshedClientCredential()
        {
            Debug.WriteLine("GetRefreshedClientCredential called");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", this.ClientId);
            parameters.Add("client_secret", this.ClientSecret);
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", this.ClientCredential.RefreshToken);

            OAuthToken token = OAuthToken.ParseJSON(this.DoPost("/oauth/v4/access_token", parameters));
            this.TokenCache.SaveToken(token);
            return token;
        }

        /// <summary>
        /// Contact the OAuth endpoint to refresh existing Authorize credentials.
        /// </summary>
        /// <returns>The <see cref="OAuthToken"/> returned by the server.</returns>
        public OAuthToken GetRefreshedAuthorizeCredential()
        {
            Debug.WriteLine("GetRefreshedAuthorizeCredential called");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", this.ClientId);
            parameters.Add("client_secret", this.ClientSecret);
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", this.AuthorizeCredential.RefreshToken);

            return OAuthToken.ParseJSON(this.DoPost("/oauth/v4/token", parameters));
        }

        /// <summary>
        /// Get valid client credentials from the best available source
        /// </summary>
        public void GetClientCredentials()
        {
            Debug.WriteLine("GetClientCredentials called");

            if (this.ClientCredential == null)
            {
                try
                {
                    this.ClientCredential = this.TokenCache.LoadToken();
                }
                catch (InvalidTokenCacheException)
                {
                    this.ClientCredential = this.GetNewClientCredential();
                }
            }

            if (this.ClientCredential.IsExpired())
            {
                // If the refresh token is not expired, use the refresh token to get access token
                if (this.ClientCredential != null && this.ClientCredential.CreationTime > DateTime.Now.AddSeconds(-(TokenConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_TIME)))
                {
                    try
                    {
                        this.ClientCredential = this.GetRefreshedClientCredential();
                        return;
                    }
                    catch
                    {
                        // if refresh token fails, fall through and try to get a clean new token
                    }
                }
                this.ClientCredential = this.GetNewClientCredential();
            }
        }

        /// <summary>
        /// Revoke a token at the authorizing server - nobody can use this token
        /// anywhere, after it is revoked.
        /// 
        /// This calls the revoke operation using the 'refresh token' hint type;
        /// this automatically also revokes the associated access token.
        /// </summary>
        /// <param name="token">the token to be revoked</param>
        public void RevokeToken(string token)
        {
            RevokeToken(token, this.ClientId, this.ClientSecret);
        }

        /// <summary>
        /// Revoke a token at the authorizing server - nobody can use this token
        /// anywhere, after it is revoked.
        /// 
        /// This calls the revoke operation using the 'refresh token' hint type;
        /// this automatically also revokes the associated access token.
        /// </summary>
        /// <param name="token">the token to be revoked</param>
        /// <param name="clientId">the public ID of the app calling this method, as registered at http://developer.att.com</param>
        /// <param name="clientSecret">the private ID of the app calling this method, as registered at http://developer.att.com</param>
        public void RevokeToken(string token, string clientId, string clientSecret)
        {
            Debug.WriteLine("RevokeToken called");

            Validate("token", token);
            Validate("clientId", clientId);
            Validate("clientSecret", clientSecret);

            var formParameters = new NameValueCollection();
            formParameters.Add("client_id", clientId);
            formParameters.Add("client_secret", clientSecret);
            formParameters.Add("token", token);
            formParameters.Add("token_type_hint", "refresh_token");

            var encoding = new UTF8Encoding();
            byte[] body = encoding.GetBytes(this.FormEncode(formParameters));

            this.Http.Send("/oauth/v4/revoke", null, body, "application/x-www-form-urlencoded", "application/json");
        }

        /// <summary>
        ///  Convert the Scopes list into a string for use in the OAuth call.
        /// </summary>
        /// <param name="scopes">A list of the scopes.</param>
        /// <returns>The string version of the scopes list.</returns>
        private string ScopesList(List<ScopeTypes> scopes)
        {
            StringBuilder sb = new StringBuilder();
            if (scopes.Contains(ScopeTypes.DeviceCapability))
            {
                sb.Append("DC ");
            }

            if (scopes.Contains(ScopeTypes.TerminalLocation))
            {
                sb.Append("TL ");
            }

            if (scopes.Contains(ScopeTypes.SMS))
            {
                sb.Append("SMS ");
            }

            if (scopes.Contains(ScopeTypes.MMS))
            {
                sb.Append("MMS ");
            }

            if (scopes.Contains(ScopeTypes.Payment))
            {
                sb.Append("PAYMENT ");
            }

            if (scopes.Contains(ScopeTypes.WAPPush))
            {
                sb.Append("WAP ");
            }

            if (scopes.Contains(ScopeTypes.MOBO) || scopes.Contains(ScopeTypes.IMMN))
            {
                sb.Append("IMMN ");
            }

            if (scopes.Contains(ScopeTypes.MIM))
            {
                sb.Append("MIM ");
            }

            if (scopes.Contains(ScopeTypes.Speech))
            {
                sb.Append("SPEECH ");
            }

            if (scopes.Contains(ScopeTypes.STTC))
            {
                sb.Append("STTC ");
            }

            if (scopes.Contains(ScopeTypes.ADS))
            {
                sb.Append("ADS ");
            }

            if (scopes.Contains(ScopeTypes.TTS))
            {
                sb.Append("TTS ");
            }

            return sb.ToString().Trim().Replace(' ', ',');
        }

        /// <summary>
        ///  Convert the Scopes list into a string for use in the OAuth call.
        /// </summary>
        /// <returns>The string version of the scopes list.</returns>
        private string ScopesList()
        {
            return this.ScopesList(this.Scopes);
        }
    }
}
