using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class RateMovie
    {
        public int MovieId { get; set; }
        public string SessionId { get; set; }
        public string GuestId { get; set; }
        public int value { get; set; }
        public string Note { get; set; }
    }
}
