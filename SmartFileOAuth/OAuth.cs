using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OAuth;
using System.IO;

namespace SmartFileOAuth
{
    public class OAuth
    {
        private string baseUrl;
        private string requestUrl;
        private string userAuthorizeUrl;
        private string accessUrl;

        private string consumer_key;
        private string consumer_secret;
        private string request_token;
        private string request_secret;
        private string access_token;
        private string access_secret;

        OAuthRequest client;

        //Stream currentResponse;

        //Constructor
        public OAuth(string consumer_key, string consumer_secret)
        {
            this.baseUrl = "https://app.smartfile.com/";

            this.requestUrl = "https://app.smartfile.com/oauth/request_token/";
            this.userAuthorizeUrl = "https://app.smartfile.com/oauth/authorize/";
            this.accessUrl = "https://app.smartfile.com/oauth/access_token/";
            this.consumer_key = consumer_key;
            this.consumer_secret = consumer_secret;

            this.client = OAuthRequest.ForRequestToken(this.consumer_key, this.consumer_secret);

        }

        #region Authorization Process

        public string GetAuthorizationUrl()
        {
            this.client.RequestUrl = requestUrl;

            // Using URL query authorization
            string auth = this.client.GetAuthorizationQuery();
            var url = this.client.RequestUrl + "?" + auth;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            //this.currentResponse = response.GetResponseStream();

            //Get String from Query Stream.
            string authorizationPara = StreamToString(response.GetResponseStream());
            string authorizationUrl;

            //Remove secret from parameters...
            //string[] query;
            List<string> query = new List<string>();
            query = QueryToArray(authorizationPara);

            //Set token secret and token.
            this.request_secret = query[1];
            this.request_token = query[3];

            //Construct parameters...
            authorizationPara = "oauth_token=" + this.request_token;

            //Construct Auth URL.
            authorizationUrl = this.userAuthorizeUrl + "?" + authorizationPara;

            //Console.WriteLine("Authorization Url: {0}", authorizationUrl);

            return authorizationUrl;
        }

        public void GetAccess(string verificationCode)
        {
            this.client = OAuthRequest.ForAccessToken(this.consumer_key, this.consumer_secret, this.request_token, this.request_secret, verificationCode);
            this.client.RequestUrl = this.accessUrl;

            // Using URL query authorization
            string auth2 = this.client.GetAuthorizationQuery();
            var url2 = this.client.RequestUrl + "?" + auth2;
            var request2 = (HttpWebRequest)WebRequest.Create(url2);
            var response2 = (HttpWebResponse)request2.GetResponse();

            //Console.WriteLine(url2);
            //Console.WriteLine("STREAM!");

            //Console.WriteLine(StreamToString(response2.GetResponseStream()));

            string authQuery = StreamToString(response2.GetResponseStream());

            Console.WriteLine(authQuery);

            //Seperate the stream into an Array.
            List<string> query = new List<string>();

            //TODO: Fix ERROR!
            query = QueryToArray(authQuery);
            //Assign each query to their value.
            access_token = query[3];
            access_secret = query[1];

            //Console.WriteLine("Access Token: " + access_token);
            //Console.WriteLine("Access Secret: " + access_secret);
        }

        #endregion
        
        #region Post, Get, Put, and Delete.

        #endregion

        #region Return Private Info.

        public string GetAccessToken()
        {
            return this.access_token;
        }
        public string GetAccessSecret()
        {
            return this.access_secret;
        }

        #endregion

        #region Util
        private List<string> QueryToArray(String original)
        {
            //Remove secret from parameters...
            char[] delimiterChars = { '&', '?', '=' };

            //System.Console.WriteLine("Original text: '{0}'", original);

            List<string> query = new List<string>();
            string[] queryArray = original.Split(delimiterChars);
            query.AddRange(queryArray);
            /*
            System.Console.WriteLine("{0} words in text:", query.Length);

            
            //Print out the words.
            
            foreach (string s in words)
            {
                System.Console.WriteLine(s);
            }
            */
            return query;
        }
        private string StreamToString(Stream original)
        {
            StreamReader streamReader = new StreamReader(original, true);
            string streamString;
            try
            {
                //Console.WriteLine(streamReader.ReadToEnd());
                streamString = streamReader.ReadToEnd();
            }
            finally
            {
                streamReader.Close();
                original.Close();
            }

            return streamString;
        }
        #endregion
    }
}
