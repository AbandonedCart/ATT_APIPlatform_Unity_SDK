// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="ErrorResponse.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------
using Newtonsoft.Json;

namespace ATT_UNITY_SDK
{
    using System.Collections.Generic;
//    using System.Web.Script.Serialization;

    /// <summary>
    /// The %ErrorResponse contains the value of %ErrorType and %ErrorObject.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the value of error type. Possible error types include service error, policy exception and service exception.
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// Gets or sets the value of error object, which contains the error information.
        /// </summary>
        public ErrorObjectRaw ErrorObject { get; set; }

        /// <summary>
        /// This method loops through the contents of the Json object and generates a dictionary of error response objects.
        /// </summary>
        /// <param name="jsonInput">Json string to be converted to dictionary.</param>
        /// <param name="list">Dictionary of key value pairs of error response.</param>
        private void GetErrors(string jsonInput, ref Dictionary<string, string> list)
        {

            IDictionary<string, object> errorObject = JsonConvert.DeserializeObject<IDictionary<string,object>> (jsonInput);

            if (null != errorObject)
            {
                foreach (KeyValuePair<string, object> keyValue in errorObject)
                {
                    if (keyValue.Value != null)
                    {
                        if (keyValue.Value is IDictionary<string, object>)
                        {
                            list.Add(keyValue.Key, keyValue.Key);
                            this.GetKeyPairs((IDictionary<string, object>)keyValue.Value, ref list);
                        }
                        else
                        {
                            list.Add(keyValue.Key, keyValue.Value.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method loops through the contents of dictionary and generates a single dictionary of error response objects.
        /// </summary>
        /// <param name="values">Dictionary of key and object pairs</param>
        /// <param name="list">Dictionary of key value pairs of error response.</param>
        private void GetKeyPairs(IDictionary<string, object> values, ref Dictionary<string, string> list)
        {
            if (null != values)
            {
                foreach (KeyValuePair<string, object> keyValue in values)
                {
                    if (keyValue.Value != null)
                    {
                        if (keyValue.Value is IDictionary<string, object>)
                        {   
                            list.Add(keyValue.Key, keyValue.Key);
                            this.GetKeyPairs((IDictionary<string, object>)keyValue.Value, ref list);
                        }
                        else
                        {
                            list.Add(keyValue.Key, keyValue.Value.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method parses given error response and constructs ErrorType and ErrorObject.
        /// </summary>
        /// <param name="errorResponse">JSON error response.</param>
        internal void ParseErrorResponse(string errorResponse)
        {
            try
            {
                Dictionary<string, string> errorValues = new Dictionary<string, string>();

                this.GetErrors(errorResponse, ref errorValues);

                ServiceExceptionObject serviceExceptionObject = null;

                PolicyExceptionObject policyExceptionObject = null;

                ServiceErrorObject serviceErrorObject = null;

                if (null != errorValues && errorValues.Count > 0)
                {
                    foreach (string key in errorValues.Keys)
                    {
                        switch (key.ToLower())
                        {
                            case "serviceerror":
                                this.ErrorType = errorValues[key];
                                serviceErrorObject = new ServiceErrorObject();
                                this.ErrorObject = serviceErrorObject;
                                break;
                            case "serviceexception":
                                this.ErrorType = errorValues[key];
                                serviceExceptionObject = new ServiceExceptionObject();
                                this.ErrorObject = serviceExceptionObject;
                                break;
                            case "policyexception":
                                this.ErrorType = errorValues[key];
                                policyExceptionObject = new PolicyExceptionObject();
                                this.ErrorObject = policyExceptionObject;
                                break;
                        }

                        if (!string.IsNullOrEmpty(this.ErrorType))
                        {
                            break;
                        }
                    }

                    foreach (string key in errorValues.Keys)
                    {
                        switch (key.ToLower())
                        {
                            case "errorid":
                            case "messageid":
                                if (null != serviceExceptionObject)
                                {
                                    serviceExceptionObject.MessageId = errorValues[key];
                                }
                                else
                                {
                                    policyExceptionObject.MessageId = errorValues[key];
                                }

                                break;
                            case "message":
                            case "text":
                                if (null != serviceExceptionObject)
                                {
                                    serviceExceptionObject.Text = errorValues[key];
                                }
                                else
                                {
                                    policyExceptionObject.Text = errorValues[key];
                                }

                                break;
                            case "faultcode":
                                serviceErrorObject.FaultCode = errorValues[key];
                                break;
                            case "faultdescription":
                                serviceErrorObject.FaultDescription = errorValues[key];
                                break;
                            case "faultmessage":
                                serviceErrorObject.FaultMessage = errorValues[key];
                                break;
                            case "variables":
                                if (null != serviceExceptionObject)
                                {
                                    serviceExceptionObject.Variables = errorValues[key];
                                }
                                else
                                {
                                    policyExceptionObject.Variables = errorValues[key];
                                }

                                break;
                        }
                    }
                }
            }
            catch
            { }
        }
    }
}
