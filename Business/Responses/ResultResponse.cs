namespace Business.Responses
{
    public class ResultResponse
    {
        public bool Status => ErrorMessage == null || ErrorMessage == "" ? true : false;
        public string ErrorMessage { get; set; }
    }
}
