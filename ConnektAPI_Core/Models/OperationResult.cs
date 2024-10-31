namespace ConnektAPI_Core.Models;

public class OperationResult
{
    public int StatusCode { get; set; }
    public bool IsSuccess => ErrorMessage == null;
    public string ErrorTitle { get; set; }
    public string ErrorMessage { get; set; }
    public object Result { get; set; }
}

public class OperationResult<T> : OperationResult
{
    public T Result { get; set; }
}