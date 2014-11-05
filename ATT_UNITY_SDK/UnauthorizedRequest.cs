// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="UnauthorizedRequest.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    using System;

    /// <summary>
    /// Exception object, that will be returned when authorization fails.
    /// </summary>
    public class UnauthorizedRequest : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnauthorizedRequest class.
        /// </summary>
        public UnauthorizedRequest()
            : this(null, null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the UnauthorizedRequest class.
        /// </summary>
        /// <param name="message">A string used to describe the exception.</param>
        public UnauthorizedRequest(string message)
            : base(message, null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the UnauthorizedRequest class.
        /// </summary>
        /// <param name="message">A string used to describe the exception.</param>
        /// <param name="ex">A nested exception.</param>
        public UnauthorizedRequest(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
