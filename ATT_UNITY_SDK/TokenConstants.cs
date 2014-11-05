// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="TokenConstants.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// Encapsulates OAuth Token constants
    /// </summary>
    public static class TokenConstants
    {
        /// <summary>
        /// Default access token expiry time in seconds if expires_in returns as 0
        /// </summary>
        public const uint DEFAULT_ACCESS_TOKEN_EXPIRY_TIME = 3153600000; // Seconds, equal to 100 years.

        /// <summary>
        /// Default refresh token expiry time
        /// </summary>
        public const uint DEFAULT_REFRESH_TOKEN_EXPIRY_TIME = 86400; // Seconds, equal to 24 hours

    }
}
