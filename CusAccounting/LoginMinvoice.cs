using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace CusAccounting
{
    public class LoginMinvoice
    {
        private static JObject _Login(string username, string password, string mst, string urlLogin)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            JObject json = new JObject
            {
                {"username",username},
                {"password",password},
                {"ma_dvcs","VP"}
            };
            var token = client.UploadString(urlLogin, json.ToString());
            return JObject.Parse(token);
        }
        private static void CreateAuthorization(WebClient webClient, string username, string pass, string mst, string urlLogin)
        {
            var tokenJson = _Login(username, pass, mst, urlLogin);
            var authorization = "Bear " + tokenJson["token"] + ";VP;vi";
            webClient.Headers[HttpRequestHeader.Authorization] = authorization;
        }
        public static WebClient SetupWebClient(string userName, string passWord, string mst, string urlLogin)
        {
            var webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");

            CreateAuthorization(webClient, userName, passWord, mst, urlLogin);
            return webClient;
        }
    }
}
