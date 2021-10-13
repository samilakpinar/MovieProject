using System.Net;

namespace MovieProject.Result
{
    public class ServiceResult<T> where T : class
    {
        /// <summary>
        /// The HttpStatusCode
        /// </summary>
        public int? StatusCode { get; private set; }

        /// <summary>
        /// If no error returns true
        /// </summary>
        public bool Result => !IsError;

        /// <summary>
        /// Returns null if error occured
        /// </summary>
        public T Data { get; internal set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Returns true if any status code exist and this value
        /// </summary>
        public bool IsError => (StatusCode.HasValue && StatusCode.Value != (int)HttpStatusCode.OK);

        /// <summary>
        /// ServiceResult constructor with parameters
        /// </summary>
        /// <param name="data">Any class</param>
        /// <param name="statusCode">The httpStatusCode</param>
        /// <param name="errorMessage">Error Message</param>
        public ServiceResult(T data, int? statusCode = null, string errorMessage = null)
        {
            Data = data;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        #region "Factory Methods"

        /// <summary>
        /// Factory methods - Create new instance of ServiceResult passing the http status code and error messages
        /// </summary>
        /// <param name="statusCode">The httpStatusCode</param>
        /// <param name="errorMesagge">Error Message</param>
        /// <returns></returns>
        public static ServiceResult<T> CreateError(HttpStatusCode statusCode, string errorMesagge)
            => new ServiceResult<T>(null, (int)statusCode, errorMesagge);

        /// <summary>
        /// Factory method - Create new instance of ServiceResult with 200 OK status code and passed object
        /// </summary>
        /// <param name="data">Response Data</param>
        /// <returns></returns>
        public static ServiceResult<T> CreateResult(T data) =>
            new ServiceResult<T>(data, (int)HttpStatusCode.OK);


        #endregion
    }
}
