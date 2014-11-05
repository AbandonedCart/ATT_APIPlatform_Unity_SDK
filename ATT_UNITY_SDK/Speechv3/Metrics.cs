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
    /// Encapsulates Metrics data returned in speect to text response
    /// </summary>
    public class Metrics
    {
        /// <summary>
        /// Gets or sets audioBytes
        /// </summary>
        public int AudioBytes { get; set; }

        /// <summary>
        /// Gets or sets audioTime
        /// </summary>
        public double AudioTime { get; set; }
    }
}
