using ArtMarketplace.Controllers.DTOs;
using ArtMarketplace.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketplace.Controllers;
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
            var token = await _authService.RegisterAsync(request);
            return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
