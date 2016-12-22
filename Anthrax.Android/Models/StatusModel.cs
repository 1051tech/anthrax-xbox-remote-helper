namespace Anthrax.Android.Models
{
    public class StatusModel : AnthraxModel
    {
        public string IPAddress { get; set; }
        public bool XboxOnline { get; set; }

        /// <summary>
        /// Deserializes dynamic data from response into strong data.
        /// </summary>
        public StatusModel(dynamic data)
        {
            IPAddress = data.IPAddress;
            XboxOnline = data.XboxOnline;
        }
    }
}