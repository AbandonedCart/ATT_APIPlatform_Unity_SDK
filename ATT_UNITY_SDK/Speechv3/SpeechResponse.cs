// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="SpeechResponse.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------
using Newtonsoft.Json;

namespace ATT_UNITY_SDK.Speechv3
{
    using System;
//    using System.Web.Script.Serialization;

#pragma warning disable 649

    /// <summary>
    /// The response to a Speech To Text request.
    /// </summary>
    public class SpeechResponse : Response
    {
        /// <summary>
        /// Initializes a new instance of the SpeechResponse class.
        /// </summary>
        public SpeechResponse()
        {
        }

        /// <summary>
        /// Gets or sets the Recognition returned by the server.
        /// </summary>
        public Recognition Recognition { get; set; }

        /// <summary>
        /// Creates an SpeechResponse object by parsing the JSON input.
        /// </summary>
        /// <param name="jsonInput">The JSON input string to parse.</param>
        /// <returns>The <see cref="SpeechResponse"/> object returned by the server.</returns>
        internal static SpeechResponse Parse(string jsonInput)
        {
            if (string.IsNullOrEmpty(jsonInput))
            {
                throw new ArgumentException("Empty Speech to text Response");
            }

            try
            {
                jsonInput = jsonInput.Replace("genre.id", "Genre_id");
                jsonInput = jsonInput.Replace("genre.words", "Genre_words");
                jsonInput = jsonInput.Replace("station.name", "Station_name");
                jsonInput = jsonInput.Replace("station.number", "Station_number");

				return JsonConvert.DeserializeObject<SpeechResponse>(jsonInput);
            }
            catch (Exception e)
            {
                throw new InvalidResponseException("Invalid Speech to text Response", e);
            }
        }
    }
}