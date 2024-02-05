using System.Net;

namespace SearchName.Models.Response;

/// <summary>

/// The error response class

/// </summary>

public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the value of the message
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Gets or sets the value of the error
    /// </summary>
    public string Error { get; set; }
    /// <summary>
    /// Gets or sets the value of the status code
    /// </summary>
    public int StatusCode { get; set; }
}