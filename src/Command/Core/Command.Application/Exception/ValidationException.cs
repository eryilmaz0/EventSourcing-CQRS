namespace Command.Application.Exception;

public class ValidationException : System.Exception
{ 
    public List<ValidationError> ValidationErrors { get; }

    public ValidationException(List<ValidationError> validationErrors)
    {
        ValidationErrors = validationErrors;
    }
}


public class ValidationError
{
    public string Field { get; set; }
    public string ErrorMessage { get; set; }
}
