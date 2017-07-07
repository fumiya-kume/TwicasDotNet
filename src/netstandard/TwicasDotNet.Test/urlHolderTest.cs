using System;
using System.Linq;
using Xunit;
using TwicasDotNet;

namespace TwicasDotNet.Test
{
    public class urlHolderTest
    {
        [Fact]
        public void APIを呼ぶためのURLが取得できるか()
        {
            Assert.Equal("https://apiv2.twitcasting.tv", URLHolder.APICallBaseURL);
        }
    }
}
