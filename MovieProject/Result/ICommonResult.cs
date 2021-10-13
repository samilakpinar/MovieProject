using System.Collections.Generic;

namespace MovieProject.Result
{
    public interface ICommonResult<T> where T : class
    {
        int? HttpStatusCode { get; set; }
        bool Result { get; }
        T Data { get; }
        List<IErrorMessage> ErrorMessages { get; }
        bool IsError { get; }
        string ErrorsWithCode(string lineBreak = "\n");
        string Errors(string lineBreak = "\n");
        void AddErrors(params string[] errorMessages);
    }
}
