// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="Search.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates Search data returned in speech to text response
    /// </summary>
    public class Search
    {   
        /// <summary>
        /// Gets or sets meta
        /// A dictionary of informational fields related to the search and the results returned
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// Gets or sets programs, a list of programs describing a TV show or movie in the results
        /// 
        /// </summary>
        public List<Program> Programs { get; set; }

        /// <summary>
        /// Gets or sets showtimes
        /// a list of show timings of the above programs, indicating start-time and channel. 
        /// </summary>
        public List<Showtime> Showtimes { get; set; }
    }
}
