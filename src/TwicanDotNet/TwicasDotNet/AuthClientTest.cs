using Xunit;
using TwicanDotNet;
using System;

namespace TwicasDotNet
{
    public class UnitTest1
    {
        private AuthClient client => new AuthClient();

        [Fact]
        public void AutoURLが正しく発行されるかテスト()
        {
            Assert.Throws(typeof(ArgumentException), () => client.GetAuthURL("", ""));
            Assert.Equal(client.GetAuthURL("Hello", "World"), $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id=Hello&response_type=code&state=World");
        }
    }
}
