namespace Command.Domain.Exception;

public class ValidationException : System.Exception
{
    public List<ValidationError> ValidationErrors { get; } 
    
    public ValidationException(List<ValidationError> errors)
    {
        ValidationErrors = errors;
    }


    public class ValidationError
    {
        public string Field { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}