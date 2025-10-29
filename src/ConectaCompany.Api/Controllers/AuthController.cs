using System.Net;
using System.Text;
using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Application.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

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
        var result = await _authService.SignInAsync(email);
        
        if (!result.IsSuccess)
            return StatusCode((int)result.StatusCode, new { Message = result.Error });
        
        if (result.Value == null)
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "E-mail ou senha inválidos.");

        
        var resultSignIn = await _signInManager.PasswordSignInAsync(result.Value, password, false, false);
        if (!resultSignIn.Succeeded)
        {
            if(resultSignIn.IsLockedOut)
                throw new InvalidOperationException("Usuário bloqueado por tentativas incorretas.");
             
            throw new InvalidOperationException("E-mail ou senha inválidos.");
        }
        
        var token = await _jwtService.GenerateToken(result.Value);
        return StatusCode((int)result.StatusCode, new
        {
            UserId = result.Value.Id,
            FullName = result.Value.FullName,
            Email = result.Value.Email,
            Token = token
        });
    }
    
    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAsync([FromBody] UserDto userDto)
    {
        try
        {
            var result = await _authService.SignUpAsync(userDto);
            
            if (!result.IsSuccess)
                return StatusCode((int)result.StatusCode, new { Message = result.Error });
            
            if (result.Value == null)
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Erro ao criar usuário.");
            
        
            var token = await _authService.GenerateTokenConfirmEmailAsync(result.Value.Id);
            var templateEmail = _authService.GenerateTemplateConfimationEmail(result.Value.FullName, token);
            var subject = "Bem-vindo ao ConectaCompany!";
            var message = templateEmail;
            await _emailService.SendEmailAsync(result.Value.Email!, subject, message);
            return StatusCode((int)result.StatusCode, new { Message = "Usuário criado com sucesso. Verifique seu e-mail para confirmar a conta." });
        }
        catch (Exception ex)
        {
            return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] long userId, string token)
    {
        if (token == null)
            return BadRequest("Link inválido.");
        
        var result = await _authService.ConfirmEmailAsync(userId, token);
        
        if (result)
            return Ok("E-mail confirmado com sucesso!");

        return BadRequest("Erro ao confirmar o e-mail.");
    }
}