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
    /// Encapsulates Showtime data retuned in speech to text response
    /// </summary>  
    public class Showtime
    {
        /// <summary>
        /// Gets or sets affiliate
        /// </summary>
        public string Affiliate { get; set; }

        /// <summary>
        /// Gets or sets callSign
        /// </summary>
        public string CallSign { get; set; }

        /// <summary>
        /// Gets or sets channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets closeCaptioned
        /// </summary>
        public string CloseCaptioned { get; set; }

        /// <summary>
        /// Gets or sets dolby
        /// </summary>
        public string Dolby { get; set; }

        /// <summary>
        /// Gets or sets duration
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets endTime
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or sets finale
        /// </summary>
        public string Finale { get; set; }

        /// <summary>
        /// Gets or sets hdtv
        /// </summary>
        public string Hdtv { get; set; }

        /// <summary>
        /// Gets or sets newShow
        /// </summary>
        public string NewShow { get; set; }

        /// <summary>
        /// Gets or sets pid
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// Gets or sets premier
        /// </summary>
        public string Premier { get; set; }

        /// <summary>
        /// Gets or sets repeat
        /// </summary>
        public string Repeat { get; set; }

        /// <summary>
        /// Gets or sets showTime
        /// </summary>
        public string ShowTime { get; set; }

        /// <summary>
        /// Gets or sets station
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// Gets or sets stereo
        /// </summary>
        public string Stereo { get; set; }

        /// <summary>
        /// Gets or sets subtitled 
        /// </summary>
        public string Subtitled { get; set; }

        /// <summary>
        /// Gets or sets weekday
        /// </summary>
        public string Weekday { get; set; }
    }
}
