using System.Net;

namespace Anthrax.Api.Helpers
{
    public static class Network
    {
        /// <summary>Pulls IP from iCanHazIP.</summary>
        public static string GetPublicIP()
        {
            using (var wc = new WebClient())
                return wc.DownloadString("http://icanhazip.com").Trim();
        }
    }
}