using System.Net;

namespace LojinhaAPI.Generics;

public class Error
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public Error(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

