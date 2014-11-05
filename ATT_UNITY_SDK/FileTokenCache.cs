using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ATT_UNITY_SDK
{
    class FileTokenCache : ITokenCache
    {
        internal String tokenFilePath;
        private static readonly object _locker = new Object();

        public FileTokenCache(string path)
        {
            Debug.WriteLine("FileTokenCache called");
            Debug.WriteLine("token cache file at " + path);

            this.tokenFilePath = path;
        }

        /// <summary>
        /// Get an authorization token from a file
        /// </summary>
        public OAuthToken LoadToken()
        {
            Debug.WriteLine("LoadToken called");
            return ReadTokenFromFile(this.tokenFilePath);
        }

        /// <summary>
        /// Store an authorization token in a file
        /// </summary>
        /// <param name="token"></param>
        public void SaveToken(OAuthToken token)
        {
            Debug.WriteLine("SaveToken called");
            WriteTokenToFile(token, this.tokenFilePath);
        }

        internal static OAuthToken ReadTokenFromFile(string tokenFilePath)
        {
            Debug.WriteLine("ReadTokenFromFile called");
            Debug.WriteLine("token cache file at " + tokenFilePath);

            lock (_locker)
            {
                if (!File.Exists(tokenFilePath))
                {
                    Debug.WriteLine("token file does not exist");
                    throw new InvalidTokenCacheException("token file does not exist: " + tokenFilePath);
                }

                string[] tokens;
                try
                {
                    tokens = File.ReadAllLines(tokenFilePath);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine("could not read from token file");
                    throw new InvalidTokenCacheException("could not read from token file: " + tokenFilePath, ex);
                }

                if (tokens == null || tokens.Length != 4)
                {
                    Debug.WriteLine("invalid token data in file");
                    throw new InvalidTokenCacheException("Invalid token data in file: " + tokenFilePath);
                }

                try
                {
                    Debug.WriteLine("read AccessToken: " + tokens[0]);
                    Debug.WriteLine("read RefreshToken: " + tokens[1]);
                    Debug.WriteLine("read ExpiresIn: " + tokens[2]);
                    Debug.WriteLine("read WriteTimestamp: " + tokens[3]);

                    return new OAuthToken(tokens[0], tokens[1], tokens[2], tokens[3]);
                }
                catch (ArgumentException ex)
                {
                    Debug.WriteLine("invalid token data in file");
                    throw new InvalidTokenCacheException("Invalid token data in file: " + tokenFilePath, ex);
                }
            }
        }

        internal static void WriteTokenToFile(OAuthToken token, string tokenFilePath)
        {
            Debug.WriteLine("WriteTokenToFile called");
            Debug.WriteLine("token cache file at " + tokenFilePath);

            if (token == null)
            {
                Debug.WriteLine("null token passed");
                throw new ArgumentNullException("token");
            }

            String[] tokens = new String[4];
            tokens[0] = token.AccessToken;
            tokens[1] = token.RefreshToken;
            tokens[2] = token.ExpiresIn.ToString();
            tokens[3] = DateTime.Now.ToString();

            Debug.WriteLine("writing AccessToken: " + tokens[0]);
            Debug.WriteLine("writing RefreshToken: " + tokens[1]);
            Debug.WriteLine("writing ExpiresIn: " + tokens[2]);
            Debug.WriteLine("writing WriteTimestamp: " + tokens[3]);

            lock (_locker)
            {
                try
                {
                    if (File.Exists(tokenFilePath))
                    {
                        File.WriteAllLines(tokenFilePath, tokens);
                        Debug.WriteLine("token written to file");
                    }
                    else
                    {
                        Debug.WriteLine("no existing token file, creating a new one");
                        Directory.CreateDirectory(Path.GetDirectoryName(tokenFilePath));
                        File.AppendAllText(tokenFilePath, String.Join("\n", tokens));
                    }
                }
                catch (IOException ex)
                {
                    Debug.WriteLine("could not write token to file");
                    throw new InvalidTokenCacheException("Could not write token to file: " + tokenFilePath, ex);
                }
            }
        }
    }
}
