using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// Methods for sending AT&amp;T API network requests for the MS SDK.
    /// </summary>
    public class Http : IHttp
    {
        private Uri EndPoint { get; set; }
        private int RequestTimeout { get; set; }
        private Uri ProxyAddress { get; set; }
        private List<ATT_UNITY_SDK.RequestFactory.ScopeTypes> Scopes { get; set; }

        /// <summary>
        /// ClientSdk value to be hardcoded in XArgs for MSSDK .
        /// </summary>
        private static string ClientSdk = "att.mssdk.4.0";

        ///// <summary>
        ///// ClientSdk value to be hardcoded in XArgs for UnitySDK.     
        ///// While building MSSDK dll for UnitySDK package, comment the above line defining the value as "att.mssdk.4.0" 
        ///// and uncomment the below line .
        ///// </summary>
        //private static string ClientSdk = "att.unity.4.0";

        /// <summary>
        /// Construct an object for sending AT&amp;T API requests.
        /// </summary>
        /// <param name="endPoint">AT&amp;T API host</param>
        /// <param name="scopes">scopes representing the AT&amp;T web services to be accessed</param>
        /// <param name="requestTimeout">request timeout</param>
        /// <param name="proxyAddress">proxy server</param>
        public Http(Uri endPoint, List<ATT_UNITY_SDK.RequestFactory.ScopeTypes> scopes, int requestTimeout, Uri proxyAddress)
        {
            Debug.WriteLine("Http constructor called");

            this.EndPoint = endPoint;
            this.Scopes = scopes;
            this.RequestTimeout = requestTimeout;
            this.ProxyAddress = proxyAddress;
        }

        /// <summary>
        /// Sends a message to the endpoint specified for this RequestFactory.
        /// </summary>
        /// <param name="method">The HTTP Method to use</param>
        /// <param name="relativeUri">Uri to concatenate with EndPoint</param>
        /// <param name="headers">HTTP Headers to include in the request</param>
        /// <param name="bodyBytes">Bytes to put into the body</param>
        /// <param name="contentType">MIME Type of the request, e.g., application/json or application/xml</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <param name="returnWebResponse">boolean value to indicate if the raw web response has to be returned, or just the response body content as a string.</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        public object Send(ATT_UNITY_SDK.RequestFactory.HTTPMethods method, string relativeUri, NameValueCollection headers, byte[] bodyBytes, string contentType, string accept, bool returnWebResponse)
        {
            Debug.WriteLine("Http.Send(ATT_MSSDK.RequestFactory.HTTPMethods method, string relativeUri, NameValueCollection headers, byte[] bodyBytes, string contentType, string accept, bool returnWebResponse = false) called");
            Debug.WriteLine("HTTP verb: " + method.ToString());

            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentException("relativeUri is empty");
            }
            Debug.WriteLine("relativeUri: " + relativeUri);

            if (string.IsNullOrEmpty(accept))
            {
                throw new ArgumentException("accept is empty");
            }
            Debug.WriteLine("accept: " + accept);

            string responseData = string.Empty;
            Uri uri = new Uri(this.EndPoint, relativeUri);
            HttpWebRequest webRequest;
            HttpWebResponse webResponse = null;

            try
            {
                webRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);

                if (this.RequestTimeout != 0)
                {
                    webRequest.Timeout = this.RequestTimeout;
                }

                if (this.ProxyAddress != null)
                {
                    webRequest.Proxy = new WebProxy(this.ProxyAddress);
                }

                switch (method)
                {
                    case ATT_UNITY_SDK.RequestFactory.HTTPMethods.GET:
                        webRequest.Method = "GET";
                        webRequest.Accept = accept;
                        break;
                    case ATT_UNITY_SDK.RequestFactory.HTTPMethods.POST:
                        webRequest.Method = "POST";
                        webRequest.ContentType = contentType;
                        webRequest.Accept = accept;
                        break;
                    case ATT_UNITY_SDK.RequestFactory.HTTPMethods.PUT:
                        webRequest.Method = "PUT";
                        webRequest.ContentType = contentType;
                        webRequest.Accept = accept;
                        break;
                    case ATT_UNITY_SDK.RequestFactory.HTTPMethods.DELETE:
                        webRequest.Method = "DELETE";
                        webRequest.ContentType = contentType;
                        webRequest.Accept = accept;
                        break;
                }

                headers = CheckForClientSdkInXArgs(headers);

                if (headers != null)
                {
                    webRequest.Headers.Add(headers);
                }

                if (this.Scopes.Contains(ATT_UNITY_SDK.RequestFactory.ScopeTypes.ADS))
                {
                    //webRequest.UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
                    webRequest.UserAgent = "Mozilla/5.0 (Android; Mobile; rv:13.0) Gecko/13.0 Firefox/13.0";
                }

                if (bodyBytes != null)
                {
                    webRequest.KeepAlive = true;

                    webRequest.ContentLength = bodyBytes.Length;

                    Stream postStream = webRequest.GetRequestStream();
                    postStream.Write(bodyBytes, 0, bodyBytes.Length);
                    postStream.Close();
                }
                else
                {
                    webRequest.ContentLength = 0;
                }

                webResponse = (HttpWebResponse)webRequest.GetResponse();
                if (returnWebResponse == true)
                {
                    return webResponse;
                }

                using (StreamReader sr2 = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseData = sr2.ReadToEnd();
                    sr2.Close();
                }

                if (returnWebResponse == true)
                    return webResponse;
            }
            catch (WebException ex)
            {
                string errorResponse = string.Empty;

                ErrorResponse responseError = null;


                try
                {
                    using (StreamReader sr2 = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        errorResponse = sr2.ReadToEnd();
                        responseError = new ErrorResponse();
                        responseError.ParseErrorResponse(errorResponse);

                        sr2.Close();
                    }
                }
                catch
                {
                    errorResponse = "Unable to get response.";
                }

                if (ex.Message != null)
                {
                    errorResponse = errorResponse + " " + ex.Message;
                }

                InvalidResponseException ire = new InvalidResponseException("Failed: " + errorResponse, ex, (webResponse != null ? webResponse.StatusCode : HttpStatusCode.BadRequest), errorResponse);
                ire.ErrorResponseObject = responseError;
                throw ire;
            }
            catch (Exception ex)
            {
                throw new InvalidResponseException("Failed: " + ex.Message, ex);
            }
            finally
            {
                webRequest = null;
                webResponse = null;
            }

            return responseData;
        }

        /// <summary>
        /// Sends a message to the endpoint specified for this RequestFactory.
        /// </summary>
        /// <param name="relativeUri">Uri to concatenate with EndPoint</param>
        /// <param name="headers">HTTP Headers to include in the request</param>
        /// <param name="body">byte data to POST</param>
        /// <param name="contentType">MIME Type of the request, e.g., application/json or application/xml</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        public string Send(string relativeUri, NameValueCollection headers, byte[] body, string contentType, string accept)
        {
            Debug.WriteLine("Http.Send(string relativeUri, NameValueCollection headers, byte[] body, string contentType, string accept) called");

            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentException("relativeUri is empty");
            }
            Debug.WriteLine("relativeUri: " + relativeUri);

            if (string.IsNullOrEmpty(accept))
            {
                throw new ArgumentException("accept is empty");
            }
            Debug.WriteLine("accept: " + accept);

            string responseData = string.Empty;
            Uri uri = new Uri(this.EndPoint, relativeUri);
            HttpWebRequest webRequest;
            HttpWebResponse webResponse = null;

            try
            {
                webRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);

                if (this.RequestTimeout != 0)
                {
                    webRequest.Timeout = this.RequestTimeout;
                }

                if (this.ProxyAddress != null)
                {
                    webRequest.Proxy = new WebProxy(this.ProxyAddress);
                }

                webRequest.Method = "POST";
                webRequest.ContentType = contentType;
                webRequest.Accept = accept;

                headers = CheckForClientSdkInXArgs(headers);

                if (headers != null)
                {
                    webRequest.Headers.Add(headers);
                }

                webRequest.ContentLength = body.Length;
                Stream postStream = webRequest.GetRequestStream();
                postStream.Write(body, 0, body.Length);
                postStream.Close();

                webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader sr2 = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseData = sr2.ReadToEnd();
                    sr2.Close();
                }
            }
            catch (WebException ex)
            {
                string errorResponse = string.Empty;
                ErrorResponse responseError = null;

                try
                {
                    using (StreamReader sr2 = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        errorResponse = sr2.ReadToEnd();
                        responseError = new ErrorResponse();
                        responseError.ParseErrorResponse(errorResponse);

                        sr2.Close();
                    }
                }
                catch
                {
                    errorResponse = "Unable to get response";
                }

                InvalidResponseException ire = new InvalidResponseException("Failed: " + errorResponse, ex, (webResponse != null ? webResponse.StatusCode : HttpStatusCode.BadRequest), errorResponse);
                ire.ErrorResponseObject = responseError;
                throw ire;
            }
            catch (Exception ex)
            {
                throw new InvalidResponseException("Failed: " + ex.Message, ex);
            }
            finally
            {
                webRequest = null;
                webResponse = null;
            }

            return responseData;
        }

        /// <summary>
        /// Checks if header contains ClientSdk name value pair. 
        /// If ClientSdk name value pair is present in XArgs it is overwritten else ClientSdk name value Pair is added to XArgs header
        /// </summary>
        /// <param name="headers">headers to be added to WebRequest</param>
        /// <returns>Header with ClientSdk name value pair added/overwritten in XArgs.</returns>
        private NameValueCollection CheckForClientSdkInXArgs(NameValueCollection headers)
        {
            if (headers != null)
            {
                String xArgs = headers.Get("X-Arg");
                if (xArgs != null)
                {
                    string xArgsData = AddClientSdkToXArgs(xArgs, ClientSdk);
                    headers.Set("X-Arg", xArgsData);
                }
                else
                {
                    headers.Add("X-Arg", "ClientSdk=" + ClientSdk);
                }
            }
            else
            {
                headers = new NameValueCollection();
                headers.Add("X-Arg", "ClientSdk=" + ClientSdk);
            }

            return headers;
        }

        private static string AddClientSdkToXArgs(string xArgs, string clientSdkValue)
        {
            string xArgsData = string.Empty;
            if (!string.IsNullOrEmpty(xArgs))
            {
                string[] xArgsValues = xArgs.Split(',');
                bool clientSdkDefined = false;
                foreach (string xArgValue in xArgsValues)
                {

                    string[] xArg = xArgValue.Split('=');
                    if (null != xArg && xArg.Length > 1)
                    {
                        string namevalue = string.Empty;
                        if (!string.IsNullOrEmpty(xArg[0]) && xArg[0].ToLower().Equals("clientsdk"))
                        {
                            namevalue = "ClientSdk=" + clientSdkValue + ",";
                            clientSdkDefined = true;
                        }
                        else
                        {
                            namevalue = xArg[0] + "=" + HttpUtility.UrlEncode(xArg[1]) + ",";
                        }

                        xArgsData += namevalue;
                    }
                }

                if (clientSdkDefined == false)
                {
                    xArgsData += "ClientSdk=" + clientSdkValue;
                }

                if (xArgsData.EndsWith(","))
                    xArgsData = xArgsData.Substring(0, xArgsData.Length - 1);
            }
            else
            {
                xArgsData = "ClientSdk=" + clientSdkValue;
            }

            return xArgsData;
        }
    }
}
