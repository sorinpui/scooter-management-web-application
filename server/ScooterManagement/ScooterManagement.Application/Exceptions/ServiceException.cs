using System.Net;

namespace ScooterManagement.Application.Exceptions;

public abstract class ServiceException : Exception
{
    public abstract string ErrorMessage { get; set; }
    public abstract HttpStatusCode StatusCode { get; set; }
}
