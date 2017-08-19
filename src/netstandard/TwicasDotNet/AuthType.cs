using System;

namespace TwicasDotNet
{
    public enum AuthType
    {
        ServerLess,
        Server
    }

    public static class ExDeviceType
    {
        public static string GetResponsTypeText(this AuthType devicetype)
        {
            switch (devicetype)
            {
                case AuthType.ServerLess:
                    return "token";
                case AuthType.Server:
                    return "code";
                default:
                    throw new ArgumentException();
            }
        }
    }
}