// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="ServiceErrorObject.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// %ServiceErrorObject is returned as part of error response from platform.
    /// These errors occur when a service is unable to process a request and retrying of the request will result in a consistent failure.
    /// </summary>
    public class ServiceErrorObject : ErrorObjectRaw
    {
        /// <summary>
        /// Gets or sets the value of FaultCode
        /// </summary>
        public string FaultCode { get; set; }

        /// <summary>
        /// Gets or sets the value of FaultMessage
        /// </summary>
        public string FaultMessage { get; set; }

        /// <summary>
        /// Gets or sets the value of FaultDescription
        /// </summary>
        public string FaultDescription { get; set; }
    }
}
