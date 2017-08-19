using System;
using System.Collections.Generic;
using System.Linq;
using TwicasDotNet;

namespace TwicasDotNet
{
    public class AuthClient
    {
        public static string GetImplicitAuthURL(string clientID)
        {
            if (string.IsNullOrWhiteSpace(clientID)) throw new NullReferenceException();
            return $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={clientID}&response_type=token";
        }
    }
}
