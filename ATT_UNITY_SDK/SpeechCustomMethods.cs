// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="SpeechMethods.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

/*! \page SpeechToTextCustomService Speech To Text Custom Service Cookbook
 * \section CustomSpeech_overview Overview
 * This cookbook shows you how to develop a Custom Speech Service application using the Platform SDK for Microsoft.
 * \n
 * The Platform SDK for Microsoft provides the following methods:
 * <ul>
 * <li>
 * SpeechToTextCustom
 * </li>
 * </ul>
 * To use these methods in an application, perform the following steps:
* <p>
 * 1. Add a reference to the SDK as shown in the \link cookbooks_overview About the Cookbooks \endlink section and import the ATT_MSSDK.Speechv3 namespace.
 * \n
 * 2. Create an instance of \link ATT_MSSDK.RequestFactory RequestFactory \endlink with the scope type RequestFactory.ScopeTypes.STTC, as shown in the \link cookbooks_overview About the Cookbooks \endlink section.
 * \n
 * 3. Get and set client credential access token in Requestfactory instance as shown in the \link cookbooks_overview About the Cookbooks \endlink section
 * \n
 * 4. 3. Invoke the Speech Service methods using the \link ATT_MSSDK.RequestFactory RequestFactory \endlink instance.
 * </p>
 * \section customSpeech_send Converting Spoken Audio to Text
 * To convert the spoken audio to text, invoke the \link ATT_MSSDK.RequestFactory.SpeechToTextCustom \endlink method 
 * using the \link ATT_MSSDK.RequestFactory RequestFactory \endlink instance by passing an audio file location and 
 * the context to be applied for Speech to Text conversion along with optional dictionary and grammar file paths as shown in the following code example.
 * \code
 * // Path of the audio file to convert to text
 * string fileToConvert = "xxxxxxxxx\\xxxxxxxx\\xxxxx"; 
 * string xArgs = "xxxxxxxx"; // example ClientApp=Test
 * string audioContentType = "xxxxxxx"; // example "audio/wav";
 * SpeechResponse response = this.requestFactory.SpeechToTextCustom(fileToConvert, dictionaryFile, grammarFile, XSpeechContext.GenericHints);
 * \endcode
 */
using System.Collections.Generic;


namespace ATT_UNITY_SDK
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;
    using System.Text;
    using ATT_UNITY_SDK.Speechv3;

    /// <summary>
    /// X-Speech Custom Context , the speech context to be applied to the transcribed text when inline hints and inline grammar are passed along with audio file.
    /// X-Speech Custom Context Enum has the values - 
    /// GenericHints, 
    /// GrammarList
    /// </summary>
    public enum XSpeechCustomContext
    {
        /// <summary>
        /// X-Speech Custom Context: context for Inline hints
        /// </summary>
        GenericHints,

        /// <summary>
        /// X-Speech Custom Context: context for Inline grammar
        /// </summary>
        GrammarList

    }

    /** \addtogroup SpeechToTextCustom SpeechToTextCustom Methods
    * @{
    */
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {

        /// <summary>
        /// This method converts spoken audio to text. Optional inline hints and inline grammar may be provided by the developer to get more
        /// accurate results with a known data set.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="dictionaryFilePath">Inline hint file path</param>
        /// <param name="grammarFilePath">Inline grammar file path</param>
        /// <param name="speechCustomContext"> XSpeechCustomContext </param>
        /// <param name="xArgs"> String having name/value pairs separated by comma, used to populate X-Arg header field is a meta parameter </param>
        /// <param name="audioContentType">Specifies the content type of the audio submitted.</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToTextCustom (string audioFilePath, string dictionaryFilePath, string grammarFilePath,
            XSpeechCustomContext speechCustomContext, string xArgs, string audioContentType)
        {
            if (!this.Scopes.Contains (ScopeTypes.STTC)) {
                throw new InvalidScopeException ("Missing Speech Scope");
            }

            if (string.IsNullOrEmpty (audioFilePath) || !File.Exists (audioFilePath.ToString ())) {
                throw new ArgumentException ("Invalid Audio File path");
            }

            this.GetClientCredentials ();

            string xdictionaryContent = string.Empty;
            string xgrammerContent = string.Empty;

            if (!string.IsNullOrEmpty (dictionaryFilePath)) {
                if (File.Exists (dictionaryFilePath)) {
                    StreamReader streamReader = new StreamReader (dictionaryFilePath);
                    xdictionaryContent = streamReader.ReadToEnd ();
                    streamReader.Close ();
                } else {
                    throw new ArgumentException (String.Format ("dictionary file not found on path {0}", dictionaryFilePath), "dictionaryFilePath");
                }
            }

            if (!string.IsNullOrEmpty (grammarFilePath)) {
                if (File.Exists (grammarFilePath)) {
                    StreamReader streamReader = new StreamReader (grammarFilePath);
                    xgrammerContent = streamReader.ReadToEnd ();
                    streamReader.Close ();
                } else {
                    throw new ArgumentException (String.Format ("grammar file not found on path {0}", grammarFilePath), "grammarFilePath");
                }
            }

            FileStream audioFileStream = new FileStream (audioFilePath.ToString (), FileMode.Open, FileAccess.Read);
            BinaryReader audioBinaryReader = new BinaryReader (audioFileStream);
            byte[] audioData = audioBinaryReader.ReadBytes ((int)audioFileStream.Length);
            audioBinaryReader.Close ();
            audioFileStream.Close ();

            string path = string.Format ("speech/v3/speechToTextCustom");
            NameValueCollection parameters = new NameValueCollection ();

            string boundary = "------------------" + DateTime.Now.Ticks.ToString ("x");

            parameters.Add ("Authorization", "Bearer " + this.ClientCredential.AccessToken);
            parameters.Add ("X-SpeechContext", speechCustomContext.ToString ());

            if (!string.IsNullOrEmpty (xArgs)) {
                parameters.Add ("X-Arg", xArgs);
            }

            string mainContentType = "multipart/x-srgs-audio; boundary=" + boundary;

            if (string.IsNullOrEmpty (audioContentType)) {
                string audioFileExtension = Path.GetExtension (audioFilePath);
                audioContentType = MapContentTypeFromExtension (audioFileExtension);
            }

            string requestData = string.Empty;

            if (!string.IsNullOrEmpty (xdictionaryContent)) {
                requestData += "--" + boundary + "\r\n";

                requestData += "Content-Disposition: form-data; name=\"x-dictionary\"; filename=\"speech_alpha.pls\"\r\nContent-Type: application/pls+xml\r\n";

                requestData += "\r\n" + xdictionaryContent + "\r\n\r\n\r\n";
            }

            if (!string.IsNullOrEmpty (xgrammerContent)) {
                requestData += "--" + boundary + "\r\n";
                requestData += "Content-Disposition: form-data; name=\"x-grammar\"; filename=\"prefix.srgs\" ";
                requestData += "\r\nContent-Type: application/srgs+xml \r\n" + "\r\n" + xgrammerContent + "\r\n\r\n\r\n";
            }

            requestData += "--" + boundary + "\r\n";
            requestData += "Content-Disposition: form-data; name=\"x-voice\"; filename=\"" + Path.GetFileName (audioFilePath) + "\"";
            requestData += "\r\nContent-Type: " + audioContentType + "\r\n\r\n";

            UTF8Encoding encoding = new UTF8Encoding ();
            byte[] contentHeaders = encoding.GetBytes (requestData);
            int newSize = contentHeaders.Length + audioData.Length;

            var memoryStream = new MemoryStream (new byte[newSize], 0, newSize, true, true);
            memoryStream.Write (contentHeaders, 0, contentHeaders.Length);
            memoryStream.Write (audioData, 0, audioData.Length);

            byte[] postBytes = memoryStream.GetBuffer ();

            byte[] byteLastBoundary = encoding.GetBytes ("\r\n\r\n" + "--" + boundary + "--");
            int totalSize = postBytes.Length + byteLastBoundary.Length;

            var totalMS = new MemoryStream (new byte[totalSize], 0, totalSize, true, true);
            totalMS.Write (postBytes, 0, postBytes.Length);
            totalMS.Write (byteLastBoundary, 0, byteLastBoundary.Length);

            byte[] finalpostBytes = totalMS.GetBuffer ();

            string resp = this.Http.Send (path, parameters, finalpostBytes, mainContentType, "application/json");
            return SpeechResponse.Parse (resp);
        }

        /// <summary>
        /// Gets content type based on the file extension.
        /// </summary>
        /// <param name="extension">file extension</param>
        /// <returns>the Content type mapped to the extension</returns>
        private static string MapContentTypeFromExtension (string extension)
        {
            Dictionary<string, string> extensionToContentTypeMapping = new Dictionary<string, string> ()
            {
                { ".jpg", "image/jpeg" }, { ".bmp", "image/bmp" }, { ".mp3", "audio/mp3" },
                { ".m4a", "audio/m4a" }, { ".gif", "image/gif" }, { ".3gp", "video/3gpp" },
                { ".3g2", "video/3gpp2" }, { ".wmv", "video/x-ms-wmv" }, { ".m4v", "video/x-m4v" },
                { ".amr", "audio/amr" }, { ".mp4", "video/mp4" }, { ".avi", "video/x-msvideo" },
                { ".mov", "video/quicktime" }, { ".mpeg", "video/mpeg" }, { ".wav", "audio/wav" },
                { ".aiff", "audio/x-aiff" }, { ".aifc", "audio/x-aifc" }, { ".midi", ".midi" },
                { ".au", "audio/basic" }, { ".xwd", "image/x-xwindowdump" }, { ".png", "image/png" },
                { ".tiff", "image/tiff" }, { ".ief", "image/ief" }, { ".txt", "text/plain" },
                { ".html", "text/html" }, { ".vcf", "text/x-vcard" }, { ".vcs", "text/x-vcalendar" },
                { ".mid", "application/x-midi" }, { ".imy", "audio/iMelody" },
                {".awb", "audio/amr-wb"}, {".spx", "audio/x-speex"}
            };
            if (extensionToContentTypeMapping.ContainsKey (extension)) {
                return extensionToContentTypeMapping [extension];
            } else {
                throw new ArgumentException ("invalid attachment extension");
            }
        }
    }
    /*! }@ */
}
