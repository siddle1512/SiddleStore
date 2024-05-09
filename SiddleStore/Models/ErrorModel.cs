namespace SiddleStore.Models
{
    public class ErrorModel
    {
        public int status { get; set; }
        public string? errorMsg { get; set; }

        public ErrorModel(int statusCode, string? message)
        {
            status = statusCode;
            errorMsg = message;
        }
    }
}

