using System.Net;

namespace Template.Validator.Api.Controllers.Bases
{
    public class DefaultResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public DateTime Date { get; private set; }
        public string Environments { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public DefaultResponse(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
            Date = DateTime.Now;
            Environments = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public DefaultResponse(HttpStatusCode statusCode, bool success, object data) : this(statusCode, success) =>
            Data = data;

        public DefaultResponse(HttpStatusCode statusCode, bool success, IEnumerable<string> errors) : this(statusCode, success) =>
            Errors = errors;

        public DefaultResponse(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> errors) : this(statusCode, success, data) =>
            Errors = errors;
    }
}
