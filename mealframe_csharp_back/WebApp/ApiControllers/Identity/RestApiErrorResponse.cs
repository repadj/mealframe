using System.Net;

namespace WebApp.ApiControllers.Identity;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}