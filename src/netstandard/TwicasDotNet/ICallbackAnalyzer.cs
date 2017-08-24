namespace TwicasDotNet
{
    public interface ICallbackAnalyzer
    {
        string getToken { get; }

        bool isLoginSuccess();
    }
}