using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using TwicasDotNet.Model;

namespace TwicasDotNet
{
    public class APIRequestClient : IAPIRequestClient
    {
        private string accessToken;

        public APIRequestClient(string accessToken)
        {
            this.accessToken = accessToken;
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

        

        public async Task<Stream> getLiveThinbnal(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID)) throw new ArgumentException();
            using (var client = new HttpClient())
            {
                var url = $"{Config.BaseURL}/users/{userID}/live/thumbnail";
                var httpMessage = SetupHttpHeader(url);
                var result = await client.SendAsync(httpMessage);
                var stream = await result.Content.ReadAsStreamAsync();
                return stream;
            }
        }

        public async Task<movieObject> getMovieInfo(string movieID)
        {
            if (string.IsNullOrWhiteSpace(movieID)) throw new ArgumentException();
            using (var client = new HttpClient())
            {
                var url = $"{Config.BaseURL}/movies/{movieID}";
                var httpMessage = SetupHttpHeader(url);

                var result = await client.SendAsync(httpMessage);
                var json = await result.Content.ReadAsStringAsync();
                var movieObject = JsonConvert.DeserializeObject<movieObject>(json);
                return movieObject;
            }
        }
    }
}
