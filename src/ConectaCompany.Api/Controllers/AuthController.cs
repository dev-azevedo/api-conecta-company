using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Application.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConectaCompany.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController(
    IAuthService authService,
    SignInManager<User> signInManager,
    IJwtService jwtService,
    IEmailService emailService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IEmailService _emailService = emailService;

    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignInAsync(string email, string password)
    {
        var user = await _authService.GetByEmailAsync(email);
        
        if (user == null)
            throw new InvalidOperationException("E-mail ou senha inválidos.");
        
        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
        {
            if(result.IsLockedOut)
                throw new InvalidOperationException("Usuário bloqueado por tentativas incorretas.");
             
            throw new InvalidOperationException("E-mail ou senha inválidos.");
        }
        
        var token = await _jwtService.GenerateToken(user);
        return Ok(new
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = token
        });
    }
    
    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAsync([FromBody] UserDto userDto)
    {
        try
        {
            var user = await _authService.SignUpAsync(userDto);
            // Disparar email para confirmaçao
            
            var subject = "Bem-vindo ao ConectaCompany!";
            var message = "Confirmar email!";
            await _emailService.SendEmailAsync(user.Email!, subject, message);
            
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        
    }
}