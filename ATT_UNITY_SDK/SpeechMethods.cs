// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="SpeechMethods.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

/*! \page SpeechToTextService Speech Service Cookbook
 * \section speech_overview Overview
 * This cookbook shows you how to develop a Speech Service application using the Platform SDK for Microsoft.
 * \n
 * The Platform SDK for Microsoft provides the following methods:
 * <ul>
 * <li>
 * SpeechToText
 * </li>
 * </ul>
 * To use these methods in an application, perform the following steps:
* <p>
 * 1. Add a reference to the SDK as shown in the \link cookbooks_overview About the Cookbooks \endlink section and import the ATT_MSSDK.Speechv3 namespace.
 * \n
 * 2. Create an instance of \link ATT_MSSDK.RequestFactory RequestFactory \endlink with the scope type RequestFactory.ScopeTypes.Speech, as shown in the \link cookbooks_overview About the Cookbooks \endlink section.
 * \n
 * 3. Get and set client credential access token in Requestfactory instance as shown in the \link cookbooks_overview About the Cookbooks \endlink section
 * \n
 * 4. Invoke the Speech Service methods using the \link ATT_MSSDK.RequestFactory RequestFactory \endlink instance.
 * </p>
 * \section speech_send Converting Spoken Audio to Text
 * To convert the spoken audio to text, invoke the \link ATT_MSSDK.RequestFactory.SpeechToText SpeechToText \endlink method using the \link ATT_MSSDK.RequestFactory RequestFactory \endlink instance by passing an audio file name and the context to be applied for Speech to Text conversion as shown in the following code example.
 * \code
 * // Path of the audio file which needs to convert to text
 * string fileToUpload = "xxxxxxxxx\\xxxxxxxx\\xxxxx"; 
 * SpeechResponse response = this.requestFactory.SpeechToText(fileToUpload, XSpeechContext.Generic);
 * \endcode
 */
namespace ATT_UNITY_SDK
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;
    using ATT_UNITY_SDK.Speechv3;

    /// <summary>
    /// X-Speech Context, the speech context to be applied to the transcribed text. X-Speech Context Enum has the values - 
    /// Generic, 
    /// TV, 
    /// BusinessSearch, 
    /// WebSearch, 
    /// SMS, 
    /// VoiceMail, 
    /// QuestionAndAnswer.
    /// </summary>
    public enum XSpeechContext
    {
        /// <summary>
        /// Default Generic
        /// </summary>
        Generic,

        /// <summary>
        /// X-Speech Context: UVerseEPG
        /// </summary>
        TV,

        /// <summary>
        /// X-Speech Context: BusinessSearch
        /// </summary>
        BusinessSearch,

        /// <summary>
        /// X-Speech Context: WebSearch
        /// </summary>
        WebSearch,

        /// <summary>
        /// X-Speech Context: SMS
        /// </summary>
        SMS,

        /// <summary>
        /// X-Speech Context: VoiceMail
        /// </summary>
        VoiceMail,

        /// <summary>
        /// X-Speech Context: QuestionAndAnswer
        /// </summary>
        QuestionAndAnswer,

        /// <summary>
        /// X-Speech Context: Gaming
        /// </summary>
        Gaming,

        /// <summary>
        /// X-Speech Context: SocialMedia
        /// </summary>
        SocialMedia
    }

    /** \addtogroup Speech Speech Methods
    * @{
    */
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {
        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SPEECH.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath)
        {
			return this.ConvertToText (audioFilePath, null, null, null, null, null);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext">X-Speech Context</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XSpeechContext speechContext)
        {
            return this.ConvertToText(audioFilePath, null, speechContext, null, null, null);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="xArgsCollection">X-Arg Collection, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, NameValueCollection xArgsCollection)
        {
            return this.ConvertToText(audioFilePath, null, null, null, xArgsCollection, null);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext"> X-Speech Context</param>
        /// <param name="xArgsCollection">X-Arg Collection, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XSpeechContext speechContext, NameValueCollection xArgsCollection)
        {
            return this.ConvertToText(audioFilePath,null, speechContext, null, xArgsCollection, null);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext"> X-Speech Context</param>
        /// <param name="xArgs">The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <param name="contentLanguage">For Generic Context only. Specifies the language of the submitted audio with one of the following two choices: ‘en-US’ (English - United States) and ‘es-US’ (Spanish - United States).</param>
        /// <param name="speechSubContext">Only used when X-SpeechContext is set to Gaming.</param>
        /// <param name="audioContentType">Custom audio content type. e.g. audio/wav</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XSpeechContext speechContext, string xArgs, string contentLanguage, string speechSubContext, string audioContentType)
        {
            if (!this.Scopes.Contains(ScopeTypes.Speech))
            {
                throw new InvalidScopeException("Missing Speech Scope");
            }

            if (string.IsNullOrEmpty(audioFilePath) || !File.Exists(audioFilePath.ToString()))
            {
                throw new ArgumentException("Invalid Audio File path");
            }

            this.GetClientCredentials();

            
            string contentType = audioContentType;
            if (string.IsNullOrEmpty(contentType))
            {
                string audioFileExtension = Path.GetExtension(audioFilePath.ToString());
                contentType = MapContentTypeFromExtension(audioFileExtension);
            }

            FileStream audioFileStream = new FileStream(audioFilePath.ToString(), FileMode.Open, FileAccess.Read);
            BinaryReader audioBinaryReader = new BinaryReader(audioFileStream);
            byte[] image = audioBinaryReader.ReadBytes((int)audioFileStream.Length);
            audioBinaryReader.Close();
            audioFileStream.Close();
            int dataToSendSize = image.Length;
            var MemoryStream = new MemoryStream(new byte[dataToSendSize], 0, dataToSendSize, true, true);
            MemoryStream.Write(image, 0, image.Length);
            byte[] finalpostBytes = MemoryStream.GetBuffer();
            
            string path = string.Format("speech/v3/speechToText");
            
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("Authorization", "Bearer " + this.ClientCredential.AccessToken);
            parameters.Add("X-SpeechContext", speechContext.ToString());

            if (!string.IsNullOrEmpty(speechSubContext))
            {
                parameters.Add("X-SpeechSubContext", speechSubContext);
            }

            if (!string.IsNullOrEmpty(contentLanguage))
            {
                parameters.Add("Content-Language", contentLanguage);
            }

            if (!string.IsNullOrEmpty(xArgs))
            {
                parameters.Add("X-Arg", xArgs);
            }

            string resp = this.Http.Send(path, parameters, finalpostBytes, contentType, "application/json");
            return SpeechResponse.Parse(resp);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="xArgs">XArgs class, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XArgs xArgs)
        {
            return this.ConvertToText(audioFilePath, null, null, null, null, xArgs);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext"> X-Speech Context</param>
        /// <param name="xArgs">XArgs class, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XSpeechContext speechContext, XArgs xArgs)
        {
            return this.ConvertToText(audioFilePath, null, speechContext, null, null, xArgs);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext"> X-Speech Context</param>
        /// <param name="xArgs">XArgs class, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <param name="xSpeechSubContext">Only used when X-SpeechContext is set to Gaming.</param>
        /// <param name="audioContentType">Custom audio content type. e.g. audio/wav</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, XSpeechContext speechContext, XArgs xArgs, string xSpeechSubContext, string audioContentType)
        {
            if (!this.Scopes.Contains(ScopeTypes.Speech))
            {
                throw new InvalidScopeException("Missing Speech Scope");
            }

            if (string.IsNullOrEmpty(audioFilePath) || !File.Exists(audioFilePath.ToString()))
            {
                throw new ArgumentException("Invalid Audio File path");
            }

            this.GetClientCredentials();


            string contentType = audioContentType;
            if (string.IsNullOrEmpty(contentType))
            {
                string audioFileExtension = Path.GetExtension(audioFilePath.ToString());
                contentType = MapContentTypeFromExtension(audioFileExtension);
            }

            FileStream audioFileStream = new FileStream(audioFilePath.ToString(), FileMode.Open, FileAccess.Read);
            BinaryReader audioBinaryReader = new BinaryReader(audioFileStream);
            byte[] image = audioBinaryReader.ReadBytes((int)audioFileStream.Length);
            audioBinaryReader.Close();
            audioFileStream.Close();
            int dataToSendSize = image.Length;
            var MemoryStream = new MemoryStream(new byte[dataToSendSize], 0, dataToSendSize, true, true);
            MemoryStream.Write(image, 0, image.Length);
            byte[] finalpostBytes = MemoryStream.GetBuffer();

            string path = string.Format("speech/v3/speechToText");

            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("Authorization", "Bearer " + this.ClientCredential.AccessToken);
            parameters.Add("X-SpeechContext", speechContext.ToString());

            if (!string.IsNullOrEmpty(xSpeechSubContext))
            {
                parameters.Add("X-SpeechSubContext", xSpeechSubContext);
            }

            if (null == xArgs)
            {
                xArgs = new XArgs();
            }

            xArgs.ClientSdk = "MSServerSDK_3_0_Speechv3";
            parameters.Add("X-Arg", xArgs.ToString());
            string resp = this.Http.Send(path, parameters, finalpostBytes, contentType, "application/json");
            return SpeechResponse.Parse(resp);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext">Speech Context</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, string speechContext)
        {
            return this.ConvertToText(audioFilePath, speechContext, null, null, null, null);
        }

        /// <summary>
        /// This method converts spoken audio to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext">Speech Context</param>
        /// <param name="xArgsParameter">X-Arg Parameter, The X-Arg header field is a meta parameter 
        /// that can be used to define multiple parameter name/value pairs. 
        /// The preferred way to provide these parameters is as a set of name/value pair strings in the format:
        /// X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> An Speech response object containing the results.</returns>
        /// <exception cref="ArgumentException">Thrown if an argument is invalid.</exception>
        /// <exception cref="InvalidScopeException">Thrown if the Request Factory scope does not include SMS.</exception>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        public SpeechResponse SpeechToText(string audioFilePath, string speechContext, string xArgsParameter)
        {
            return this.ConvertToText(audioFilePath, speechContext, null, xArgsParameter, null, null);
        }

        //! @cond SKIP_THIS

        /// <summary>
        /// Converts given spoken audio file to text.
        /// </summary>
        /// <param name="audioFilePath">Audio file path</param>
        /// <param name="speechContext">Speech Context</param>
        /// <param name="xSpeechContext">X-Speech Context, values from the enum X-SpeechContext</param>
        /// <param name="xArgsParameter">string value, X-Arg Parameter, The X-Arg header field is a meta parameter that can be used to define multiple parameter name/value pairs. The preferred way to provide these parameters is as a set of name/value pair strings in the format: X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <param name="xArgsCollection">NameValueCollection, X-Arg Collection, The X-Arg header field is a meta parameter that can be used to define multiple parameter name/value pairs. The preferred way to provide these parameters is as a set of name/value pair strings in the format: X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <param name="xArgs">XArgs object, The X-Arg header field is a meta parameter that can be used to define multiple parameter name/value pairs. The preferred way to provide these parameters is as a set of name/value pair strings in the format: X-Arg: param1=value1,param2=value2,param3=value3</param>
        /// <returns><see cref="ATT_MSSDK.Speechv3.SpeechResponse"/> object containing the results.</returns>
        private SpeechResponse ConvertToText(string audioFilePath, string speechContext, XSpeechContext? xSpeechContext, string xArgsParameter, NameValueCollection xArgsCollection, XArgs xArgs)
        {
            if (!this.Scopes.Contains(ScopeTypes.Speech))
            {
                throw new InvalidScopeException("Missing Speech Scope");
            }

            if (string.IsNullOrEmpty(audioFilePath) || !File.Exists(audioFilePath.ToString()))
            {
                throw new ArgumentException("Invalid Audio File path");
            }

            this.GetClientCredentials();

            string audioFileExtension = Path.GetExtension(audioFilePath.ToString());
            string audioContentType = MapContentTypeFromExtension(audioFileExtension);
            FileStream audioFileStream = new FileStream(audioFilePath.ToString(), FileMode.Open, FileAccess.Read);
            BinaryReader audioBinaryReader = new BinaryReader(audioFileStream);
            byte[] image = audioBinaryReader.ReadBytes((int)audioFileStream.Length);
            audioBinaryReader.Close();
            audioFileStream.Close();
            int dataToSendSize = image.Length;
            var MemoryStream = new MemoryStream(new byte[dataToSendSize], 0, dataToSendSize, true, true);
            MemoryStream.Write(image, 0, image.Length);
            byte[] finalpostBytes = MemoryStream.GetBuffer();
            string path = string.Format("speech/v3/speechToText");
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("Authorization", "Bearer " + this.ClientCredential.AccessToken);
            if (!string.IsNullOrEmpty(speechContext))
            {
                parameters.Add("X-SpeechContext", speechContext);
            }
            else if (null != xSpeechContext && xSpeechContext.HasValue)
            {
                parameters.Add("X-SpeechContext", xSpeechContext.Value.ToString());
            }

            string xArgsData = string.Empty;
            if (!string.IsNullOrEmpty(xArgsParameter))
            {
                string[] xArgsValues = xArgsParameter.Split(',');
                foreach (string xArgValue in xArgsValues)
                {
                    string[] xArg = xArgValue.Split('=');
                    if (null != xArg && xArg.Length > 1)
                    {
                        string namevalue = string.Empty;
                        if (!string.IsNullOrEmpty(xArg[0]) && xArg[0].ToLower().Equals("clientsdk"))
                        {
                            namevalue = "ClientSdk=MSServerSDK_3_0_Speechv3,";
                        }
                        else
                        {
                            namevalue = xArg[0] + "=" + HttpUtility.UrlEncode(xArg[1]) + ",";
                        }

                        xArgsData += namevalue;
                    }
                }

                xArgsData = xArgsData.Substring(0, xArgsData.Length - 1);
                parameters.Add("X-Arg", xArgsData);
            }
            else if (null != xArgsCollection)
            {   
                if (xArgsCollection != null)
                {
                    foreach (String keystring in xArgsCollection.AllKeys)
                    {
                        string namevalue = string.Empty;
                        if (!string.IsNullOrEmpty(keystring) && keystring.ToLower().Equals("clientsdk"))
                        {
                            namevalue = "ClientSdk=MSServerSDK_3_0_Speechv3,";
                        }
                        else
                        {
                            namevalue = keystring + "=" + HttpUtility.UrlEncode(xArgsCollection[keystring]) + ",";
                        }

                        xArgsData += namevalue;
                    }

                    xArgsData = xArgsData.Substring(0, xArgsData.Length - 1);
                    parameters.Add("X-Arg", xArgsData.ToString());
                }
            }
            else if (null != xArgs)
            {
                xArgs.ClientSdk = "MSServerSDK_3_0_Speechv3";
                parameters.Add("X-Arg", xArgs.ToString());
            }
            else
            {
                xArgs = new XArgs();
                xArgs.ClientSdk = "MSServerSDK_3_0_Speechv3";
                parameters.Add("X-Arg", xArgs.ToString());
            }

            string resp = this.Http.Send(path, parameters, finalpostBytes, audioContentType, "application/json");
            return SpeechResponse.Parse(resp);
        }

        //! @endcond
    }
    /*! }@ */
}
