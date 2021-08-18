using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses
{
    class PopulerCastResponse
    {
        public int Id { get; set; }
        public List<Cast> Cast { get; set; }
    }
}
