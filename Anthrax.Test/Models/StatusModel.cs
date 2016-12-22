using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anthrax.Test.Models
{
    public class StatusModel : AnthraxModel
    {
        public string IPAddress { get; set; }
        public bool XboxOnline { get; set; }

        public StatusModel(dynamic data)
        {
            IPAddress = data.IPAddress;
            XboxOnline = data.XboxOnline;
        }
    }
}