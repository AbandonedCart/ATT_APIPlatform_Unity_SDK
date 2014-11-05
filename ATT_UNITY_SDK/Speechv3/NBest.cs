// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="NBest.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.Speechv3
{
    using System.Collections.Generic;

    /// <summary>
    /// %NBest returned by speech to text response.
    /// %NBest is a complex structure that holds the results of the transcription. Supports multiple transcriptions.
    /// </summary>
    public class NBest
    {
        /// <summary>
        /// Initializes a new instance of the NBest class.
        /// </summary>
        public NBest()
        {
        }

        /// <summary>
        /// Gets or sets the Confidence.
        /// The confidence value of the Hypothesis, a value between 0.0 and 1.0 inclusive.
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// Gets or sets the Hypothesis.
        /// The transcription of the audio. 
        /// </summary>
        public string Hypothesis { get; set; }

        /// <summary>
        /// Gets or sets the LanguageId.
        /// The language used to decode the Hypothesis. 
        /// Represented using the two-letter ISO 639 language code, hyphen, two-letter ISO 3166 country code in lower case, e.g. “en-us”.
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the Grade.
        /// A machine-readable string indicating an assessment of utterance/result quality and the recommended treatment of the Hypothesis.
        /// The assessment reflects a confidence region based on prior experience with similar results.
        /// accept - the hypothesis value has acceptable confidence
        /// confirm - the hypothesis should be independently confirmed due to lower confidence
        /// reject - the hypothesis should be rejected due to low confidence
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// Gets or sets the ResultText.
        /// A text string prepared according to the output domain of the application package. 
        /// The string will generally be a formatted version of the hypothesis, 
        /// but the words may have been altered through insertions/deletions/substitutions to make the result more readable or usable for the client.  
        /// </summary>
        public string ResultText { get; set; }

        /// <summary>
        /// Gets or sets the Words. List of strings
        /// The words of the Hypothesis split into separate strings.  
        /// May omit some of the words of the Hypothesis string, and can be empty.  
        /// Never contains words not in hypothesis string.  
        /// </summary>
        public List<string> Words { get; set; }

        /// <summary>
        /// Gets or sets the Words. List of numbers
        /// The confidence scores for each of the strings in the words array.  
        /// Each value ranges from 0.0 to 1.0 inclusive.
        /// </summary>
        public List<decimal> WordScores { get; set; }

        /// <summary>
        /// Gets or sets the value of NluHypothesis.
        /// </summary>
        public NluHypothesis NluHypothesis { get; set; }
    }
}
