using System;
using TwicasDotNet;

namespace TwicasDotNet
{
    public class AuthClient
    {
        public string ClientID { get; set; } = "";
        public string CSRF_Token { get; set; } = "";
        public string GetAuthURL(string ClientID = "", string CSRF_Token = "")
        {
            if (string.IsNullOrWhiteSpace(ClientID) || string.IsNullOrWhiteSpace(CSRF_Token)) throw new ArgumentException();
            return $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={ClientID}&response_type=code&state={CSRF_Token}";
        }
    }
}
