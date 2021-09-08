using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses
{
    public class BaseResponse<T>
    {
        public bool IsSuccess => Data != null && ErrorMessages == null ? true : false;
        public string ErrorMessages { get; set; } 
        public T Data { get; set; } 

    }

}

