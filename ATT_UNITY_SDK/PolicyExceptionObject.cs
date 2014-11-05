// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="PolicyExceptionObject.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------
namespace ATT_UNITY_SDK
{
    /// <summary>
    /// %PolicyExceptionObject is returned as part of error response from platform.
    /// These exceptions occur when a policy criteria has not been met.  
    /// </summary>
    public class PolicyExceptionObject : ErrorObjectRaw
    {
        /// <summary>
        /// Gets or sets the value of message id
        /// Unique message identifier of the format 'ABCnnnn' where 'ABC' is either 'SVC' for Service Exceptions or 'POL' for Policy 
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the value of text
        /// Message text, with replacement variables marked with %n, where n is an index into the list of elements, starting at 1
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value of variables
        /// List of zero or more strings that represent the contents of the variables used by the message text.
        /// </summary>
        public string Variables { get; set; }
    }
}
