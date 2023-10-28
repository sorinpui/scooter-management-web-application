using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterManagement.Domain.Dtos;
using ScooterManagement.Domain.Interfaces;
using ScooterManagement.Domain.Requests;
using ScooterManagement.Domain.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace ScooterManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScooterController : ControllerBase
{
    private readonly IScooterService _scooterService;
    private readonly IValidationService _validationService;

    public ScooterController(IScooterService scooterService, IValidationService validationService)
    {
        _scooterService = scooterService;
        _validationService = validationService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllScooters()
    {
        var scooters = await _scooterService.GetScootersAsync();

        var response = new SuccessResponse<List<ScooterDto>>
        {
            Message = "List of scooters successfully returned.",
            Payload = scooters,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateScooter(CreateScooterRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        await _scooterService.CreateScooterAsync(request);

        var response = new SuccessResponse<string>
        {
            Message = "Scooter created successfully.",
            Payload = string.Empty,
            Status = HttpStatusCode.Created
        };

        return Created(string.Empty, response);
    }

    [HttpPut("{scooterId}")]
    [Authorize(Roles = "Rider")]
    public async Task<IActionResult> RentScooter([FromRoute] int scooterId)
    {
        await _scooterService.UpdateScooterAsync(scooterId);

        var response = new SuccessResponse<string>
        {
            Message = "Scooter rented successfully.",
            Payload = string.Empty,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }

    [HttpDelete("{scooterId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteScooter([FromRoute] int scooterId)
    {
        await _scooterService.DeleteScooterAsync(scooterId);

        var response = new SuccessResponse<string>
        {
            Message = "Scooter deleted successfully.",
            Payload = string.Empty,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }

    [HttpPut("return/{scooterId}")]
    [Authorize(Roles = "Rider")]
    public async Task<IActionResult> ReturnScooter([FromRoute] int scooterId)
    {
        await _scooterService.ReturnScooterAsync(scooterId);

        var response = new SuccessResponse<string>
        {
            Message = "Scooter returned successfully.",
            Payload = string.Empty,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }
}
