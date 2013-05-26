using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFileOAuth;

namespace SmartFileOAuthTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string consumer_key;
            string consumer_secret;
            string request_token;
            string request_secret;
            string access_token;
            string access_secret;

            //Step 1) Get consumer key and secret.
            Console.WriteLine("Consumer Key?");
            consumer_key = Console.ReadLine();
            Console.WriteLine("Consumer Secret?");
            consumer_secret = Console.ReadLine();

            //Start the OAuth Process.
            OAuth con = new OAuth(consumer_key, consumer_secret);
            
            //Step 2) Obtain the request cred.
            con.GetRequestToken();

            //Get verificationCode back from user.
            Console.WriteLine(con.GetAuthorizationUrl().ToString());
            string ver = Console.ReadLine();

            //Step 3) Obtain the access cred.
            con.GetAccessToken(ver);

            //Step 4) Do and API call.
        }
    }
}
