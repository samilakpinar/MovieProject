namespace Business.Responses
{
    public class BaseResponse<T>
    {
        public bool IsSuccess => Data != null && ErrorMessages == null ? true : false;
        public string ErrorMessages { get; set; } 
        public T Data { get; set; } 

    }

}

