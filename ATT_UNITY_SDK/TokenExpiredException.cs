// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="TokenExpiredException.cs" company="AT&amp;T Intellectual Property">
// Copyright AT&amp;T, 2012.
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    using System;

    /// <summary>
    /// Exception object that will be returned if the token is expired
    /// </summary>
    public class TokenExpiredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the TokenExpiredException class.
        /// </summary>
        public TokenExpiredException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TokenExpiredException class.
        /// </summary>
        /// <param name="message">String message describing the exception.</param>
        public TokenExpiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TokenExpiredException class.
        /// </summary>
        /// <param name="message">String message describing the exception.</param>
        /// <param name="ex">The nested exception.</param>
        public TokenExpiredException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
