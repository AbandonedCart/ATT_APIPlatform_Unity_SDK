// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="XArgs.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK.Speechv3
{
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// %XArgs structure. The X-Arg header is a meta parameter that can be used to define multiple parameter name/value pairs. 
    /// </summary>
    public class XArgs
    {
        /// <summary>
        /// Initializes a new instance of the XArgs class.
        /// </summary>
        public XArgs()
        {
        }

        /// <summary>
        /// Gets or sets the ClientApp. The application name.
        /// </summary>
        public string ClientApp { get; set; }

        /// <summary>
        /// Gets or sets the ClientVersion. A version id for the app.
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// Gets or sets the ClientScreen. A string representing the application's focus UI screen/widget for this request.
        /// </summary>
        public string ClientScreen { get; set; }

        /// <summary>
        /// Gets or sets the ClientSdk. The syntax for the parameter is library-platform-version
        /// </summary>
        public string ClientSdk { get; set; }

        /// <summary>
        /// Gets or sets the DeviceType. The device name, preferably from a standard API on the device itself (e.g. "iPhone4,1", "SGHT999", "SAMSUNG-SGH-I897", etc.)
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the DeviceOs. The version string of the OS for the device type, preferably from a standard API on the device or autofilled by an SDK.
        /// </summary>
        public string DeviceOs { get; set; }

        /// <summary>
        /// Gets or sets the DeviceTime. The local device time at the time of the request. Format "YYYY-MMDD&lt; space&gt;HH:MM:SS&lt;space&gt;TZ". 
        /// </summary>
        public string DeviceTime { get; set; }

        /// <summary>
        /// Gets or sets the value of ShowWordTokens.
        /// Control output detail. If the packages have formatting applied to the ResultText field, this flag generates additional detail to show the named entities and the words from which those entities were derived. If no formatting is applied the output still shows the derivation from the spoken Hypothesis field, but the correspondence is one-to-one with the ResultText.
        /// </summary>
        public bool? ShowWordTokens { get; set; }

        /// <summary>
        /// Gets or sets the ContentLanguage.
        /// For Generic Context only.
        /// Specifies the language of the submitted audio with one of the following two choices: ‘en-US’ (English - United States) and	‘es-US’ (Spanish - United States).
        /// If Content-Language is not specified, the default will be en-us.
        /// </summary>
        public string ContentLanguage { get; set; }

        /// <summary>
        /// Gets or sets the value of Microphone.
        /// Specifies the acoustic model to be used during processing with one of the following two choices: ‘smartphone’ and ‘farfield’
        /// </summary>
        public string Microphone { get; set; }

        /// <summary>
        /// Gets or sets the value of HasMultipleNBest.
        /// Specifies whether multiple NBest elements should be returned with output parameters.
        /// •	'true' - Send multiple NBest elements
        /// •	'false' - Send a single NBest element
        /// Default value is false.
        /// </summary>
        public bool? HasMultipleNBest { get; set; }

        /// <summary>
        /// Gets or sets the value of PunctuateFlag. Gaming Chat sub-context only.
        /// Only for en-US output. Default value is true.
        /// Automatic Punctuation. “true” for On and “false” for Off to disable the automatic punctuation of the text in the JSON ResultText.
        /// </summary>
        public bool? PunctuateFlag { get; set; }

        /// <summary>
        /// Gets or sets the value of FormatFlag. Gaming Chat sub-context only.
        /// Only for en-US output. Default value is true.
        /// Automatic text formatting (for example, numbers, times, and so forth) “true” for On and “false” for Off to disable the automatic formatting markup of the text in the JSON ResultText. 
        /// Only
        /// </summary>
        public bool? FormatFlag { get; set; }

        /// <summary>
        /// Gets or sets TVXArgParams.
        /// </summary>
        public TVXArgs TVXArgParams { get; set; }

        /// <summary>
        /// Gets or sets SocialMediaXArgParams.
        /// </summary>
        public SocialMediaXArgs SocialMediaXArgParams { get; set; }

        /// <summary>
        /// Returns the string format of the properties.
        /// </summary>
        /// <returns>Returns the string format of the properties.</returns>
        public override string ToString()
        {
            string xArgs = string.Empty;

            if (!string.IsNullOrEmpty(this.ClientApp))
            {
                xArgs += "ClientApp=" + HttpUtility.UrlEncode(this.ClientApp) + ",";
            }

            if (!string.IsNullOrEmpty(this.ClientScreen))
            {
                xArgs += "ClientScreen=" + HttpUtility.UrlEncode(this.ClientScreen) + ",";
            }

            if (!string.IsNullOrEmpty(this.ClientVersion))
            {
                xArgs += "ClientVersion=" + HttpUtility.UrlEncode(this.ClientVersion) + ",";
            }

            if (!string.IsNullOrEmpty(this.ClientSdk))
            {
                xArgs += "ClientSdk=" + HttpUtility.UrlEncode(this.ClientSdk) + ",";
            }

            if (!string.IsNullOrEmpty(this.DeviceOs))
            {
                xArgs += "DeviceOs=" + HttpUtility.UrlEncode(this.DeviceOs) + ",";
            }

            if (!string.IsNullOrEmpty(this.DeviceTime))
            {
                xArgs += "DeviceTime=" + HttpUtility.UrlEncode(this.DeviceTime) + ",";
            }

            if (!string.IsNullOrEmpty(this.DeviceType))
            {
                xArgs += "DeviceType=" + HttpUtility.UrlEncode(this.DeviceType) + ",";
            }

            if (null!=this.ShowWordTokens && this.ShowWordTokens.HasValue)
            {
                xArgs += "ShowWordTokens=" + HttpUtility.UrlEncode(this.ShowWordTokens.Value.ToString()) + ",";
            }

            if (!string.IsNullOrEmpty(this.ContentLanguage))
            {
                xArgs += "Content-Language=" + HttpUtility.UrlEncode(this.ContentLanguage) + ",";
            }

            if (!string.IsNullOrEmpty(this.Microphone))
            {
                xArgs += "Microphone=" + HttpUtility.UrlEncode(this.Microphone) + ",";
            }

            if (null!=this.HasMultipleNBest && this.HasMultipleNBest.HasValue)
            {
                xArgs += "HasMultipleNBest=" + HttpUtility.UrlEncode(this.HasMultipleNBest.Value.ToString()) + ",";
            }

            if (null!=this.PunctuateFlag && this.PunctuateFlag.HasValue)
            {
                xArgs += "PunctuateFlag=" + HttpUtility.UrlEncode(this.PunctuateFlag.Value.ToString()) + ",";
            }

            if (null!=this.FormatFlag && this.FormatFlag.HasValue)
            {
                xArgs += "FormatFlag=" + HttpUtility.UrlEncode(this.FormatFlag.Value.ToString()) + ",";
            }

            if (null != this.TVXArgParams)
            {
                xArgs += this.TVXArgParams.ToString() + ",";
            }

            if (null != this.SocialMediaXArgParams)
            {
                xArgs += this.SocialMediaXArgParams.ToString() + ",";
            }

            xArgs = xArgs.Substring(0, xArgs.Length - 1);

            return xArgs;
        }
    }

    /// <summary>
    ///  This class contains the parameters specific to TV Context.
    /// </summary>
    public class TVXArgs
    {
        /// <summary>
        /// Initializes a new instance of TVXArgs.
        /// </summary>
        public TVXArgs()
        { }

        /// <summary>
        /// Gets or sets the value of Search.
        /// </summary>
        public bool? Search { get; set; }

        /// <summary>
        /// Gets or sets the value of Lineup.
        /// The Lineup is a 5 digit code identifying one of 66 U-Verse market lineups. 
        /// </summary>
        public string Lineup { get; set; }

        /// <summary>
        /// Gets or sets the value of DeviceId.
        /// The device identifier should be an anonymized hash string identifying the device with a high likelihood of uniqueness while not providing any identifiable string or algorithmic method of recovering the original unique device identifier (UDID). 
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the vlaue of NumResults.
        /// The size of the results returned is capped by this number. Use this parameter to limit the size of the returned program list.
        /// </summary>
        public int? NumResults { get; set; }

        /// <summary>
        /// Gets or sets the value of PageRequest.
        /// Used to guide response messages and select next or previous page of partial search results. Use with “ResultKeys - FirstResultKey and LastResultKey” to get next or previous results. Valid values are: next or prev.
        /// </summary>
        public string PageRequest { get; set; }

        /// <summary>
        /// Gets or sets the value of FirstResultKey.
        /// Used with “prev” option to identify the first result from the previous response. The response will include search items from before this response.
        /// </summary>
        public string FirstResultKey { get; set; }

        /// <summary>
        /// Gets or sets the value of LastResultKey.
        /// Used with “next” Page-Request option to get next results. This string identifies the last result the user saw in the previous response.
        /// </summary>
        public string LastResultKey { get; set; }

        /// <summary>
        /// Returns the string format of the properties.
        /// </summary>
        /// <returns>Returns the string format of the properties.</returns>
        public override string ToString()
        {
            string xArgs = string.Empty;

            if (!string.IsNullOrEmpty(this.DeviceId))
            {
                xArgs += "DeviceId=" + HttpUtility.UrlEncode(this.DeviceId) + ",";
            }

            if (!string.IsNullOrEmpty(this.FirstResultKey))
            {
                xArgs += "FirstResultKey=" + HttpUtility.UrlEncode(this.FirstResultKey) + ",";
            }

            if (!string.IsNullOrEmpty(this.LastResultKey))
            {
                xArgs += "LastResultKey=" + HttpUtility.UrlEncode(this.LastResultKey) + ",";
            }

            if (!string.IsNullOrEmpty(this.Lineup))
            {
                xArgs += "Lineup=" + HttpUtility.UrlEncode(this.Lineup) + ",";
            }

            if (null != this.NumResults && this.NumResults.HasValue)
            {
                xArgs += "NumResults=" + HttpUtility.UrlEncode(this.NumResults.Value.ToString()) + ",";
            }

            if (!string.IsNullOrEmpty(this.PageRequest))
            {
                xArgs += "PageRequest=" + HttpUtility.UrlEncode(this.PageRequest) + ",";
            }

            if (null != this.Search && this.Search.HasValue)
            {
                xArgs += "Search=" + HttpUtility.UrlEncode(this.Search.Value.ToString()) + ",";
            }

            xArgs = xArgs.Substring(0, xArgs.Length - 1);

            return xArgs;
        }
    }

    /// <summary>
    /// This class contains the parameters specific to Gaming Chat sub context.
    /// </summary>
    public class SocialMediaXArgs
    {
        /// <summary>
        /// Initializes a new instance of SocialMediaXArgs.
        /// </summary>
        public SocialMediaXArgs()
        {
        }

        /// <summary>
        /// Gets or sets the value of CommandPrefix.
        /// This parameter customizes the recognition by selecting a preconfigured recognition context.
        /// For some applications, phrases may include common command-like prefixes like “update twitter” or “facebook”.
        /// If you expect end-users to include these prefixes, use the “CommandPrefix=true”.
        /// Default: false
        /// </summary>
        public bool? CommandPrefix { get; set; }

        /// <summary>
        /// Gets or sets the value of BrevityLevel
        /// Control for automatic text abbreviation behavior. This setting controls abbreviation of the output for things like “LOL”. “0” disables abbreviations. Higher numbers are more aggressive, meaning it will use less common abbreviations. Default: 0
        /// </summary>
        public int? BrevityLevel { get; set; }

        /// <summary>
        /// Gets or sets the value of SocialSites.
        /// Use the common names of the social media sites your application supports to provide hints for better recognition results. 
        /// </summary>
        public string SocialSites { get; set; }

        /// <summary>
        /// Returns the string format of the properties.
        /// </summary>
        /// <returns>Returns the string format of the properties.</returns>
        public override string ToString()
        {
            string xArgs = string.Empty;

            if (null != this.CommandPrefix && this.CommandPrefix.HasValue)
            {
                xArgs += "CommandPrefix=" + HttpUtility.UrlEncode(this.CommandPrefix.Value.ToString()) + ",";
            }

            if (null != this.BrevityLevel && this.BrevityLevel.HasValue)
            {
                xArgs += "BrevityLevel=" + HttpUtility.UrlEncode(this.BrevityLevel.Value.ToString()) + ",";
            }

            if (!string.IsNullOrEmpty(this.SocialSites))
            {
                xArgs += "SocialSites=" + HttpUtility.UrlEncode(this.SocialSites) + ",";
            }

            xArgs = xArgs.Substring(0, xArgs.Length - 1);
            return xArgs;
        }
    }
}
