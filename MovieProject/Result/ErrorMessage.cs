namespace MovieProject.Result
{
    public class ErrorMessage : IErrorMessage
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }


        public static IErrorMessage Create(string message, string type, string code)
            => new ErrorMessage { Message = message, Type = type, Code = code };

        public static IErrorMessage Create(string message, string type)
            => new ErrorMessage { Message = message, Type = type };

        public static IErrorMessage Create(string message)
            => new ErrorMessage { Message = message };

    }
}
