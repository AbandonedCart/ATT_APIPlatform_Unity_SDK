// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="InvalidScopeException.cs" company="AT&amp;T Intellectual Property">
// Copyright AT&amp;T, 2012.
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    using System;

    /// <summary>
    /// Exception returned if the %RequestFactory does not include the scope necessary for this call.
    /// </summary>
    public class InvalidScopeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidScopeException class.
        /// </summary>
        public InvalidScopeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidScopeException class.
        /// </summary>
        /// <param name="message">String message describing the exception.</param>
        public InvalidScopeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidScopeException class.
        /// </summary>
        /// <param name="message">String message describing the exception.</param>
        /// <param name="ex">The nested exception.</param>
        public InvalidScopeException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
