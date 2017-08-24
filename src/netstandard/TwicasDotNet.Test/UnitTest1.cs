using System;
using System.Linq;
using TwicasDotNet.Model;
using Xunit;

namespace TwicasDotNet.Test
{
    public class UnitTest1
    {
        private static string ClientKey = TestSetting.ClientID;
        private AuthClient client => new AuthClient();

        public static APIRequestClient getAPIClient()
        {
            return new APIRequestClient(TestSetting.AccessToken);
        }

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

        public class GrantなAuthURLを取得する
        {
            [Fact]
            public void 適切なClientIDを引数に渡した場合()
            {
                var clientID = Guid.NewGuid().ToString();
                var ActualURL = $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id={clientID}&response_type=code";
                Assert.Equal(ActualURL, AuthClient.GetGrantAuthURL(clientID));
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            public void 不適切なClientIDを渡すと例外が発生する(string ClientID)
            {
                Assert.Throws<NullReferenceException>(() => AuthClient.GetImplicitAuthURL(ClientID));
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

    public class ライブ画像を取得する
    {
        [Fact]
        public async void 正常系()
        {
            var userID = "yonex";
            APIRequestClient apiClient = getAPIClient();
            var LiveTHunbailStream = await apiClient.getLiveThinbnal(userID);
            Assert.NotNull(LiveTHunbailStream);
            Assert.NotEqual(0, LiveTHunbailStream.Length);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void userIDが不正(string userID)
        {
            var apiClient = new APIRequestClient(TestSetting.AccessToken);
            await Assert.ThrowsAsync<ArgumentException>(async () => await apiClient.getLiveThinbnal(userID));
        }

    }

    public class ライブ情報を取得する
    {
        [Fact]
        public async void 正常系()
        {
            var movieID = TestSetting.MovieID;
            var apiclient = getAPIClient();
            movieObject LiveInfo = await apiclient.getMovieInfo(movieID);
            Assert.NotEqual("", LiveInfo.movie.id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void 異常系(string movieID)
        {
            var apiclient = getAPIClient();
            await Assert.ThrowsAsync<ArgumentException>(async () => await apiclient.getMovieInfo(movieID));
        }
    }
}
}
