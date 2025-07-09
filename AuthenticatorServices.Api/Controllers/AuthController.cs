using Microsoft.AspNetCore.Mvc;
using AuthenticatorServices.Api.DTOs;
using AuthenticatorServices.Domain.Services;

namespace AuthenticatorServices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CreateUserServices _createUserService;
    private readonly AuthenticateUserServices _authenticateUserService;

    public AuthController(CreateUserServices createUserService, AuthenticateUserServices authenticateUserService)
    {
        _createUserService = createUserService;
        _authenticateUserService = authenticateUserService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        try
        {
            var response = await _createUserService.Execute(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
    {
        try
        {
            var response = await _authenticateUserService.Execute(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}