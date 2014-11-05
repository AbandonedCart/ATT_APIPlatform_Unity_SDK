using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// Methods for sending AT&amp;T API network requests for the MS SDK.
    /// </summary>
    public interface IHttp
    {
        /// <summary>
        /// Send an AT&amp;T API request.
        /// </summary>
        /// <param name="method">HTTP verb</param>
        /// <param name="relativeUri">URL path</param>
        /// <param name="headers">HTTP headers</param>
        /// <param name="bodyBytes">HTTP body content</param>
        /// <param name="contentType">HTTP body content type</param>
        /// <param name="accept">Acceptable response types</param>
        /// <param name="returnWebResponse">should the WebResponse object be returned?</param>
        /// <returns>WebResponse object, or null</returns>
        object Send(ATT_UNITY_SDK.RequestFactory.HTTPMethods method, string relativeUri, NameValueCollection headers, byte[] bodyBytes, string contentType, string accept, bool returnWebResponse);

        /// <summary>
        /// Send an AT&amp;T API request.
        /// </summary>
        /// <param name="relativeUri">URL path</param>
        /// <param name="headers">HTTP headers</param>
        /// <param name="body">HTTP body content</param>
        /// <param name="contentType">HTTP body content type</param>
        /// <param name="accept">Acceptable response types</param>
        /// <returns>response body</returns>
        string Send(string relativeUri, NameValueCollection headers, byte[] body, string contentType, string accept);

    }
}
