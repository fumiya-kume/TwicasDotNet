namespace TwicasDotNet
{
    public interface ICallbackAnalyzer
    {
        string GetToken { get; }

        bool IsLoginSuccess();
    }
}