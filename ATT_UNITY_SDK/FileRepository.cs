// Licensed by AT&T under 'Software Development Kit Tools Agreement.' v14, Jan 2013
// TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION: http://developer.att.com/sdk_agreement/
// Copyright 2013 AT&T Intellectual Property. All rights reserved. http://developer.att.com
// For more information contact developer.support@att.com
// -----------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="AT&amp;T Intellectual Property">
// Copyright 2013 AT&amp;T Intellectual Property. All rights reserved. http://developer.att.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace ATT_UNITY_SDK
{
    /// <summary>
    /// 
    /// </summary>
    public class FileRepository : IRepository
    {
        /// <summary>
        /// Name of the file to save OAuthToken.
        /// </summary>
        private string fileToSave;

        /// <summary>
        /// Create an instance of FileRepository.
        /// </summary>
        /// <param name="fileToSave">Name of the file to save OAuthtoken information to.</param>
        public FileRepository(string fileToSave)
        {
            this.fileToSave = fileToSave;
        }

        /// <summary>
        /// Save OAuthToken to the file.
        /// </summary>
        /// <param name="oauthToken">OAuthToken instance.</param>
        public void SaveToken(OAuthToken oauthToken)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                fileStream = new FileStream(this.fileToSave, FileMode.OpenOrCreate, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(oauthToken.AccessToken);
                streamWriter.WriteLine(oauthToken.RefreshToken);
                streamWriter.WriteLine(oauthToken.CreationTime);
                streamWriter.WriteLine(oauthToken.ExpiresIn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != streamWriter)
                {
                    streamWriter.Close();
                }

                if (null != fileStream)
                {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// Get the OAuthToken from the file.
        /// </summary>
        /// <returns>An instance of OAuthToken</returns>
        public OAuthToken GetToken()
        {
            FileStream fileStream = null;
            StreamReader streamReader = null;
            OAuthToken oauthToken = null;
            try
            {
                if (System.IO.File.Exists(this.fileToSave))
                {
                    fileStream = new FileStream(this.fileToSave, FileMode.OpenOrCreate, FileAccess.Read);
                    streamReader = new StreamReader(fileStream);
                    string accessToken = streamReader.ReadLine();
                    string refreshToken = streamReader.ReadLine();
                    string creationTime = streamReader.ReadLine();
                    string expiresIn = streamReader.ReadLine();

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauthToken = new OAuthToken(accessToken, refreshToken, expiresIn, creationTime);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != streamReader)
                {
                    streamReader.Close();
                }

                if (null != fileStream)
                {
                    fileStream.Close();
                }
            }

            return oauthToken;
        }
    }
}
