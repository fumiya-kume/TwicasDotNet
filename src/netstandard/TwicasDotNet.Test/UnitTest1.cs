using System;
using System.Linq;
using Xunit;

namespace TwicasDotNet.Test
{
    public class UnitTest1
    {
        private AuthClient client => new AuthClient();

        [Fact]
        public void AutoURLが正しく発行されるかテスト()
        {
            Assert.Throws(typeof(ArgumentException), () => client.GetAuthURL("", ""));
            Assert.Equal(client.GetAuthURL("Hello", "World"), $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id=Hello&response_type=token&state=World");
        }

        [Fact]
        public void CallbackURLからアクセストークンを取得できるかテスト()
        {
            Assert.Equal("Hello", client.GetAccessTokenFromCallbackURL("http://example.com/#access_token=Hello&token_type=bearer&expires_in=15552000"));
            //Assert.Equal("", client.GetAccessTokenFromCallbackURL("http://example.com/#result=denied"));
        }
    }
}
