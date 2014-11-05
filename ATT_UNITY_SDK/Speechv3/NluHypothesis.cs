// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="NluHypothesis.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates NluHypothesis data retuned in speech to text response
    /// </summary>
    public class NluHypothesis
    {
        /// <summary>
        /// Gets or sets cast
        /// </summary>
        public string Cast { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the station.number
        /// 
        /// </summary>
        public string Station_number { get; set; }

        /// <summary>
        /// Gets or sets the station.name
        /// 
        /// </summary>
        public string Station_name { get; set; }
        
        /// <summary>
        /// Gets or sets the genre_id
        /// 
        /// </summary>
        public string Genre_id { get; set; }

        /// <summary>
        /// Gets or sets genre_words
        /// </summary>
        public string Genre_words { get; set; }

        /// <summary>
        /// Gets or sets OutComposite
        /// </summary>
        public List<OutComposite> OutComposite { get; set; }
    }
}
