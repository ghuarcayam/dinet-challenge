namespace Dinet.Module.Challenge.Application
{
    public class OperationResult
    {
        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public string Message { get; }


        public bool Success { get; }

        public static OperationResult<T> Ok<T>(T data) 
        {
            return new Application.OperationResult<T>(data, true);
        }
        public static OperationResult<T> WithError<T>(string message)
        {
            return new Application.OperationResult<T>(message, false);
            
        }

        public static OperationResult Ok()
        {
            return new Application.OperationResult(true);
        }

        public static OperationResult WithError(string message)
        {
            return new OperationResult(message, false);
        }
    }
}
