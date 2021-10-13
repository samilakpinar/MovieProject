namespace MovieProject.Result
{
    public interface IErrorMessage
    {
        string Message { get; set; }
        string Type { get; set; }
        string Code { get; set; }
    }
}
