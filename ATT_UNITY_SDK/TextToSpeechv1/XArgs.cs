// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="XArgs.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

using System.Web;
namespace ATT_UNITY_SDK.TextToSpeechv1
{
    /// <summary>
    /// The X-Arg header field is a meta parameter that can be used to define multiple parameter name/value pairs. 
    /// </summary>
    public class XArgs
    {
        /// <summary>
        /// Gets or sets AppId.
        /// An opaque token representing the client application.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets OrgId.
        /// An opaque token representing the developer organization of the client application.
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// Gets or sets VoiceName.
        /// May be defined as one of the following values:
        ///	‘crystal’ F en-US
        ///	‘mike’ M en-US
        ///	‘rosa’ F es-US
        ///	‘alberto’ M es-US
        /// The default value is ‘crystal’.
        /// </summary>
        public string VoiceName { get; set; }

        /// <summary>
        /// Gets or sets ClientApp.
        /// Application Name
        /// </summary>
        public string ClientApp { get; set; }

        /// <summary>
        /// Gets or sets ClientVersion.
        /// Version ID of the App.
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// Gets or sets ClientScreen.
        /// A string representing the application's focus UI screen/widget for this request.
        /// </summary>
        public string ClientScreen { get; set; }

        /// <summary>
        /// Gets or sets Volume.
        /// May be defined in the following range:	[0, 500]. 100 is the default volume.
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// Gets or sets Tempo.
        /// May be defined in the following range:[-18, 18].
        /// 0 is the default rate of word output.
        /// Lower (negative) values produce fewer words per second, higher values produce more words per second. This parameter is not related to the output audio sample rate 
        /// </summary>
        public int? Tempo { get; set; }

        /// <summary>
        /// Returns the string representation of X-Arg members
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string xArgs = string.Empty;

            if (!string.IsNullOrEmpty(this.AppId))
            {
                xArgs = "AppId=" + HttpUtility.UrlEncode(this.AppId) + ",";
            }

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

            if (!string.IsNullOrEmpty(this.OrgId))
            {
                xArgs += "OrgId=" + HttpUtility.UrlEncode(this.OrgId) + ",";
            }

            if (this.Tempo.HasValue)
            {
                xArgs += "Tempo=" + HttpUtility.UrlEncode(this.Tempo.Value.ToString()) + ",";
            }

            if (!string.IsNullOrEmpty(this.VoiceName))
            {
                xArgs += "VoiceName=" + HttpUtility.UrlEncode(this.VoiceName) + ",";
            }

            if (this.Volume.HasValue)
            {
                xArgs += "Volume=" + HttpUtility.UrlEncode(this.Volume.Value.ToString()) + ",";
            }

            xArgs = xArgs.Substring(0, xArgs.Length - 1);

            return xArgs;
        }
    }
}
