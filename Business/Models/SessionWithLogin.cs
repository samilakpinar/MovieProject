using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SessionWithLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string request_token { get; set; }
    }
}
