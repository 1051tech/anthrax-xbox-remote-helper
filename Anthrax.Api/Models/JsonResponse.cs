namespace Anthrax.Api.Models
{
    /// <summary>
    /// This encapsulates data sent for every response to a JSON request.
    /// </summary>
    public class JsonResponse
    {
        public object Data { get; set; } // Contains the model
        public string Info { get; set; } // Additional info to report
        public Status ResponseStatus { get; set; }

        public JsonResponse(Status status, object data = null, string info = "")
        {
            Data = data;
            Info = info;
            ResponseStatus = status;
        }

        public enum Status
        {
            Success = 0,
            Error = 255
        }
    }
}