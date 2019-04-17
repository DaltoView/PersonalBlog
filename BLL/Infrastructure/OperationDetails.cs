namespace BLL.Infrastructure
{
    /// <summary>
    /// Represents a result of identity opertions.
    /// </summary>
    public class OperationDetails
    {
        public OperationDetails(bool succeded)
        {
            Succeeded = succeded;
            if (succeded)
            {
                Message = "Succeeded";
            }
            else
            {
                Message = "Failed";
            }
        }

        public OperationDetails(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
    }
}
