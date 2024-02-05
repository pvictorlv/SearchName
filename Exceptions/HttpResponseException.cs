using System.Net;

namespace SearchName.Exceptions;

/// <summary>

/// The http response exception class

/// </summary>

/// <seealso cref="Exception"/>

public class HttpResponseException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResponseException"/> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="status">The status</param>
    public HttpResponseException(string message, HttpStatusCode status = HttpStatusCode.BadRequest) : base(message)
    {
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpResponseException"/> class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    public HttpResponseException(string message, int statusCode) : base(message)
    {
        Status = (HttpStatusCode)statusCode;
    }
    /// <summary>
    /// Gets or sets the value of the status
    /// </summary>
    public HttpStatusCode Status { get; set; }
}