using System;
using System.Linq;

namespace TwicasDotNet
{
    public class CallbackAnalyzer
    {
        private string sampleCallBackURL;

        public CallbackAnalyzer(string sampleCallBackURL)
        {
            this.sampleCallBackURL = sampleCallBackURL;
        }

        public string getToken =>
             string.IsNullOrWhiteSpace(sampleCallBackURL) ?
             throw new NullReferenceException() :
             sampleCallBackURL
             .Split('#', '&')
             .Where(s => s.Contains("access_token"))
             .Select(s => s.Replace("access_token=", ""))
             .FirstOrDefault();




        public bool isLoginSuccess() =>
                              string.IsNullOrWhiteSpace(sampleCallBackURL) ?
                              throw new NullReferenceException() :
                              sampleCallBackURL
                              .Split('#', '&')
                              .Where(s => s.Contains("access_token"))
                              .Count() > 0;


    }
}
