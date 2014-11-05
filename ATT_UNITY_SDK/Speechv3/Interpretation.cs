// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="Interpretation.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates %Interpretation info returned in speech to text response
    /// </summary>
    public class Interpretation
    {
        /// <summary>
        /// Gets or sets genre_id
        /// </summary>
        public string Genre_id { get; set; }

        /// <summary>
        /// Gets or sets genre_words
        /// </summary>
        public string Genre_words { get; set; }

        /// <summary>
        /// Gets or sets station_name
        /// </summary>
        public string Station_name { get; set; }
    }
}
