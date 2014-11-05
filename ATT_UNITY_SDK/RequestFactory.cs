// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="RequestFactory.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

namespace ATT_UNITY_SDK
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /*!
     * 
     * \namespace ATT_UNITY_SDK
     * \brief Core namespace for ATT MSSDK
     * \namespace ATT_UNITY_SDK.MIMv1
     * \brief Core namespace for In-App Messaging Service for fetching message details
     * \namespace ATT_UNITY_SDK.MOBOv1
     * \brief Core namespace for In-App Messaging Service for sending message
     *  \namespace ATT_UNITY_SDK.InAppMessaging
     * \brief Core namespace for In-App Messaging Service for sending and fetching messages
     * \namespace ATT_UNITY_SDK.MMSv3
     * \brief Core namespace for Multimedia Messaging Service Version 3
     * \namespace ATT_UNITY_SDK.IMMNv1
     * \brief Core namespace for In-App Messaging Service for sending messages    
     * \namespace ATT_UNITY_SDK.SMSv3
     * \brief Core namespace for Short Message Service Version 2
     * \namespace ATT_UNITY_SDK.Speechv3
     * \brief Core namespace for Speech Service Version 3
     * \namespace ATT_UNITY_SDK.DeviceCapabilitiesv2
     * \brief Core namespace for Device %Capabilities Service Version 2
     * \namespace ATT_UNITY_SDK.Advertisementv1
     * \brief Core namespace for Advertising Service Version 1
     * \namespace ATT_UNITY_SDK.TextToSpeechv1
     * \brief Core namespace for Text To Speech Service Version 1
     */

    /*!
     * \mainpage
     * <table style="border: 1px dotted #D5D5D5; float: left; width:100%;">
     * 			<tr style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
     *          <td colspan="2" style="font-size: 12pt; font-weight:bold !important; COLOR: white;">AT&amp;T API Platform APIs 
     *			</td>
     *							</tr>
     *							<tr>
     *								<td class="floatLeft marTop-10px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref SMS "SMS"</b><br/>
     *									<span>The Short Message Service (SMS) API enables your application to reliably send and receive secure text messages to mobile devices on the AT&amp;T network.
     *									<ul>
     *										<li>Send SMS</li>
     *										<li>Get SMS Status</li>
     *										<li>Receive SMS</li>
     *										<li>Get SMS</li>
     *										<li>Receive SMS Delivery Status</li>
     *									</ul>	
     *									</span>

     *								<td class="floatRight marTop-10px marLeft-30px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref MMS "MMS"</b><br/>
     *									<span>The Multimedia Messaging Service (MMS) API enables your application to reliably send and receive messages that include video, images, and audio, in addition to text, and get the delivery status of sent messages. 
     *									<ul>
     *										<li>Send MMS</li>
     *										<li>Get MMS Status</li>
     *										<li>Receive MMS</li>
     *										<li>Receive MMS Delivery Status</li>
     *									</ul>	
     *									</span>
     *								</td>
     *							</tr>
                
     *							<tr>
     *								<td class="floatLeft marTop-10px" style="width: 45%;" valign="top">
     *								    <b style="color: #ff7200;font-size: 11pt;">\ref DC "Device Capabilities"</b><br/>
     *								    <span>The Device Capabilities API enables your application to identify the capabilities of a mobile device on the AT&T network.
     *								        <ul>
     *									        <li>Get Device Capabilities</li>
     *									    </ul>	
     *									</span>
     *								</td>
     *								<td class="floatRight marTop-10px marLeft-30px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref Speech "Speech"</b><br/>
     *									<span>The Speech API enables apps to convert speech (audio) to text using different contexts.
     *									<ul>
     *										<li>Speech To Text</li>
     *									</ul>
     *									</span>
     *								</td>
     *							</tr>

     *                          <tr>                             
     *								<td class="floatLeft marTop-10px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref TTS "Text To Speech"</b><br/>
     *									<span>The Text to Speech API enables your application to convert text to different speech audio formats.
     *									<ul>
     *										<li>Text to Speech</li>
     *									</ul>	
     *									</span>
     *								</td>
     *								<td class="floatLeft marTop-10px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref TTS "Speech To Text Custom"</b><br/>
     *									<span>The Speech To Text Custom API enables your applications to convert speech to text using custom inline grammars or hints. 
     *									<ul>
     *									    <li>Speech To Text Custom</li>
     *									</ul>	
     *									</span>
     *								</td>
     *							</tr>

     *							<tr>
     *								<td class="floatLeft marTop-10px" style="width: 45%;" valign="top">
     *									<b style="color: #ff7200;font-size: 11pt;">\ref IAM "In-App Messaging"</b><br/>
     *									<span>The In-App Messaging API enables your application to send and receive SMS and MMS messages on the behalf of mobile devices for the user.
     *									<ul>
     *										<li>Send Message</li>
     *										<li>Get Message List</li>
     *										<li>Get Message</li>
     *										<li>Get Message Content</li>
     *										<li>Get Message Delta</li>
     *										<li>Update Messages</li>
     *										<li>Update Message</li>
     *										<li>Delete Messages</li>
     *										<li>Delete Message</li>
     *										<li>Create Message Index</li>
     *										<li>Get Message Index Info</li>
     *										<li>Get Notification Connection details</li>
     *									</ul>	
     *									</span>
     *								</td>
     *
     *								<td class="floatRight marTop-10px marLeft-30px" style="width: 45%;" valign="top">
     *									    <b style="color: #ff7200;font-size: 11pt;font-family:Verdana, Geneva, Arial, Helvetica, sans-serif">\ref Advertisement "Advertising"</b><br/>
     *						   			<span>The Advertising API enables your application to support advertisements within the app. The application developer can collect a revenue share of the advertisement. When users click the advertisement in the app, they are redirected to the web page for the advertisement.
     *     									<ul>
     *		        								<li>Get Ads</li>
     *				        					</ul>	
     *						    		</span>
     *							    </td>
     *							</tr>
     *					</table>
     * 
     */
    /*!
     * \page Overview Overview
     * <ul>
     * <li>
     * \subpage GettingStarted
     * </li>
     * </ul>
     */
    /*!
     * \page ServerGuide Server Guide
     * <ul>
     * <li>
     * \subpage installation
     * </li>
     * </ul>
     */
    /*!
     * \page CookBook Cookbooks
     * \n
     * \subpage cookbooks_overview
     * \n
     * \subpage OAuthImplementation
     * \n
     * \subpage SMSService
     * \n
     * \subpage MMSService
     * \n
     * \subpage DCService
     * \n
     * \subpage InAppMessage
     * \n		 
     * \subpage SpeechToTextService
     * \n
     * \subpage AdvertisementsService
     * \n
     * \subpage TextToSpeechService
     * \n
     * \subpage SpeechToTextCustomService
     */

    /*!
     * \page cookbooks_overview About the Cookbooks
     * \section cb_overview Overview
     * The Platform SDK for Microsoft contains a RequestFactory class that provides methods for the AT&amp;T Platform services. Apps create an instance of RequestFactory using parameters that identify both the application and the services the application uses. To use these methods in an application, perform the following steps:
 * <p>
 * 1. Add a reference to the SDK
 * \n
 * 2. Create an instance of RequestFactory
 * \n
 * 3. Get and set access token in  RequestFactory instance
 * \n
 * 4. Invoking platform APIs through RequestFactory instance.
 * </p>
 * The following section explains these steps in more detail.
 * \section cookbook_add_reference1 Step 1. Add a reference to the SDK
 * Add a reference to ATT_MSSDK.dll to the project, and import the ATT_MSSDK namespace.
 * \code
 * using ATT_UNITY_SDK;
 * \endcode
 * \section cookbook_add_reference2 Step 2. Create an instance of RequestFactory
 *   To create an instance of the RequestFactory class that identifies your application and provides access to the services your application uses, 
     *   pass the API Key and Secret Key values that were assigned to your application when you registered it with AT&T and pass the appropriate scope value. For example, for SMS use RequestFactory.ScopeTypes.SMS as shown in the following code snippet.
     *   
     * NOTE: There is an overloaded RequestFactory constructor which takes an additional parameter 'timeout'. This overloaded constructor can be used by the application, if the application wants to set 'HttpWebRequest.Timeout' property for WebRequests to be done by RequestFactory. 
     * In this case, timeout value in milliseconds passed through constructor will be used by RequestFactory to set 'HttpWebRequest.Timeout' property when RequestFactory creates WebRequest.
     * If this constructor is not used then RequestFactory will not set 'HttpWebRequest.Timeout' property during creation of WebRequest, resulting in default behaviour of 'HttpWebRequest.Timeout' property.
 * \code
 * string endPoint = "xxxxxx.xxxxx.xxx";
 * string apiKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";  // APPLICATION key of the application as registered at developer portal.
 * string secretKey = "xxxxxxxxxxxxxxxx";  //Secret Key of the application as registered at developer portal.
 * string redirectURI = "xxxxxx.xxxxxxxx.xxx";
 * List<RequestFactory.ScopeTypes> scopes = new List<RequestFactory.ScopeTypes>();
 * scopes.Add(RequestFactory.ScopeTypes.CallControl);
 * RequestFactory requestFactory = new RequestFactory(endPoint, apiKey, secretKey, scopes, redirectURI, null);
 * \endcode
 * 
 * \section cookbook_add_reference3 Step 3. Get and set access token in  RequestFactory instance
 * \n
 *     For client credential based applications, obtain Client credential based token by invoking GetNewClientCredential().
 * \n
 *     Save the access token in application session
 * \n
 *     Before invoking platform APIs through RequestFactory instance, 
 * \n 
 *        i.Check whether access token saved in the session is expired by invoking method token.IsExpired() on OauthToken object. 
 * \n
 *        ii.If expired refresh the access token by invoking GetRefreshedClientCredential() on requestFactory instance. 
 * \n
 *     Set the access token in RequestFactory instance.
 * \n
 * Refer the below code snippet of client credential based sample application for steps 3.
 * \n
 * \code
 * private void setAccessToken()
      { 
        //Set access token in RequestFactory instance
        if (null != Session["CSSMS_ACCESS_TOKEN"])
        {
            OAuthToken token = (OAuthToken)Session["CSSMS_ACCESS_TOKEN"];
            this.requestFactory.ClientCredential = (OAuthToken)Session["CSSMS_ACCESS_TOKEN"];

            if (token.IsExpired())
            {
                // If Refresh Token is not expired, use the refresh token to get new access token
                if (token.CreationTime > DateTime.Now.AddSeconds(-(TokenConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_TIME)))
                {
                    token = this.requestFactory.GetRefreshedClientCredential();
                    Session["CSSMS_ACCESS_TOKEN"] = token;

                    //Set the token in requestFactory instance
                    this.requestFactory.ClientCredential = token;

                 }
                 else
                 {
                   //Get new token and set in requestFactory instance
                   token = this.requestFactory.GetNewClientCredential();
                   this.requestFactory.ClientCredential = token;
                   Session["CSSMS_ACCESS_TOKEN"] = token;
                 }
             }
          }
          else
          {
                //Get new token and set in requestFactory instance
                OAuthToken token = this.requestFactory.GetNewClientCredential();
                this.requestFactory.ClientCredential = token;
                Session["CSSMS_ACCESS_TOKEN"] = token;
          }
    }
 * \endcode
 *     For Authorization grant based applications, obtain Authorization grant access token can be obtained by redirecting the user to url returned by method GetOAuthRedirect(string customParam = null) and then invoking method GetAuthorizeCredentials(string authorizationCode) by passing query parameter authorizationCode returned by the platform  as a response of redirect.
 * \n
 *     Save the access token in application session
 * \n
 *     Before invoking platform APIs through RequestFactory instance, 
 * \n 
 *        i.Check whether access token saved in the session is expired by invoking method token.IsExpired() on OauthToken object. 
 * \n
 *        ii.If expired refresh the access token by invoking GetRefreshedClientCredential() on requestFactory instance. 
 * \n
 *     Set the access token in RequestFactory instance.
 *
 *     Refer the below code snippet of authorization grant based sample application for steps 3.
 * \n
 * \code
 *     private void GetAuthorization()
    {
        if (null != Session["CSIAM_ACCESS_TOKEN"])
        {
            OAuthToken token = (OAuthToken)Session["CSIAM_ACCESS_TOKEN"];
            //Set the token in requestFactory instance
            this.requestFactory.AuthorizeCredential = (OAuthToken)Session["CSIAM_ACCESS_TOKEN"];

            if (token.IsExpired())
            {
                // If Refresh Token is not expired, use the refresh token to get new access token
                if (token.CreationTime > DateTime.Now.AddSeconds(-(TokenConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_TIME)))
                {
                    token = this.requestFactory.GetRefreshedAuthorizeCredential();
                    Session["CSIAM_ACCESS_TOKEN"] = token;

                    //Set the token in requestFactory instance
                    this.requestFactory.AuthorizeCredential = token;

                }
                else
                {
                    //unset the token in requestFactory instance
                    this.requestFactory.AuthorizeCredential = null;
                    Session["CSIAM_ACCESS_TOKEN"] = null;
                }
            }
        }

        if (this.requestFactory.AuthorizeCredential == null)
        {
            Response.Redirect(this.requestFactory.GetOAuthRedirect(this.customParam).ToString());
        }
    }
 * \endcode
 *
 * Retrieve the code query parameter from the AT&T platform response and then invoke the GetAuthorizeCredetials() on the requestFactory object as the agrument. 
 \code
  In Page Load:
     if (!string.IsNullOrEmpty(Request["code"]))
     {
           //Extract the code query parameter, which is authorization code and store in temporary variable.
           authCode = Request["code"].ToString();
           //Exchange the authorization code to get access token.
           this.requestFactory.GetAuthorizeCredentials(authCode);
           Session["CSIAM_ACCESS_TOKEN"] = this.requestFactory.AuthorizeCredential;
     }
 \endcode
 */

    /*!
     * \page OAuthImplementation Getting started with OAuth API Implementation
     * \section oauth_overview Overview
     * <p>In order to access a protected resource, whether on behalf of a customer or as an application with no customer context, the application must use the OAuth 2.0  2.x protocol to request an access token authorizing the application to use the requested scope.
     * </p>
     * <p>The Platform SDK for Microsoft allows developers to request and store the tokens. OAuth tokens can be managed in an application by performing the following actions depending on the scope/service of the application:
     * </p>
     * <p>
     * <ul>
     *      <li>Add a reference to the SDK as shown in \link cookbooks_overview About the Cookbooks \endlink.</li>
     *      <li>Request OAuth token for the following scenarios:
     *              <ul><li>Applications without customer context</li>
     *              <li>Applications with customer context </li></ul>
     *      </li>
     * </ul>     
     * </p>
     * The following section explains these actions in more detail.
     * \section request_token Request OAuth Token
     *      \subsection request_token_technique_1 For Applications without Customer Context (Client Credentials Model)
     *          If an Application uses services that do not require customer authorization, the application can use the following code to get and use the access token.
     *\code
     * // Create an instance of OAuthToken and invoke GetClientCredentials() of RequestFactory instance.
     * OAuthToken oauthToken = requestFactory.GetClientCredentials();
     * \endcode
     *          This token can be persisted in the database or in local storage for later use and can be assigned to the requestFactory.ClientCredential property.
     *      \subsection request_token_technique_2 For Applications with Customer Context (Authorization Code Model)
     *          If an application were to use services that require customer authorization, perform the following steps.
     *          <p>
     *              1.	Invoke GetOAuthRedirect(string customParam = null) method on requestFactory to get redirectUrl (Url for authorization endpoint of AT&T platform) and get user consent by redirecting the browser to an authorization endpoint.
     * \code
     * // Redirect the browser to get user consent. 
     * Response.Redirect(requestFactory.GetOAuthRedirect(customParam));
     * \endcode
     *              \n
     *              2. Retrieve the code query parameter from the AT&T platform response and then invoke the GetAuthorizeCredetials() on the requestFactory object as the agrument. 
     * \code
     * In Page Load:
     *  if (!string.IsNullOrEmpty(Request["code"]))
     *  {
     *      //Extract the code query parameter, which is authorization code and store in temporary variable.
     *      authCode = Request["code"].ToString();
     *      //Exchange the authorization code to get access token.
     *      this.requestFactory.GetAuthorizeCredentials(authCode);
     *      Session["ACCESS_TOKEN"] = this.requestFactory.AuthorizeCredential;
     *  }
     * \endcode
     *              \n
     *              3.	Save the OAuth token to make subsequent API calls.
     * \code
     * // Save the access token for future reference or processing.
     * OAuthToken oauthToken = this.requestFactory.AuthorizeCredential;
     *  Session["ACCESS_TOKEN"] = this.requestFactory.AuthorizeCredential;
     * \endcode
     *              \n
     *              4.	This token can be persisted in the database or local storage for later use and can be assigned to the requestFactory.AuthorizeCredential property.
     *          </p>
     * 
     */

    /*!
     * \page GettingStarted Getting Started
     * <table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
    *							<tr vAlign=center>
    *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">Introduction</TD>
    *							</tr>
    *						</table>
    *						
    *						<table style="width: 100%;">
    *						<tr><td>The AT&amp;T API Platform SDK for Microsoft&reg; leverages the power of the Microsoft&reg; .NET platform and AT&amp;T services so that developers can quickly bring robust C# and Visual Basic applications to market. This SDK includes a Wrapper library, full API documentation, guides to installation and setup, and working code samples. The SDK Wrapper library significantly reduces the complexity of building applications that use the AT&amp;T Platform services.
    *						<br />
    *						The SDK Wrapper library provides access to the following AT&amp;T API Platform APIs: 
    *						<ul>
    *						    <li>
    *							Device Capabilities API </li> 
    *							<li>
    *							SMS API </li>
    *							<li>
    *							MMS API </li>	
    *							<li>
    *							Speech API </li>
    *							<li>
    *							In-App Messaging API</li>
    *							<li>
    *							Advertising API</li>
    *							<li>
    *							Text To Speech API</li>
    *							<li>
    *							Speech To Text Custom API</li>
    *							</ul>
    *							The complete documentation for the AT&amp;T API Platform can be found at <a href="http://developer.att.com">http://developer.att.com.</a>
    *							</td>
    *						</tr>
    *						</table>
    *						<table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
    *							<tr valign=center>
    *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">SDK Overview</TD>
    *							</tr>
    *						</table>
    *    					<table style="width: 100%;">
    *						<tr><td>The AT&amp;T API Platform SDK for Microsoft&reg; architecture consists of three layers:  
    *						<ul>
    *							<li>
    *							<b>Client / Browser</b> - This layer contains the application code that interacts with the end user. Application developers will find it much easier to develop C# and Visual Basic applications using the SDK Wrapper library.</li>
    *							<li>
    *							<b>SDK Wrapper library</b> - This layer provides a wrapper library to give application developers an easy way to develop C# and Visual Basic applications. The SDK Wrapper library (ATT_MSSDK.dll) contains a set of wrapper functions that provide uniform and easy to use interfaces for addressing the AT&amp;T REST APIs.</li>
    *							<li>
    *							<b>AT&amp;T API Platform</b> - This layer consists of APIs exposing the suite of AT&amp;T services that the applications will consume. </li>
    *						</ul>			
    *							</td>
    *						</tr>
    *						<tr><td align="center">
    *						<img src="images/SDK_Block_Diagram.png"  height="50%" width="50%"/><br/>
    *						</td>
    *						</tr>
    *						</table>
    *						<table style="width: 100%;">
    *						<tr><td>The SDK Wrapper library consists of two types of classes that are used to address the AT&amp;T REST APIs:
    *						<ul>
    *							<li>
    *							<b>RequestFactory class</b> - The RequestFactory class provides wrapper functions around AT&amp;T Platform services like SMS, MMS, Speech, In App Messaging, Payment, and Device Capabilities. An application creates an instance of the Request Factory class by passing the API Key, Secret Key, scope (a list of services to address), and a redirect URL. When the wrapper functions in the RequestFactory are invoked by the application, an instance of the corresponding Response class is returned. </li>
    *							<li>
    *							<b>Response class</b> - The Response classes provide wrappers for de-serializing the responses returned by the AT&amp;T Platform services.</li>
    *						</ul>			
    *							</td>
    *						</tr>
    *						</table>
    *						<table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
    *							<TR vAlign=center>
    *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">SDK and Sample Apps Packaging</TD>
    *							</TR>
    *						
    *						</table>
    *						<table style="width: 100%;">
    *						<tr><td>The AT&amp;T API Platform SDK for Microsoft&reg; package contains the SDK Wrapper library, licensing terms, and the SDK documentation.<br />
    *						The SDK package has the following file and directory structure:
    *						<ul>
    *							<li>
    *							<i><b>license.txt</b></i> - A file containing the licensing terms for using the SDK Wrapper classes and sample applications.</li>
    *							<li>
    *							<i><b>bin</b></i> - A directory containing the SDK Wrapper class library ATT_MSSDK.DLL.</li>
    *							<li>
    *							<i><b>docs</b></i> - A directory containing SDK documentation, including the Getting Started guide, Installation guide, and Cookbooks for using the SDK Wrapper library.
    *							</li>
     *							<li>
    *							<i><b>README</b></i> - ReadMe file.
    *							</li>
    *							</ul>			
    *							</td>
    *						</tr>
    *						</table>
    *						<table style="width: 100%;">
    *						<tr><td>The Sample Apps package contains sample applications for both the C# and VB.NET languages that showcase the usage of the SDK Wrapper library.<br />
    *						The Sample Apps package has the following directory structure:
    *						<ul>
    *							<li>
    *							<i><b>csharp</b></i> - C# sample applications.</li>
    *							</ul>			
    *							</td>
    *							<tr><td>Sample applications are provided in the following directories:
    *						<ul>
    *							<li>
    *							<i><b>\\sms\\app1</b></i>- The SMS sample application.</li>
    *							<li>
    *							<i><b>\\sms\\app2</b></i>- The SMS voting application receiving inbound SMS messages.</li>
    *							<li>
    *							<i><b>\\mms\\app1</b></i>- The MMS sample application.</li>
    *							<li>
    *							<i><b>\\mms\\app2</b></i>- The MMS Coupon application.</li>
    *							<li>
    *							<i><b>\\mms\\app3</b></i>- The MMS sample application for receiving inbound MMS messages.</li>								
    *							<li>
    *							<i><b>\\speech\\app1</b></i>- The Speech sample application.</li>
    *                           <li>
    *						    <i><b>\\iam\\app1</b></i>- The In-App Messaging sample application.</li>
    *                           <li>
    *							<i><b>\\dc\\app1</b></i>- The Device Capabilities sample application.</li>
    *							<li>
    *							<i><b>\\ads\\app1</b></i>- The Advertising sample application.</li>
    *                           <li>
     *                           <i><b>\\texttospeech\\app1</b></i>- The Text to Speech sample application.</li>
    *						</ul>			
    *							</td>
    *						</tr>
    *						</table>
     * */
    /*!
     * \page installation Installation Guide
     *
     *	<table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
     *		<TR vAlign=center>
     *			<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">Installation prerequisites</TD>
     *		</TR>
     *	</table>
     *	<table style="width: 100%;">
     *		<tr><td>
     *			<ul>
     *				<li>
     *					For deployment: 
     *					<ul><li>Microsoft&reg; Windows Server 2008 R2 is needed.</li></ul><br />
     *				</li>
     *				<li>
     *					For development: 
     *					<ul>
     *						<li>.NET Framework 3.5 or greater should be installed.</li>
     *						<li>Microsoft Visual Studio version 2008 or greater can be used as an IDE.</li>
     *					</ul>
     *				</li>
     *			</ul>
     *		</td></tr>
     *	</table>
     *<table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
     *							<TR vAlign=center>
     *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">Installation process</TD>
     *							</TR>
     *						</table>
     *						<table style="width: 100%;">
     *						<tr><td>
     *						<b> Installing the SDK Wrapper library </b><br />
     *						To install the SDK Wrapper library, do the following:
     *						<ol>
     *							<li>
     *								Download the SDK package from AT&amp;T developer portal.<br />
     *							</li>
     *							<li>
     *							Unzip the SDK package.
     *							</li>
     *							<li>
     *							Copy ATT_MSSDK.dll from the bin folder in the SDK package to a bin directory in the web root of your web domain in IIS server.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     *						<table style="width: 100%;">
     *						<tr><td>
     *							<b>Installing the SDK Sample Applications</b><br />      *							The AT&amp;T API Platform SDK for Microsoft contains sample applications that showcase the usage of the APIs in the SDK. <br />
     *							To install the sample applications, do the following:
     *							<ol>
     *							<li>
     *								Download the sample application package from AT&amp;T developer portal.<br />
     *							</li>
     *							<li>
     *							Unzip the contents of the sample application package.
     *							</li>
     *							<li>Refer to the README in the sample application package for instructions on installation, configuration, and startup of the sample applications.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     *						<table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
     *							<TR vAlign=center>
     *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">Environment setup for C# apps</TD>
     *							</TR>
     *						</table>
     *						
     *						<table style="width: 100%;">
     *						<tr><td>
     *							To setup your development environment for building a C# app that includes the SDK Wrapper library, you need to first create a new Web site project, and then add the SDK Wrapper library as a reference in that project.
     *							<br /><br />
     *							The following steps describe how to create a new C# Web site project using Microsoft Visual Studio as the IDE:
     *							<ol>
     *							<li>Open Microsoft Visual Studio.</li>
     *							<li>On the File menu, click New Web Site. This opens the New Web Site dialog box.
     *							</li>
     *							<li>In the New Web Site dialog box, under Visual Studio installed templates, select ASP.NET Web Site.</li>
     *							<li>In the web Location box, select File System from the dropdown menu and enter the name of the folder where you want to keep the pages of your Web site. <br />
     *							For example:  C:\\BasicWebSite.
     *							</li>
     *							<li>In the Language list, click Visual C#.</li>
     *							<li>Click OK. The folder and a new page named Default.aspx application development are created.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     *						<table style="width: 100%;">
     *						<tr><td>
     *							The following steps describe how to add the SDK Wrapper library as a reference in the Visual Studio Web site project created above.
     *							<ol>
     *							<li>In Solution Explorer, right click on Project and select Add Reference from the dropdown menu. This will open the Add Reference dialog box.</li>
     *							<li>In the Add Reference dialog box, select the Browse tab and then select the file ATT_MSSDK.DLL.</li>
     *							<li>Click OK. A bin directory will be created under your project that contains ATT_MSSDK.DLL along with support files.</li>
     *							<li>The APIs in the SDK Wrapper library can now be referenced in your project.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     *   <table id="one" style="background-color: #EF9E20; margin: 1em 0px; width: 100%; height: 33px;">
     *							<TR vAlign=center>
     *								<TD style="BACKGROUND-COLOR: #EF9E20; font-weight:bold !important; PADDING-LEFT: 10px; WIDTH: 192px; font-family:Verdana, Geneva, Arial, Helvetica, sans-serif; COLOR: white; FONT-SIZE: 12pt">Environment setup for VB apps</TD>
     *							</TR>
     *						</table>
     *						
     *						<table style="width: 100%;">
     *						<tr><td>
     *							To setup your development environment for building a VB app that includes the SDK Wrapper library, you need to first create a new Web site project, and then add the SDK Wrapper library as a reference in that project.<br /><br />
     *							The following steps describe how to create a new Visual Basic Web site project using Microsoft Visual Studio as the IDE:
     *							<ol>
     *							<li>Open Microsoft Visual Studio.</li>
     *							<li>On the File menu, click New Web Site. This opens the New Web Site dialog box.</li>
     *							<li>In the New Web Site dialog box, under Visual Studio installed templates, click ASP.NET Web Site.</li>
     *							<li>In the web Location box, select File System from the dropdown menu and enter the name of the folder where you want to keep the pages of your Web site. <br />For example:  C:\\BasicWebSite.</li>
     *							<li>In the Language list, click Visual Basic.</li>
     *							<li>Click OK. A folder and a new page named Default.aspx application development are created.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     *						<table style="width: 100%;">
     *						<tr><td>
     *							The following steps describe how to add the SDK Wrapper library as a reference in the Visual Studio Web site project created above. 
     *							<ol>
     *							<li>In Solution Explorer, right-click on Project and select Add Reference from the dropdown menu. This will open the Add Reference dialog box.</li>
     *							<li>In the Add Reference dialog box, select the Browse tab and then select the file ATT_MSSDK DLL.</li>
     *							<li>Click OK. A bin directory will be created under your project that contains ATT_MSSDK DLL along with support files.</li>
     *							<li>The APIs in the SDK Wrapper library can now be referenced in your project.</li>
     *							</ol>					
     *							</td>
     *						</tr>
     *						</table>
     */

    /*!
    * \class RequestFactory
    * \brief The %RequestFactory manages the connections and calls to the AT&amp;T API Platform.
    * 
    * You must create a %RequestFactory instance for each AT&amp;T API Platform Application.
    * The clientId and clientSecret values are available from the AT&amp;T Developer Platform website.
    * Each %RequestFactory must have a set of Scopes specifying which services are allowed.
    * Scopes that are not configured for your application will not work.
    * For example, your application may be configured in the AT&amp;T API Platform to support the MMS and SMS scopes.
    * The %RequestFactory may specify any combination of MMS or SMS. If you specify Scopes other than MMS and SMS, those scopes will not work.
    * Connections and Credentials are not established until commands, for example SendSms, are called.
    */
    
    /// <summary>
    /// The %RequestFactory manages the connections and calls to the AT&amp;T API Platform.
    /// </summary>
    public partial class RequestFactory
    {
        /// <summary>
        /// Initializes a new instance of the RequestFactory class. If application creates and uses multiple instances of Request Factory, then aplication should use this constructor to create Request Factory instance.
        /// This constructor takes the filepath passed as parameter to store/load the access token. So instead of each RequestFactory instance getting its own access token,  access token saved/loaded from the file will be reused.
        /// </summary>
        /// <param name="endPoint">The End Point URL.  This defaults to https://api.att.com.</param>
        /// <param name="clientId">The client id of your application.</param>
        /// <param name="clientSecret">The client secret of your application.</param>
        /// <param name="scopes">One or more values from  SMS, MMS, SPEECH etc.</param>
        /// <param name="redirectUri">The URL for the OAuth redirect.  Required if scopes include Terminal Location or Device Capabilities</param>
        /// <param name="proxyAddress">The URL and Port number of your proxy, if you have one.  For example: http://proxy:8888.</param>
        /// <param name="tokenCache">Object responsible for persisting the authorization token.</param>
        /// <param name="http">Object responsible for sending AT&amp;T API network requests.</param>
        /// <exception cref="ArgumentException">Thrown if an argument is incorrect.</exception> 
        /// <exception cref="ArgumentNullException">Thrown if an argument is unexpectedly null.</exception> 
        public RequestFactory(string endPoint, string clientId, string clientSecret, List<ScopeTypes> scopes, string redirectUri, string proxyAddress, ITokenCache tokenCache, IHttp http)
        {
            // For defaultTimeout = 0, webRequest.Timeout will not be set while creating HttpWebRequest in the send() method , thereby resulting
            // in default behaviour for webRequest.Timeout
            int defaultTimeout = 0;
            this.intialize(endPoint, clientId, clientSecret, scopes, redirectUri, proxyAddress, defaultTimeout, tokenCache, http);
        }

        /// <summary>
        /// Initializes a new instance of the RequestFactory class. If application creates and uses multiple instances of Request Factory, then aplication should use this constructor to create Request Factory instance.
        /// This constructor takes the filepath passed as parameter to store/load the access token. So instead of each RequestFactory instance getting its own access token,  access token saved/loaded from the file will be reused.
        /// </summary>
        /// <param name="endPoint">The End Point URL.  This defaults to https://api.att.com.</param>
        /// <param name="clientId">The client id of your application.</param>
        /// <param name="clientSecret">The client secret of your application.</param>
        /// <param name="scopes">One or more values from  SMS, MMS, SPEECH etc.</param>
        /// <param name="redirectUri">The URL for the OAuth redirect.  Required if scopes include Terminal Location or Device Capabilities</param>
        /// <param name="proxyAddress">The URL and Port number of your proxy, if you have one.  For example: http://proxy:8888.</param>
        /// <param name="tokenFilePath">The filepath for saving/loading the access token. RequestFactory instance loads and saves the access token in this file.</param>
        /// <exception cref="ArgumentException">Thrown if the arguments are incorrect.</exception> 
        /// <exception cref="ArgumentNullException">Thrown if an argument is unexpectedly null.</exception> 
        public RequestFactory(string endPoint, string clientId, string clientSecret, List<ScopeTypes> scopes, string redirectUri, string proxyAddress, String tokenFilePath)
        {
            // For defaultTimeout = 0, webRequest.Timeout will not be set while creating HttpWebRequest in the send() method , thereby resulting
            // in default behaviour for webRequest.Timeout
            int defaultTimeout = 0;
            this.intialize(endPoint, clientId, clientSecret, scopes, redirectUri, proxyAddress, defaultTimeout, new FileTokenCache(tokenFilePath), null);
        }

        /// <summary>
        /// Initializes a new instance of the RequestFactory class.
        /// </summary>
        /// <param name="endPoint">The End Point URL.  This defaults to https://api.att.com.</param>
        /// <param name="clientId">The client id of your application.</param>
        /// <param name="clientSecret">The client secret of your application.</param>
        /// <param name="scopes">One or more values from  SMS, MMS, SPEECH etc.</param>
        /// <param name="redirectUri">The URL for the OAuth redirect.  Required if scopes include Terminal Location or Device Capabilities</param>
        /// <param name="proxyAddress">The URL and Port number of your proxy, if you have one.  For example: http://proxy:8888.</param>
        /// <exception cref="ArgumentException">Thrown if the arguments are incorrect.</exception>
        /// <exception cref="ArgumentNullException">Thrown if an argument is unexpectedly null.</exception> 
        public RequestFactory(string endPoint, string clientId, string clientSecret, List<ScopeTypes> scopes, string redirectUri, string proxyAddress)
        {
            // For defaultTimeout = 0, webRequest.Timeout will not be set while creating HttpWebRequest in the send() method , thereby resulting
            // in default behaviour for webRequest.Timeout
            int defaultTimeout = 0;
            this.intialize(endPoint, clientId, clientSecret, scopes, redirectUri, proxyAddress, defaultTimeout, new InMemoryTokenCache(), null);
        }

        /// <summary>
        /// Initializes a new instance of the RequestFactory class. Takes an additional parameter 'timeout'. 
        /// This overloaded constructor can be used by the application, if the application wants to set 'HttpWebRequest.Timeout' property for WebRequests to be done by RequestFactory. 
        /// In this case, timeout value in milliseconds passed through constructor will be used by RequestFactory to set 'HttpWebRequest.Timeout' property when RequestFactory creates WebRequest.
        /// If this constructor is not used then RequestFactory will not set 'HttpWebRequest.Timeout' property during creation of WebRequest, resulting in default behaviour of 'HttpWebRequest.Timeout' property.
        /// </summary>
        /// <param name="endPoint">The End Point URL.  This defaults to https://api.att.com.</param>
        /// <param name="clientId">The client id of your application.</param>
        /// <param name="clientSecret">The client secret of your application.</param>
        /// <param name="scopes">One or more values from  SMS, MMS, SPEECH etc.</param>
        /// <param name="redirectUri">The URL for the OAuth redirect.  Required if scopes include Terminal Location or Device Capabilities</param>
        /// <param name="proxyAddress">The URL and Port number of your proxy, if you have one.  For example: http://proxy:8888.</param>
        /// <param name="timeout">Timeout value in milliseconds to be used by request factory for setting HttpWebRequest.timeout property while invoking HttpWebRequest  For example:5000.</param>
        /// <exception cref="ArgumentException">Thrown if the arguments are incorrect.</exception>
        /// <exception cref="ArgumentNullException">Thrown if an argument is unexpectedly null.</exception> 
        public RequestFactory(string endPoint, string clientId, string clientSecret, List<ScopeTypes> scopes, string redirectUri, string proxyAddress, int timeout)
        {
            this.intialize(endPoint, clientId, clientSecret, scopes, redirectUri, proxyAddress, timeout, new InMemoryTokenCache(), null);
        }

        private void intialize(string endPoint, string clientId, string clientSecret, List<ScopeTypes> scopes, string redirectUri, string proxyAddress, int timeout, ITokenCache tokenCache, IHttp http)
        {
            Debug.WriteLine("RequestFactory.initialize called");

            this.EndPoint = CreateValidatedUri("endPoint", endPoint);
            this.ClientId = Validate("clientId", clientId);
            this.ClientSecret = Validate("clientSecret", clientSecret);
            this.TokenCache = ValidateObject<ITokenCache>("tokenCache", tokenCache);
            this.ProxyAddress = CreateValidatedProxyAddress(proxyAddress);
            this.Scopes = CreateValidatedScopes(scopes);
            this.RedirectUri = CreateValidatedRedirectUri(redirectUri, this.Scopes);
            this.RequestTimeout = ValidateRequestTimeout(timeout);
            this.Http = http != null ? http : new Http(this.EndPoint, this.Scopes, this.RequestTimeout, this.ProxyAddress);

            DisableRemoteCertificateValidationWhenNotInProduction();

        }

        ///// <summary>
        ///// OAuthToken persistence repository.
        ///// </summary>
        //public IRepository TokenRepository;

        /// <summary>
        /// Set of allowed Scope values
        /// </summary>
        public enum ScopeTypes
        {
            /// <summary>
            /// Device Capability scope
            /// </summary>
            DeviceCapability,

            /// <summary>
            /// Payment scope
            /// </summary>
            Payment,

            /// <summary>
            /// SMS scope
            /// </summary>
            SMS,

            /// <summary>
            /// MMS scope
            /// </summary>
            MMS,

            /// <summary>
            /// DeviceLocation scope
            /// </summary>
            TerminalLocation,

            /// <summary>
            /// WAPPush scope
            /// </summary>
            WAPPush,

            /// <summary>
            /// MOBO scope
            /// </summary>
            MOBO,

            /// <summary>
            /// IMMN scope
            /// </summary>
            IMMN,

            /// <summary>
            /// MIM scope
            /// </summary>
            MIM,

            /// <summary>
            /// Speech scope
            /// </summary>
            Speech,

            /// <summary>
            /// ADS scope
            /// </summary>
            ADS,

            ///// <summary>
            ///// Call Control scope
            ///// </summary>
            //CallControl,

            /// <summary>
            /// Text To Speech scope
            /// </summary>
            TTS,

            /// <summary>
            /// Speech To Text Custom scope
            /// </summary>
            STTC
        }

        /// <summary>
        /// Set of allowed HTTP Methods
        /// </summary>
        public enum HTTPMethods
        {
            /// <summary>
            /// HTTP POST
            /// </summary>
            POST,

            /// <summary>
            /// HTTP PUT
            /// </summary>
            PUT,

            /// <summary>
            /// HTTP GET
            /// </summary>
            GET,

            /// <summary>
            /// HTTP DELETE
            /// </summary>
            DELETE
        }

        /// <summary>
        /// Gets or sets the EndPoint for this RequestFactory.
        /// </summary>
        private Uri EndPoint { get; set; }

        /// <summary>
        /// Gets or sets the ClientId for this RequestFactory. 
        /// The ClientID of registered application.
        /// </summary>
        private string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the ClientSecret for this RequestFactory. 
        /// The ClientSecret of registered application.
        /// </summary>
        private string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the scopes allowed for this RequestFactory.         
        /// </summary>
        private List<ScopeTypes> Scopes { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUri for this RequestFactory. 
        /// The URL for the OAuth redirect.
        /// </summary>
        private Uri RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the ProxyAddress for this RequestFactory. 
        /// The URL and Port number of your proxy
        /// </summary>
        private Uri ProxyAddress { get; set; }

        /// <summary>
        /// Gets or sets the RequestTimeout for this RequestFactory. 
        /// RequestFactory uses RequestTimeout value in milliseconds to set HttpWebRequest.timeout property while invoking HttpWebRequest
        /// </summary>
        private int RequestTimeout { get; set; }

        /// <summary>
        /// Gets or sets the ClientCredential for this RequestFactory.
        /// </summary>
        public OAuthToken ClientCredential { get; set; }

        /// <summary>
        /// Gets or sets the AuthorizeCredential for this RequestFactory.
        /// </summary>
        public OAuthToken AuthorizeCredential { get; set; }

        /// <summary>
        /// Persistently stores our client auth token, so we can use it
        /// across process restarts.
        /// </summary>
        internal ITokenCache TokenCache;

        /// <summary>
        /// Abstracts out network communication, for testing purposes.
        /// </summary>
        private IHttp Http;

        /// <summary>
        /// This method can be used during testing to differentiate between test endpoints and the production endpoints.
        /// </summary>
        /// <returns>True if the EndPoint is https://api.att.com.</returns>
        public bool IsProduction()
        {
            return this.EndPoint.Host.Equals("api.att.com") && this.EndPoint.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// Used internally to turn off certificate validation for non-production end points.
        /// </summary>
        /// <param name="sender">Boilerplate headers: sender</param>
        /// <param name="certificate">Boilerplate headers: certificate</param>
        /// <param name="chain">Boilerplate headers: chain</param>
        /// <param name="sslPolicyErrors">Boilerplate headers: sslPolicyErrors</param>
        /// <returns>Always returns true, which allows any certificate to work.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*")]
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private bool ScopeRequiresUserAuthentication(ScopeTypes scope)
        {
            return scope == ScopeTypes.DeviceCapability
                || scope == ScopeTypes.TerminalLocation
                || scope == ScopeTypes.MOBO
                || scope == ScopeTypes.MIM
                || scope == ScopeTypes.IMMN;
        }

        private bool ScopesRequireRedirectUrlForUserAuthentication(List<ScopeTypes> scopes)
        {
            return scopes.FindAll(scope => ScopeRequiresUserAuthentication(scope)).Count != 0;
        }

        private string Validate(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterValue))
            {
                string errorMessage = string.Format("missing {0} parameter", parameterName);
                Debug.WriteLine(errorMessage);
                throw new ArgumentException(errorMessage, parameterName);
            }
            Debug.WriteLine(string.Format("{0} parameter: {1}", parameterName, parameterValue));
            return parameterValue;
        }

        private T ValidateObject<T>(string parameterName, T parameterValue)
        {
            if (parameterValue == null)
            {
                Debug.WriteLine(string.Format("missing {0} parameter", parameterName));
                throw new ArgumentNullException(parameterName);
            }
            return parameterValue;
        }

        private Uri CreateValidatedUri(string parameterName, string uri)
        {
            try
            {
                return new Uri(Validate(parameterName, uri));
            }
            catch (UriFormatException ex)
            {
                string errorMessage = string.Format("{0} parameter not in URL format: {1}", parameterName, uri);
                Debug.WriteLine(errorMessage);
                throw new ArgumentException(errorMessage, parameterName, ex);
            }
        }

        private List<ScopeTypes> CreateValidatedScopes(List<ScopeTypes> scopes)
        {
            if (scopes == null)
            {
                scopes = new List<ScopeTypes>();
            }
            scopes.ForEach(scope => Debug.WriteLine("scope requested: " + scope.ToString()));
            return scopes;
        }

        private Uri CreateValidatedRedirectUri(string redirectUri, List<ScopeTypes> scopes)
        {
            if (ScopesRequireRedirectUrlForUserAuthentication(scopes))
            {
                if (string.IsNullOrEmpty(redirectUri))
                {
                    string errorMessage = "redirect URL is missing and the scopes require it";
                    Debug.WriteLine(errorMessage);
                    throw new ArgumentException(errorMessage, "redirectUri");
                }
                try
                {
                    return new Uri(redirectUri);
                }
                catch (UriFormatException ex)
                {
                    string errorMessage = "proxy address parameter not in URL format: " + redirectUri;
                    Debug.WriteLine(errorMessage);
                    throw new ArgumentException(errorMessage, "redirectUri", ex);
                }
            }
            return null;
        }

        private Uri CreateValidatedProxyAddress(string proxyAddress)
        {
            if (!string.IsNullOrEmpty(proxyAddress))
            {
                Debug.WriteLine("proxy address parameter: " + proxyAddress);
                try
                {
                    this.ProxyAddress = new Uri(proxyAddress);
                }
                catch (UriFormatException ex)
                {
                    Debug.WriteLine("proxy address parameter not in URL format: " + proxyAddress);
                    throw new ArgumentException("proxy address parameter not in URL format: " + proxyAddress, "proxyAddress", ex);
                }
            }
            return null;
        }

        private void DisableRemoteCertificateValidationWhenNotInProduction()
        {
            if (!this.IsProduction() || (this.ProxyAddress != null && this.ProxyAddress.ToString().StartsWith("http://localhost")))
            {
                Debug.WriteLine("non-production environment detected - disabling remote certificate validation");
                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
            }
        }

        private int ValidateRequestTimeout(int requestTimeout)
        {
            if (requestTimeout < 0)
            {
                string errorMessage = string.Format("invalid request timeout value: {0}", requestTimeout);
                Debug.WriteLine(errorMessage);
                throw new ArgumentException(errorMessage, "requestTimeout");
            }
            return requestTimeout;
        }
    }
}
