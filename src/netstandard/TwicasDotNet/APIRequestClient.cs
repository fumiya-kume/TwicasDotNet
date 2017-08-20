using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwicasDotNet
{
    public class APIRequestClient
    {
        private string accessToken;

        public APIRequestClient(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<UserObject> getUserInfo(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException();
            var requestURL = $"{Config.BaseURL}/users/{userName}";
            HttpRequestMessage httpRequestMessage = SetupHttpHeader(requestURL);

            using (var client = new HttpClient())
            {
                var result = await client.SendAsync(httpRequestMessage);
                var json = await result.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json)) throw new HttpRequestException(json);
                UserObject userObject = null;
                try
                {
                    userObject = JsonConvert.DeserializeObject<UserObject>(json);
                }
                catch (Exception e)
                {
                    throw e;
                }
                return userObject;
            }
        }

        private HttpRequestMessage SetupHttpHeader(string requestURL)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpRequestMessage.Headers.Add("X-Api-Version", "2.0");
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + accessToken);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.RequestUri = new Uri(requestURL);
            return httpRequestMessage;
        }
    }
}
