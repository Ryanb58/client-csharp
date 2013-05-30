using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFileOAuth;
using System.Diagnostics;

namespace SmartFileOAuthTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string consumer_key;
            string consumer_secret;
            string access_token;
            string access_secret;

            //Step 1) Get consumer key and secret.
            Console.WriteLine("Consumer Key?");
            consumer_key = Console.ReadLine();
            Console.WriteLine("Consumer Secret?");
            consumer_secret = Console.ReadLine();

            //Start the OAuth Process.
            OAuth con = new OAuth(consumer_key, consumer_secret);

            //Step 2) Obtain the Authorization URL.
            string getAuthUrl = con.GetAuthorizationUrl();
            Console.WriteLine(getAuthUrl);
            Process.Start(getAuthUrl);

            //Step 3) Get verificationCode back from user.
            Console.WriteLine("Verification Code:");
            string ver = Console.ReadLine();

            //Step 4) Obtain the access credentials.
            con.GetAccess(ver);

            //Step 5) Store away the access token and secret.
            access_token = con.GetAccessToken().ToString();
            access_secret = con.GetAccessSecret().ToString();

            Console.WriteLine("Access Token : " + con.GetAccessToken().ToString());
            Console.WriteLine("Access Secret : " + con.GetAccessSecret().ToString());
            
            //Step 6) API call.
            dynamic jsonObj = con.Get("api/2/ping");
            Console.WriteLine("Ping: " + jsonObj.ping.ToString());

            // Close the application.
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
        }
    }
}
