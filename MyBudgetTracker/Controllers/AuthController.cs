using System.Net;
using CustomResponsePackage;
using Microsoft.AspNetCore.Mvc;
using MyBudgetTracker.Requests;
using MyBudgetTracker.Responses;
using MyBudgetTracker.Services;

namespace MyBudgetTracker.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost(ApiEndpoints.Auth.Registration)]
    public async Task<ActionResult<CustomResponse<RegistrationResponse>>> Registration([FromBody] RegistrationRequest request, CancellationToken token)
    {
        var registration = await _service.Registration(request, token);
        if (!registration)
        {
            var errorResponse = CustomResponse<RegistrationResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = "Registration failed",
                Code = HttpStatusCode.BadRequest.ToString()
            });

            return BadRequest(errorResponse);
        }

        var successResponse = CustomResponse<RegistrationResponse>.CreateSuccessResponse(new RegistrationResponse
        {
            Message = "Created successfully, now confirm your email",
            Email = request.Email,
            Name = request.Name,
            Gender = request.Gender,
            Birthday = request.Birthday,
            Password = request.Password
        });
        
        return Ok(successResponse);
    }

    [HttpPost(ApiEndpoints.Auth.Confirmation)]
    public async Task<ActionResult<CustomResponse<ConfirmationResponse>>> ConfirmRegistration([FromBody] ConfirmRegistrationRequest request, CancellationToken token)
    {
        var confirm = await _service.ConfirmRegistration(request, token);
        if (!confirm)
        {
            var errorResponse = CustomResponse<ConfirmationResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = $"User {request.Username} not confirmed",
                Code = HttpStatusCode.BadRequest.ToString()
            });
            return BadRequest(errorResponse);
        }

        // var response = request.MapToResponse();
        var successResponse = CustomResponse<ConfirmationResponse>.CreateSuccessResponse(new ConfirmationResponse
        {
            Username = request.Username,
            Message = "Successfully confirmed"
        });
        return Ok(successResponse);
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<ActionResult<CustomResponse<AuthLoginResponse>>> Login([FromBody] LoginRequest loginRequest,
        CancellationToken token)
    {
        var tokens = await _service.Login(loginRequest, token);
        if (tokens == null)
        {
            var errorResponse = CustomResponse<AuthLoginResponse>.CreateErrorResponse(new ErrorResponse
            {
                Message = "Login or password incorrect",
                Code = HttpStatusCode.BadRequest.ToString()
            });
            return BadRequest(errorResponse);
        }

        var successResponse = CustomResponse<AuthLoginResponse>.CreateSuccessResponse(tokens);

        return Ok(successResponse);
    }
}