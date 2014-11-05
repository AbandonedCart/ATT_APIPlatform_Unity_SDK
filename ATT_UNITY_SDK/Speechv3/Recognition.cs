// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="Recognition.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.Speechv3
{
    using System.Collections.Generic;

    /// <summary>
    /// %Recognition returned by the server for Speech to text request.
    /// %Recognition is a complex structure for all the other parameters of the response.
    /// </summary>
    public class Recognition
    {
        /// <summary>
        /// Initializes a new instance of the Recognition class.
        /// </summary>
        public Recognition()
        {
        }

        /// <summary>
        /// Gets or sets the id value assigned by the server to the speech to text response.
        /// A unique string that identifies this particular transcription.
        /// </summary>
        public string Responseid { get; set; }

        /// <summary>
        /// Gets or sets the NBest object.
        /// NBest is a complex structure that holds the results of the transcription.
        /// </summary>
        public List<NBest> NBest { get; set; }

        /// <summary>
        /// Gets or sets the status of the request
        /// Result of the particular transcription.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the value of Info object.
        /// </summary>
        public Info Info { get; set; }
    }
}
