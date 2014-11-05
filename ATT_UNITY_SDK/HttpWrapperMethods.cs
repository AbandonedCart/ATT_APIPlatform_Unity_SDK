using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented")]
    public partial class RequestFactory
    {
        /// <summary>
        /// Sends a message to the endpoint specified for this RequestFactory.
        /// </summary>
        /// <param name="method">The HTTP Method to use</param>
        /// <param name="relativeUri">Uri to concatenate with EndPoint</param>
        /// <param name="parameters">Values to send</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string Send(HTTPMethods method, string relativeUri, NameValueCollection parameters)
        {
            return Send(method, relativeUri, parameters, "application/json");
        }

        /// <summary>
        /// Sends a message to the endpoint specified for this RequestFactory.
        /// </summary>
        /// <param name="method">The HTTP Method to use</param>
        /// <param name="relativeUri">Uri to concatenate with EndPoint</param>
        /// <param name="parameters">Values to send</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string Send(HTTPMethods method, string relativeUri, NameValueCollection parameters, string accept)
        {
            string body = this.FormEncode(parameters);

            return Send(method, relativeUri, null, body, "application/x-www-form-urlencoded", accept);
        }

        /// <summary>
        /// Sends a message to the endpoint specified for this RequestFactory.
        /// </summary>
        /// <param name="method">The HTTP Method to use</param>
        /// <param name="relativeUri">Uri to concatenate with EndPoint</param>
        /// <param name="headers">HTTP Headers to include in the request</param>
        /// <param name="body">String to POST</param>
        /// <param name="contentType">MIME Type of the request, e.g., application/json or application/xml</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string Send(HTTPMethods method, string relativeUri, NameValueCollection headers, string body, string contentType, string accept)
        {
            byte[] postBytes = null;

            if (!string.IsNullOrEmpty(body))
            {
                UTF8Encoding encoding = new UTF8Encoding();

                postBytes = encoding.GetBytes(body);
            }

            return (string)this.Http.Send(method, relativeUri, headers, postBytes, contentType, accept, false);
        }

        /// <summary>
        /// HTTP POSTs the Parameters to the specified URI
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <param name="parameters">The name/value pairs to post</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoPost(string relativeUri, NameValueCollection parameters)
        {
            return this.Send(HTTPMethods.POST, relativeUri, parameters);
        }

        /// <summary>
        /// HTTP POSTs the Parameters to the specified URI
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <param name="parameters">The name/value pairs to post</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoPost(string relativeUri, NameValueCollection parameters, string accept)
        {
            return Send(HTTPMethods.POST, relativeUri, parameters, accept);
        }

        /// <summary>
        /// HTTP PUTs the Parameters to the specified URI
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <param name="parameters">The name/value pairs to post</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoPut(string relativeUri, NameValueCollection parameters)
        {
            return this.Send(HTTPMethods.PUT, relativeUri, parameters);
        }

        /// <summary>
        /// HTTP PUTs the Parameters to the specified URI
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <param name="parameters">The name/value pairs to put</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoPut(string relativeUri, NameValueCollection parameters, string accept)
        {
            return this.Send(HTTPMethods.PUT, relativeUri, parameters, accept);
        }

        /// <summary>
        /// GET a from the endpoint specified for this RequestFactory.  The accept type is application/json.
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoGet(string relativeUri)
        {
            NameValueCollection nvc = null; // Compiler couldn't figure out which Send method to use with all the nulls
            byte[] bodyBytes = null; // Refactor?
            return (string)this.Http.Send(HTTPMethods.GET, relativeUri, nvc, bodyBytes, null, "application/json", false);
        }

        /// <summary>
        /// GET a from the endpoint specified for this RequestFactory.  The accept type is application/json.
        /// </summary>
        /// <param name="relativeUri">The relative URI to post the information to</param>
        /// <param name="accept">MIME Type of the returned value, e.g., application/json or application/xml</param>
        /// <exception cref="InvalidResponseException">Thrown if the server returns an error or there is a connection failure.</exception>
        /// <returns>The response to the API call.</returns>
        private string DoGet(string relativeUri, string accept)
        {
            return Send(HTTPMethods.GET, relativeUri, null, accept);
        }

        /// <summary>
        /// HTTP Form Encode the headers
        /// </summary>
        /// <param name="parameters">The parameters to encode.</param>
        /// <returns>The Encoded String.</returns>
        private string FormEncode(NameValueCollection parameters)
        {
            StringBuilder sb = new StringBuilder(string.Empty);

            if (parameters != null && parameters.Count > 0)
            {
                sb.Append(string.Format("{0}={1}", parameters.GetKey(0), parameters.Get(0).ToString()));
                for (int i = 1; i < parameters.Count; i++)
                {
                    sb.Append(string.Format("&{0}={1}", parameters.GetKey(i), parameters.Get(i).ToString()));
                }
            }

            return sb.ToString();
        }
    }
}
