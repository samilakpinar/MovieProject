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
        public string ErrorMessages { get; set; } //dolu ise ıs IsSucess false olur.
        public T Data { get; set; } // dolu ise isSuccess true olur. + 

    }

    }
