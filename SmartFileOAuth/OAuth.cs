using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFileOAuth
{
    class OAuth
    {

        string baseUrl;

        string requestUrl;
        string userAuthorizeUrl;
        string accessUrl;

        string consumer_key;
        string consumer_secret;
        string request_token;
        string request_secret;
        string access_token;
        string access_secret;

        //Constructor
        public OAuth(string consumer_key, string consumer_secret)
        {
            Properties.Settings.Default.consumer_key = consumer_key;
            Properties.Settings.Default.consumer_secret = consumer_secret;
            LoadSettings();
        }

        public void LoadSettings()
        {
            try
            {
                this.baseUrl = Properties.Settings.Default.baseUrl.ToString();
                this.requestUrl = Properties.Settings.Default.requestUrl.ToString();
                this.userAuthorizeUrl = Properties.Settings.Default.authorizationUrl.ToString();
                this.accessUrl = Properties.Settings.Default.accessUrl.ToString();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
