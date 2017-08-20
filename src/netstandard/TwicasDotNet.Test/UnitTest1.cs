using System;
using System.Linq;
using Xunit;

namespace TwicasDotNet.Test
{
    public class UnitTest1
    {
        private static string ClientKey = TestSetting.ClientID;
        private AuthClient client => new AuthClient();

        public class ImplicintなAuthURLを取得する
        {
            [Fact]
            public void ImplicitAuthするURLを取得する()
            {
                var guid = Guid.NewGuid();
                Assert.Equal("https://apiv2.twitcasting.tv/oauth2/authorize?client_id=" + guid.ToString() + "&response_type=token", AuthClient.GetImplicitAuthURL(guid.ToString()));
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            public void ImplicitAuthするURLをNULLを渡して初期化(string ClientID)
            {
                Assert.Throws<NullReferenceException>(() => AuthClient.GetImplicitAuthURL(ClientID));
            }
        }

        public class ImplicintなAuthURLを解析する
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void ImplicitなAuthの空白なコールバックURLを解析(string CallbackURL)
            {
                var analyzer = new CallbackAnalyzer(CallbackURL);
                Assert.Throws<NullReferenceException>(() => analyzer.isLoginSuccess());
                Assert.Throws<NullReferenceException>(() => analyzer.getToken);
            }

            [Fact]
            public void ImplicitなAuthの正常なコールバックURLを解析()
            {
                var accessToken = "hugo";
                var SampleCallbackURL = $"twicas://MainPage?#access_token={accessToken}&token_type=bearer&expires_in=15552000";
                var analyzer = new CallbackAnalyzer(SampleCallbackURL);
                Assert.Equal(true, analyzer.isLoginSuccess());
                Assert.Equal(accessToken, analyzer.getToken);
            }
        }

        public class ユーザーの情報を取得する
        {
            [Fact]
            public async void 正常系()
            {
                var userid = "yonex";
                var accessToken = TestSetting.AccessToken;
                var apiclient = new APIRequestClient(accessToken);
                UserObject result = await apiclient.getUserInfo(userid);
                Assert.NotNull(result);
                Assert.NotNull(result.user);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public async void 異常系(string userID)
            {
                var accessToken = TestSetting.AccessToken;
                var apiclient = new APIRequestClient(accessToken);
                await Assert.ThrowsAsync<ArgumentException>(async () => await apiclient.getUserInfo(userID));
            }
        }
    }
}
