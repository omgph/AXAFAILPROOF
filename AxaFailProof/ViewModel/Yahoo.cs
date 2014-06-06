using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Serialization.Json;

namespace Utils
{
    public static class Yahoo
    {
        private const string requestTokenURL = "https://api.login.yahoo.com/oauth/v2/get_request_token";
        private const string getTokenURL = "https://api.login.yahoo.com/oauth/v2/get_token";
        private const string calbackURL = "http://omgph.net/YahooCallBack/";
        private const String unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
        private const int timeadded = +8;

        private static String urlEncode(String value)
        {
            StringBuilder result = new StringBuilder();
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }
            return result.ToString();
        }

        private static String getSignature(String method, String url, String queryString, String tokenSecret = "")
        {
            String signatureBase = String.Concat(method, "&", urlEncode(url), "&", urlEncode(queryString));
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(
                String.Concat(
                    urlEncode(ConfigurationManager.AppSettings.Get("YahooConsumerSecret")) ,
                    "&",
                    urlEncode(tokenSecret)
                )
            );
            return urlEncode(Convert.ToBase64String(hmacsha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(signatureBase))));
        }

        public static String LogIn()
        {
            String queryString = String.Format("oauth_callback={0}&oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature_method=HMAC-SHA1&oauth_timestamp={3}&oauth_version=1.0",
                urlEncode(calbackURL),
                urlEncode(ConfigurationManager.AppSettings.Get("YahooConsumerKey")),
                Convert.ToBase64String(new ASCIIEncoding().GetBytes(Guid.NewGuid().ToString())),
                Convert.ToInt64((DateTime.Now.AddHours(timeadded) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString()
            );

            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(
                    String.Concat(
                        requestTokenURL, "?", queryString, "&oauth_signature=", getSignature("POST", requestTokenURL, queryString)
                    )
                );
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            try 
            {	        
                WebResponse response = webRequest.GetResponse();
                if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                    throw new Exception(
                        String.Format(
                        "Yahoo responded with: status code {0}, message:{1}",
                        ((HttpWebResponse)response).StatusCode,
                        ((HttpWebResponse)response).StatusDescription)
                        );
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    response = null;
                    webRequest = null;
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(reader.ReadToEnd());
                    if (qscoll["oauth_callback_confirmed"] != "true")
                        throw new Exception("Twitter oAuth callback was not confirmed!");
                    System.Diagnostics.Debug.WriteLine(string.Empty);
                    System.Diagnostics.Debug.WriteLine("******************************************");
                    foreach (String key in qscoll.AllKeys)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            String.Concat(key, ": ", qscoll[key])
                        );
                    }
                    System.Diagnostics.Debug.WriteLine("******************************************");
                    System.Diagnostics.Debug.WriteLine(string.Empty);

                    HttpCookie secret = new HttpCookie("oauth_token_secret", qscoll["oauth_token_secret"]);
                    secret.Expires = DateTime.Now.AddMinutes(1);
                    secret.HttpOnly = false;
                    HttpContext.Current.Response.Cookies.Add(secret);
                    System.Diagnostics.Debug.WriteLine(HttpContext.Current.Request.Cookies["oauth_token_secret"].Value);
                    return qscoll["xoauth_request_auth_url"];
                }
            }
            catch(WebException e)
            {
                System.Diagnostics.Debug.WriteLine(
                        "Yahoo server time: " + e.Response.Headers["Date"]
                    );
                if (!String.IsNullOrEmpty(e.Response.Headers["WWW-Authenticate"]))
                    throw new Exception(e.Response.Headers["WWW-Authenticate"]);
                else
                    throw;
            }
            catch (Exception)
            {
	            throw;
            }
        }

        public static YahooProfile GetUserData(String oauth_token, String oauth_verifier)
        {
            String oauth_token_secret = string.Empty;
            if (HttpContext.Current.Request.Cookies["oauth_token_secret"] != null)
                oauth_token_secret = HttpContext.Current.Request.Cookies["oauth_token_secret"].Value;
            else
                throw new Exception("Token expired!");
            System.Diagnostics.Debug.WriteLine("oauth_token_secret: " + oauth_token_secret);

            String queryString = String.Format(
                "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method=HMAC-SHA1&oauth_timestamp={2}&oauth_token={3}&oauth_verifier={4}&oauth_version=1.0",
                urlEncode(ConfigurationManager.AppSettings.Get("YahooConsumerKey")),
                Convert.ToBase64String(new ASCIIEncoding().GetBytes(Guid.NewGuid().ToString())),
                Convert.ToInt64((DateTime.Now.AddHours(timeadded) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString(),
                oauth_token,
                oauth_verifier
            );

            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(
                    String.Concat(
                        getTokenURL, "?", queryString, "&oauth_signature=", getSignature("POST", getTokenURL, queryString, oauth_token_secret)
                    )
                );
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            String userGuid = string.Empty, token = string.Empty;
            try
            {
                WebResponse response = webRequest.GetResponse();

                if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                    throw new Exception(
                        String.Format("Yahoo responded with: status code {0}, message:{1}",
                            ((HttpWebResponse)response).StatusCode,
                            ((HttpWebResponse)response).StatusDescription)
                        );

                
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    response = null;
                    webRequest = null;
                    NameValueCollection qscoll = HttpUtility.ParseQueryString(reader.ReadToEnd());
                    token = qscoll["oauth_token"];
                    oauth_token_secret = qscoll["oauth_token_secret"];
                    userGuid = qscoll["xoauth_yahoo_guid"];

                    System.Diagnostics.Debug.WriteLine(string.Empty);
                    System.Diagnostics.Debug.WriteLine("******************************************");
                    foreach (String key in qscoll.AllKeys)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            String.Concat(key, ": ", qscoll[key])
                        );
                    }
                    System.Diagnostics.Debug.WriteLine("******************************************");
                    System.Diagnostics.Debug.WriteLine(string.Empty);
                }

            }
            catch (WebException e)
            {
                if (!String.IsNullOrEmpty(e.Response.Headers["WWW-Authenticate"]))
                    throw new Exception(e.Response.Headers["WWW-Authenticate"]);
                else
                    throw;
            }
            catch (Exception)
            {
                throw;
            }

            String profileEndPoint = String.Concat("http://social.yahooapis.com/v1/user/", userGuid, "/profile");

            queryString = String.Format(
                "Authorization=OAuth&format=json&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method=HMAC-SHA1&oauth_timestamp={2}&oauth_token={3}&oauth_version=1.0&realm=yahooapis.com",
                urlEncode(ConfigurationManager.AppSettings.Get("YahooConsumerKey")),
                Convert.ToBase64String(new ASCIIEncoding().GetBytes(Guid.NewGuid().ToString())),
                Convert.ToInt64((DateTime.Now.AddHours(timeadded) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString(),
                urlEncode(token)
            );

            webRequest = (HttpWebRequest)System.Net.WebRequest.Create(
                    String.Concat(
                        profileEndPoint, "?", queryString, "&oauth_signature=", getSignature("GET", profileEndPoint, queryString, oauth_token_secret)
                    )
                );
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "GET";

            try
            {
                WebResponse response = webRequest.GetResponse();

                if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                    throw new Exception(
                        String.Format("Yahoo responded with: status code {0}, message:{1}",
                            ((HttpWebResponse)response).StatusCode,
                            ((HttpWebResponse)response).StatusDescription)
                        );


                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    YahooProfile profile = Deserialise<YahooProfile>(reader.ReadToEnd()); ;
                    response = null;
                    webRequest = null;
                    return profile;
                }

            }
            catch (WebException e)
            {
                if (!String.IsNullOrEmpty(e.Response.Headers["WWW-Authenticate"]))
                    throw new Exception(e.Response.Headers["WWW-Authenticate"]);
                else
                    throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static T Deserialise<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(T));
                return (T)serialiser.ReadObject(ms);
            }
        }
    }

    public class YahooProfile
    {
        public Profile profile { get; set; }

        public class Profile
        {
            public string uri { get; set; }
            public string guid { get; set; }
            public bool bdRestricted { get; set; }
            public string created { get; set; }
            public List<Email> emails { get; set; }
            public Image image { get; set; }
            public string memberSince { get; set; }
            public string nickname { get; set; }
            public string profileUrl { get; set; }
            public string timeZone { get; set; }
            public bool isConnected { get; set; }
        }

        public class Image
        {
            public int height { get; set; }
            public string imageUrl { get; set; }
            public string size { get; set; }
            public int width { get; set; }
        }

        public class Email
        {
            public string handle { get; set; }
            public int id { get; set; }
            public bool primary { get; set; }
            public string type { get; set; }
        }
    }
}