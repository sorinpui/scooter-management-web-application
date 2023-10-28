using Microsoft.AspNetCore.Mvc;
using ScooterManagement.Domain.Responses;
using ScooterManagement.Domain.Interfaces;
using ScooterManagement.Domain.Requests;
using System.Net;

namespace ScooterManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IValidationService _validationService;

    public AccountController(IAccountService accountService, IValidationService validationService)
    {
        _accountService = accountService;
        _validationService = validationService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser(RegisterRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        await _accountService.RegisterUserAsync(request);

        var response = new SuccessResponse<string>
        {
            Message = "User account created successfully.",
            Payload = string.Empty,
            Status = HttpStatusCode.Created
        };

        return Created(string.Empty, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        string token = await _accountService.LoginUserAsync(request);

        var response = new SuccessResponse<string>
        {
            Message = "User logged in successfully.",
            Payload = token,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }
}
