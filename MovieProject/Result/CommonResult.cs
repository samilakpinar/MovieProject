using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MovieProject.Result
{
    public class CommonResult<T> : ICommonResult<T> where T : class
    {
        public int? HttpStatusCode { get; set; }

        public bool Result => !IsError;

        public T Data { get; internal set; }

        public List<IErrorMessage> ErrorMessages { get; private set; }

        public bool IsError
        {
            get
            {
                return (ErrorMessages != null && ErrorMessages.Count > 0);
            }
        }

        public CommonResult(T data, int? httpStatusCode = null, List<IErrorMessage> errorMessages = null)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            ErrorMessages = errorMessages ?? new List<IErrorMessage>();
        }
        public string ErrorsWithCode(string lineBreak = "\n")
        {
            StringBuilder bld = new StringBuilder();
            foreach (ErrorMessage error in ErrorMessages)
                bld.Append((string.IsNullOrEmpty(bld.ToString()) ? "" : lineBreak) + error.Code + " - " + error.Message);
            return bld.ToString();
        }

        public string Errors(string lineBreak = "\n")
        {
            StringBuilder bld = new StringBuilder();
            foreach (ErrorMessage error in ErrorMessages)
                bld.Append((string.IsNullOrEmpty(bld.ToString()) ? "" : lineBreak) + error.Message);

            return bld.ToString();

        }

        public void AddErrors(params string[] errorMessages) =>
            ErrorMessages = errorMessages.Select(ErrorMessage.Create) as List<IErrorMessage>;


        public static ICommonResult<T> CreateError(params string[] validationErrors) =>
            new CommonResult<T>(null, (int)System.Net.HttpStatusCode.BadRequest, validationErrors.Select(ErrorMessage.Create).ToList());

        public static ICommonResult<T> CreateError(HttpStatusCode httpStatusCode, params string[] errorMessages) =>
            new CommonResult<T>(null, (int)httpStatusCode, errorMessages.Select(ErrorMessage.Create) as List<IErrorMessage>);


        public static ICommonResult<T> CreateError(HttpStatusCode httpStatusCode, params IErrorMessage[] errorMessages)
        {
            return new CommonResult<T>(null, (int)httpStatusCode, new List<IErrorMessage>(errorMessages.Select(p => new ErrorMessage
            {
                Message = p.Message,
                Code = p.Code,
                Type = p.Type
            })));
        }

        public static ICommonResult<T> CreateResult(T data) =>
            new CommonResult<T>(data, 200);


    }
}
