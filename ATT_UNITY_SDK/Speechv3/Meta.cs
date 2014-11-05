// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="NluHypothesis.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------
namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates the Meta info retuned in speech to text response
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets guideDateEnd 
        /// </summary>
        public object GuideDateEnd { get; set; }

        /// <summary>
        /// Gets or sets guideDateStart
        /// </summary>
        public object GuideDateStart { get; set; }

        /// <summary>
        /// Gets or sets lineup 
        /// </summary>
        public string Lineup { get; set; }

        /// <summary>
        /// Get or sets market
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// Gets or sets resultCount
        /// </summary>
        public int ResultCount { get; set; }
    }
}
