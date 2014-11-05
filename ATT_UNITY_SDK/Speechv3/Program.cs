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
    /// Encapsulates Program data returned in speech to text response
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets or sets cast
        /// </summary>
        public string Cast { get; set; }

        /// <summary>
        /// Gets or sets category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets director
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// Gets or sets language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets mpaaRating
        /// </summary>
        public string MpaaRating { get; set; }

        /// <summary>
        /// Gets or sets originalAirDate
        /// </summary>
        public string OriginalAirDate { get; set; }

        /// <summary>
        /// Gets or sets pid
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// Gets or sets runTime
        /// </summary>
        public string RunTime { get; set; }

        /// <summary>
        /// Gets or sets showType
        /// </summary>
        public string ShowType { get; set; }

        /// <summary>
        /// Gets or sets starRating
        /// </summary>
        public string StarRating { get; set; }

        /// <summary>
        /// Gets or sets subtitle
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets year
        /// </summary>
        public string Year { get; set; }
    }
}
