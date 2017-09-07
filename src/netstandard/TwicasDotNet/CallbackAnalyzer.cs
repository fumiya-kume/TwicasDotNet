using System;
using System.Linq;

namespace TwicasDotNet
{
    public class CallbackAnalyzer : ICallbackAnalyzer
    {
        private string sampleCallBackURL;

        public CallbackAnalyzer(string sampleCallBackURL)
        {
            this.sampleCallBackURL = sampleCallBackURL;
        }

        public string GetToken =>
             string.IsNullOrWhiteSpace(sampleCallBackURL) ?
             throw new NullReferenceException() :
             sampleCallBackURL
             .Split('#', '&')
             .Where(s => s.Contains("access_token"))
             .Select(s => s.Replace("access_token=", ""))
             .FirstOrDefault();
        
        public bool IsLoginSuccess() =>
            string.IsNullOrWhiteSpace(sampleCallBackURL) ?
            throw new NullReferenceException() :
            sampleCallBackURL
            .Split('#', '&')
            .Where(s => s.Contains("access_token"))
            .Count() > 0;
    }
}
