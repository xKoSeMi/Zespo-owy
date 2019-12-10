using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektZespolowy2.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public bool isAuthenticated { get; set; }
        public string Code { get; set; }

        public List<MAC> Macs { get; set; }
        public List<Browser> Browsers { get; set; }
    }
}