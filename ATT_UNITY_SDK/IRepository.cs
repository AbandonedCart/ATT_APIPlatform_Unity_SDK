// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// Base interface for saving and retrieving OAuthToken.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Save OAuthToken to the persistence model chosen.
        /// </summary>
        /// <param name="oauthToken">OAuthToken instance.</param>
        void SaveToken(OAuthToken oauthToken);

        /// <summary>
        /// Get the OAuthToken from the persistence model.
        /// </summary>
        /// <returns>An instance of OAuthToken</returns>
        OAuthToken GetToken();
    }
}
