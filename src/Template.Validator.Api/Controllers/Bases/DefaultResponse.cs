using Bogus.DataSets;
using System.Net;

namespace Template.Validator.Api.Controllers.Bases;

    /// <summary>Default DTO response api.</summary>
    public class DefaultResponse : DefaultResponse<object>
    {
        public DefaultResponse(HttpStatusCode statusCode, bool success, IEnumerable<string> errors) : base(statusCode, success, errors) {}
        public DefaultResponse(HttpStatusCode statusCode, bool success, object data) : base(statusCode, success, data) { }
    }

    /// <summary>Default DTO response api.</summary>
    public class DefaultResponse<T>
    {
        /// <summary>HttpStatusCode result.</summary>
        /// <example>200</example>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>Date and time the request was processed.</summary>
        /// <example>2020-10-10T10:00:00</example>
        public DateTime Date { get; private set; }

        /// <summary>Environment in which the application is running.</summary>
        /// <example>Production</example>
        public string Environments { get; private set; }

        /// <summary>Status request.</summary>
        /// <example>true</example>
        public bool Success { get; private set; }

        /// <summary>Data response.</summary>
        public T Data { get; private set; }

        /// <summary>List of messages about errors generated during the request.</summary>
        public IEnumerable<string> Errors { get; private set; }

        public DefaultResponse(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
            Date = DateTime.Now;
            Environments = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"Not Detected";
        }

        public DefaultResponse(HttpStatusCode statusCode, bool success, T data) : this(statusCode, success) =>
            Data = data;

        public DefaultResponse(HttpStatusCode statusCode, bool success, IEnumerable<string> errors) : this(statusCode, success) =>
            Errors = errors;

        public DefaultResponse(HttpStatusCode statusCode, bool success, T data, IEnumerable<string> errors) : this(statusCode, success, data) =>
            Errors = errors;
    }

