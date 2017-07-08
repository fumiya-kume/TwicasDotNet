using System;
using System.Linq;
using Xunit;

namespace TwicasDotNet.Test
{
    public class UnitTest1
    {
        private AuthClient client => new AuthClient();

        public static class AuthURLが正しく発行されるかテスト
        {
            [Fact]
            public static void サーバータイプのAuthURLを取得するテスト()
            {
                var client = new AuthClient();
                Assert.Equal(client.GetAuthURL("Hello", DeviceType.Server), $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id=Hello&response_type=code&state=");
            }

            [Fact]
            public static void サーバーレスタイプのAuthURLを取得するテスト()
            {
                var client = new AuthClient();
                Assert.Equal(client.GetAuthURL("Hello", DeviceType.ServerLess), $"https://apiv2.twitcasting.tv/oauth2/authorize?client_id=Hello&response_type=token&state=");
            }

            [Fact]
            public static void クランとキーをセットしない場合のテスト()
            {
                var client = new AuthClient();
                Assert.Throws(typeof(ArgumentException), () => client.GetAuthURL(""));
            }
        }


        [Fact]
        public void CallbackURLからアクセストークンを取得できるかテスト()
        {
            Assert.Equal("Hello", client.GetAccessTokenFromCallbackURL("http://example.com/#access_token=Hello&token_type=bearer&expires_in=15552000"));
            Assert.Equal(null, client.GetAccessTokenFromCallbackURL("http://example.com/#result=denied"));
        }

        public static class デバイスのタイプからレスポンスタイプの指定する文字列を生成するのテスト
        {
            [Fact]
            public static void サーバーレスを表すテキストを表示するテスト()
            {
                Assert.Equal("token", DeviceType.ServerLess.GetResponsTypeText());
            }

            [Fact]
            public static void サーバーを表すテキストを表示するテスト()
            {
                Assert.Equal("code", DeviceType.Server.GetResponsTypeText());
            }
        }
    }
}
