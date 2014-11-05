// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="Info.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.Speechv3
{
    /// <summary>
    /// Encapsulates %Info data returned in speect to text response
    /// </summary>
    public class Info
    {
        /// <summary>
        /// Gets or sets actionType
        /// It is a string with either “EPG” or “COMMAND” indicating the high level action to be associated with the result
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Gets or sets interpretation
        /// It is a dictionary containing slots that provide semantic meaning to the recognized utterances. This dictionary 
        /// will normally be derived from the top n-best nluHypothesis dictionary.
        /// Slots used are: “cast”,“title”, “station.name”, “genre.id”, “genre.words”, and “station.number
        /// </summary>
        public Interpretation Interpretation { get; set; }

        /// <summary>
        /// Gets or sets metrics. 
        /// Metrics is a dictionary with items that show metric-information about the received audio-file, namely: audioTime and audioBytes. 
        /// </summary>
        public Metrics Metrics { get; set; }

        /// <summary>
        /// Gets or sets recognized
        /// the recognition result used to produce the search results- usually the same as the top n-best resultText string.
        /// </summary>
        public string Recognized { get; set; }

        /// <summary>
        /// Gets or sets search
        /// the search dictionary contains the results of the search on the
        /// EPG database for the requested lineup (market ID). If the search feature
        /// has been disabled, the “search” field will not be present.
        /// </summary>
        public Search Search { get; set; }

        /// <summary>
        /// Gets or sets version. Version is a string indicating the version of the processor providing the response
        /// </summary>
        public string Version { get; set; }

    }
}
