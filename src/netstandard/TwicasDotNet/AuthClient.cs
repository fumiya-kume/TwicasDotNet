﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            return $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={ClientID}&response_type=token&state={CSRF_Token}";
        }

        public string GetAccessTokenFromCallbackURL(string callbackURL)
        {
            return callbackURL
                .Split('#', '&')
                .Where(s => s.Contains("access_token"))
                .Select(s => s.Replace("access_token=", ""))
                .First();
        }
    }
}