// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="InvalidResponseException.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    using System;
    using System.Net;

    /// <summary>
    /// Exception returned if there is an error returned by the server or a connection failure.
    /// </summary>
    public class InvalidResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidResponseException class.
        /// </summary>
        public InvalidResponseException()
            : this(null, null, 0, null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the InvalidResponseException class.
        /// </summary>
        /// <param name="message">A string used to describe the exception.</param>
        public InvalidResponseException(string message)
            : this(message, null, HttpStatusCode.BadRequest, null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the InvalidResponseException class.
        /// </summary>
        /// <param name="message">A string used to describe the exception.</param>
        /// <param name="ex">The nested exception.</param>
        public InvalidResponseException(string message, Exception ex)
            : this(message, ex, HttpStatusCode.BadRequest, null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the InvalidResponseException class.
        /// </summary>
        /// <param name="message">A string used to describe the exception.</param>
        /// <param name="ex">The nested exception.</param>
        /// <param name="status">The HTTP status code returned by the server.</param>
        /// <param name="body">The body of the response returned by the server.</param>
        public InvalidResponseException(string message, Exception ex, HttpStatusCode status, string body)
            : base(message, ex)
        {
            this.Status = status;
            this.Body = body;
        }

        /// <summary>
        /// Gets or sets the HTTP status code of the invalid response message.
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the formatData of the invalid response message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the error response for invalid response message
        /// </summary>
        public ErrorResponse ErrorResponseObject { get; set; }
    }
}