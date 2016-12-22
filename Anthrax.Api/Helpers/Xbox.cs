using Anthrax.Lib.Extensions;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Anthrax.Api.Helpers
{
    /// <summary>
    /// Encapsulates functions for getting Xbox data and invoking functionality.
    /// This wouldn't have been possible without https://github.com/Schamper who did this first with their python script.
    /// I also need to thank Bailey Miller @StackOverflow - http://stackoverflow.com/questions/40200429/c-sharp-socket-implementation-in-uwp-doesnt-work-compared-to-previous-implement
    /// </summary>
    public static class Xbox
    {
        private const int _XboxPort = 5050; // the port the Xbox accepts communications on
        private const string _PowerHeader = "dd02001300000010"; // this is the prefix for the "power-on" packet in hex
        private const string _PingPacket = "dd00000a000000000000000400000002"; // this is the ping packet converted to hex
        private static string _xboxLiveId => ConfigurationManager.AppSettings["XboxLiveId"];

        /// <summary>
        /// Creates a new Xbox socket and attempts to ping the device.
        /// If packet is received, Xbox is obviously online.
        /// </summary>
        /// <param name="timeout">The amount in milliseconds to wait before assuming Xbox is offline.</param>
        /// <returns>True if packet is received, false otherwise.</returns>
        public static bool IsReachable(int timeout)
        {
            using (var socket = CreateXboxSocket())
            {
                try
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, 0));
                    socket.Connect(Network.GetPublicIP(), _XboxPort);
                    socket.Send(_PingPacket.ToByteArray());

                    var buffer = new byte[short.MaxValue];

                    socket.ReceiveTimeout = timeout;
                    var len = socket.Receive(buffer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Creates a new Xbox socket and attempts to turn the device on.
        /// Currently unknown if possible to turn Xbox off.
        /// </summary>
        /// <returns>True if no exceptions, not guaranteed to have powered the device.</returns>
        public static bool TogglePower(int attemptCount = 10)
        {
            using (var socket = CreateXboxSocket())
            {
                try
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, 0));
                    socket.Connect(Network.GetPublicIP(), _XboxPort);

                    var idBuffer = Encoding.UTF8.GetBytes(_xboxLiveId + "\x00");
                    for (var i = 0; i < attemptCount; i++)
                    {
                        socket.Send(_PowerHeader.ToByteArray().Concat(idBuffer).ToArray());
                        if (IsReachable(1000))
                            break;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private static Socket CreateXboxSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
    }
}