using System;

namespace TwicasDotNet
{
    public enum DeviceType
    {
        ServerLess,
        Server
    }

    public static class ExDeviceType
    {
        public static string GetResponsTypeText(this DeviceType devicetype)
        {
            switch (devicetype)
            {
                case DeviceType.ServerLess:
                    return "token";
                case DeviceType.Server:
                    return "code";
                default:
                    throw new ArgumentException();
            }
        }
    }
}