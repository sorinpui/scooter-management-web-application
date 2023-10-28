using System.Net;

namespace ScooterManagement.Application.Exceptions;

public class LoginException : ServiceException
{
    public override string ErrorMessage { get; set ; }
    public override HttpStatusCode StatusCode { get; set; }
}
