// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="TextToSpeechMethods.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

/*! \page TextToSpeechService Text To Speech Service Cookbook
 * \section t2s_overview Overview
 * This cookbook shows you how to develop a Text To Speech Service application using the Platform SDK for Microsoft.
 * \n
 * The Platform SDK for Microsoft provides the following method: 
 * <ul>
 * <li>
 * Text To Speech
 * </li>
 * </ul>
  * To use these methods in an application, perform the following steps:
 * <p>
 * 1. Add a reference to the SDK as shown in the \link cookbooks_overview About the Cookbooks \endlink section and import the ATT_MSSDK.TextToSpeechv1 namespace.
 * \n
 * 2. Create an instance of RequestFactory with the scope type RequestFactory.ScopeTypes.TTS, as shown in the \link cookbooks_overview About the Cookbooks \endlink section.
 * \n
 * 3. Get and set client credential access token in Requestfactory instance as shown in the \link cookbooks_overview About the Cookbooks \endlink section
 * \n
 * 4. Invoke the Text To Speech Service method using the RequestFactory instance.
 * </p>
 * \section t2speech_send Converting Text to Speech
 * To convert text to speech, invoke the TextToSpeech method using the RequestFactory instance by passing text to be converted along with optional content type, content language, return audio format and X-Args, as shown in the following code example.
 * \code
 * // Text to be converted to speech audio. 
 * // Must be of type specified in the Content-Type header.
 * string textToConvert = "xxxxxxxxxxxx";
 * 
 * // Must be defined as one of the following values:
 * //    text/plain
 * //    application/ssml+xml
 * string contentType = "text/plain";
 * 
 * // US English
 * string contentLanguage = "en-US";
 * TextToSpeechResponse response = this.requestFactory.TextToSpeech(textToConvert, contentType, contentType);
 * \endcode
 */
namespace ATT_UNITY_SDK
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using ATT_UNITY_SDK.TextToSpeechv1;
    using System.Text;

    /** \addtogroup TTS Text To Speech Methods
        * @{
        */

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {
        /// <summary>
        /// This method converts text to synthesized voice.
        /// </summary>
        /// <param name="textToConvert">Text to be converted to speech audio. Must be of type specified in the Content-Type header.</param>
        /// <param name="contentType">Must be defined as one of the following values: text/plain and application/ssml+xml</param>
        /// <param name="contentLanguage">May be defined as one of the following values: 
        /// ‘en-US’ – US English
        /// ‘es-US’ – US Spanish
        /// Default value is ‘en-US’.</param>
        /// <param name="returnAudioFormat">Determines the format of the body of the response. Valid values are: 
        /// audio/amr
        /// audio/amr-wb
        /// audio/x-wav
        /// The default format is audio/amr-wb. </param>
        /// <param name="xArgs">A meta parameter to define multiple parameters within a single HTTP header.</param>
        /// <returns><see cref="ATT_MSSDK.TextToSpeechv1.TextToSpeechResponse"/> object containing the text to speech response.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include TTS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public TextToSpeechResponse TextToSpeech(string textToConvert, string contentType, string contentLanguage, string returnAudioFormat,string xArgs)
        {

            if (!this.Scopes.Contains(ScopeTypes.TTS))
            {
                throw new InvalidScopeException("Missing TTS Scope");
            }

            if (string.IsNullOrEmpty(textToConvert))
            {
                throw new ArgumentNullException("Missing textToConvert value");
            }

            this.GetClientCredentials();

            string relativeUrl = string.Format("speech/v3/textToSpeech");

            NameValueCollection parameters = new NameValueCollection();

            string accept = returnAudioFormat;
            if (string.IsNullOrEmpty(accept))
            {
                accept = "audio/amr-wb";
            }

            parameters.Add("Authorization", "Bearer " + this.ClientCredential.AccessToken);
            
            if (!string.IsNullOrEmpty(contentLanguage))
            {
                parameters.Add("Content-Language", contentLanguage);
            }

            if (!string.IsNullOrEmpty(xArgs))
            {
                parameters.Add("X-Arg", xArgs);
            }

             byte[] postBytes = null;

            if (!string.IsNullOrEmpty(textToConvert))
            {
                UTF8Encoding encoding = new UTF8Encoding();

                postBytes = encoding.GetBytes(textToConvert);
            }

            HttpWebResponse webresponse = (HttpWebResponse)this.Http.Send(HTTPMethods.POST, relativeUrl, parameters, postBytes, contentType, accept, true);
            TextToSpeechResponse response = new TextToSpeechResponse();
            int offset = 0;
            int remaining = Convert.ToInt32(webresponse.ContentLength);
            response.ContentLength = webresponse.ContentLength;
            response.ContentType = webresponse.ContentType;
            using (var stream = webresponse.GetResponseStream())
            {
                var bytes = new byte[webresponse.ContentLength];
                while (remaining > 0)
                {
                    int read = stream.Read(bytes, offset, remaining);
                    if (read <= 0)
                    {
                        throw new InvalidResponseException(String.Format("End of stream reached with {0} bytes left to read", remaining));
                    }

                    remaining -= read;
                    offset += read;
                }

                string[] splitData = System.Text.RegularExpressions.Regex.Split(webresponse.ContentType.ToLower(), ";");
                response.ContentType = splitData[0];
                response.SpeechContent = bytes;
            }

            return response;
        }
    }
    /*! }@ */
}
