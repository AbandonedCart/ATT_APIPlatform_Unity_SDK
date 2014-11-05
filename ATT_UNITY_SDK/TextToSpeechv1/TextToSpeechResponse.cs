// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="TextToSpeechResponse.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.TextToSpeechv1
{
    /// <summary>
    /// Encapsulates the response of TextToSpeech API.
    /// </summary>
    public class TextToSpeechResponse : Response
    {
        /// <summary>
        /// Gets or sets Content Length of speech data
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets Audio Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets converted Speech Content
        /// </summary>
        public byte[] SpeechContent { get; set; }
    }
}
