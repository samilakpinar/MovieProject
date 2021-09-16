using Business.Models;
using System.Collections.Generic;

namespace Business.Responses
{
    public class PopulerCastResponse
    {
        public int Id { get; set; }
        public List<Cast> Cast { get; set; }
    }
}
