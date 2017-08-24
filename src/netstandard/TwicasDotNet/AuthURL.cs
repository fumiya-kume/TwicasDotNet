using System;

namespace TwicasDotNet
{
    public static class AuthURL
    {
        public static string GetImplicitAuthURL(string clientID)
        {
            if (string.IsNullOrWhiteSpace(clientID)) throw new NullReferenceException();
            return $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={clientID}&response_type=token";
        }

        public static string GetGrantAuthURL(string clientID)
        {
            if (string.IsNullOrWhiteSpace(clientID)) throw new NullReferenceException();
            return $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={clientID}&response_type=code";
        }
    }
}
