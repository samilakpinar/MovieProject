using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses
{
    public class ResultResponse
    {
        public bool Status => ErrorMessage == null || ErrorMessage == "" ? true : false;
        public string ErrorMessage { get; set; }
    }
}
