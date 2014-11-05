// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="ServiceExceptionObject.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// %ServiceExceptionObject is returned as part of error response from platform.
    /// These exceptions occur when a service is unable to process a request and retrying of the request will result in a consistent failure.
    /// </summary>
    public class ServiceExceptionObject : ErrorObjectRaw
    {
        /// <summary>
        /// Gets or sets the value of message id
        /// Unique message identifier of the format 'ABCnnnn' where 'ABC' is either 'SVC' for Service Exceptions or 'POL' for Policy 
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the value of Text
        /// Message text, with replacement variables marked with %n, where n is an index into the list of variables elements, starting at 1
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value of variables
        /// List of zero or more strings that represent the contents of the variables used by the message text.
        /// </summary>
        public string Variables { get; set; }
    }
}
