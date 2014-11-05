// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="OAuthToken.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------
using Newtonsoft.Json;

namespace ATT_UNITY_SDK
{
    using System;
    using System.Diagnostics.CodeAnalysis;
//    using System.Web.Script.Serialization;

    /// <summary>
    /// Encapsulates OAuth token data and parsing
    /// </summary>
    [Serializable]
    public class OAuthToken
    {
        /// <summary>
        /// Access token value returned by the server.
        /// </summary>
        private string accessToken;

        /// <summary>
        /// Refresh token value returned by the server.
        /// </summary>
        private string refreshToken;

        /// <summary>
        /// Number of seconds until the access token expires
        /// </summary>
        private long expiresIn;

        /// <summary>
        /// Expiration time of the access token
        /// </summary>
        private DateTime expiration;

        /// <summary>
        /// Creation time of the access token
        /// </summary>
        private DateTime creationTime;

        /// <summary>
        /// Initializes a new instance of the OAuthToken class.
        /// </summary>
        public OAuthToken()
        {
        }

        /// <summary>
        /// Initializes a new instance of the OAuthToken class.
        /// </summary>
        /// <param name="accessToken">Access token value returned by the server.</param>
        /// <param name="refreshToken">Refresh token value returned by the server.</param>
        /// <param name="expiresIn">Number of seconds until the access token expires.</param>
        /// <param name="createdTime">The initial creation time of access token in earlier requests.</param>
        /// <exception cref="ArgumentException">Thrown if headers are null, empty, or invalid.</exception>
        public OAuthToken(string accessToken, string refreshToken, string expiresIn, string createdTime)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("accessToken invalid");
            }

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("refreshToken invalid");
            }

            if (string.IsNullOrEmpty(expiresIn))
            {
                expiresIn = "0";
                //throw new ArgumentException("expiresIn invalid");
            }

            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
            try
            {
                this.expiresIn = long.Parse(expiresIn);

                //If access token already acquired. make that creation time to calculate new expiry time.
                if (!string.IsNullOrEmpty(createdTime))
                {
                    this.creationTime = Convert.ToDateTime(createdTime);
                }
                else
                {
                    this.creationTime = DateTime.Now;
                }

                if (this.expiresIn == 0)
                {
                    this.expiration = this.creationTime.AddSeconds(TokenConstants.DEFAULT_ACCESS_TOKEN_EXPIRY_TIME);
                }
                else
                {
                    this.expiration = this.creationTime.AddSeconds((double)this.expiresIn);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("unable to parse expiresIn: " + expiresIn);
            }
        }

        /// <summary>
        /// Gets the access token value
        /// </summary>
        public string AccessToken
        {
            get
            {
                return this.accessToken;
            }
        }

        /// <summary>
        /// Gets the refresh token value
        /// </summary>
        public string RefreshToken
        {
            get
            {
                return this.refreshToken;
            }
        }

        /// <summary>
        /// Gets the expiration time of this access token
        /// </summary>
        public DateTime Expiration
        {
            get
            {
                return this.expiration;
            }
        }

        /// <summary>
        /// Gets the creation time of this access token
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return this.creationTime;
            }
        }

        /// <summary>
        /// Gets the number of seconds until the access token expires.
        /// </summary>
        public long ExpiresIn
        {
            get
            {
                return this.expiresIn;
            }
        }

        /// <summary>
        /// Creates an OAuthToken from a JSON string.
        /// </summary>
        /// <param name="jsonInput">The JSON string to parse.</param>
        /// <returns>An OAuthToken.</returns>
        public static OAuthToken ParseJSON(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                throw new ArgumentException("Empty access token");
            }

            OAuthTokenRaw raw = JsonConvert.DeserializeObject<OAuthTokenRaw>(jsonInput);

            return new OAuthToken(raw.access_token, raw.refresh_token, raw.expires_in, null);
        }

        /// <summary>
        /// Returns the string version of the OAuthToken object
        /// </summary>
        /// <returns>Formatted AuthToken</returns>
        public override string ToString()
        {
           return string.Format("OAuthToken: AccessToken={0}, RefreshToken={1}, ExpiresIn={2}, Expiration={3}", this.AccessToken, this.RefreshToken, this.ExpiresIn, this.Expiration);
        }

        /// <summary>
        /// Checks the access token for expiration.
        /// </summary>
        /// <returns>true if the access token has expired.</returns>
        public bool IsExpired()
        {
            try
            {
                return DateTime.Now > this.expiration;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Internal class used for JSON de-serialization.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore")]
        private class OAuthTokenRaw
        {
            /// <summary>
            /// Initializes a new instance of the OAuthTokenRaw class.
            /// </summary>
            public OAuthTokenRaw()
            {
            }

            /// <summary>
            /// Initializes a new instance of the OAuthTokenRaw class.
            /// </summary>
            /// <param name="access_token">access token returned by server</param>
            /// <param name="refresh_token">refresh token returned by server</param>
            /// <param name="expires_in">number of seconds before expiration</param>
            public OAuthTokenRaw(string access_token, string refresh_token, string expires_in)
            {
                this.access_token = access_token;
                this.refresh_token = refresh_token;
                this.expires_in = expires_in;
            }

            /// <summary>
            /// Gets or sets the access token value.
            /// access_token is used by the application server to make subsequent API method requests.
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// Gets or sets the expires in value.
            /// expiry time returned in the response denotes the number of seconds before the current access token expires.
            /// </summary>
            public string expires_in { get; set; }

            /// <summary>
            /// Gets or sets the refresh token value.
            /// refresh_token returned in the response can be used to obtain a new access_token after the initial access_token returned in this response expires.
            /// </summary>
            public string refresh_token { get; set; }
        }
    }
}
