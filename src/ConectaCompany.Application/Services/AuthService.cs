using System.Text;
using ConectaCompany.Application.Dto;
using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Application.Interfaces;
using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Shared.Constants;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace ConectaCompany.Application.Services;

public class AuthService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : IAuthService
{
    private readonly IUnitOfWork _uow = unitOfWork;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _uow.Users.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _uow.Users.GetByEmailAsync(email);
        if(user != null && !user.EmailConfirmed)
            throw new InvalidOperationException("E-mail não confirmado. Favor verificar sua  caixa de entrada.");
        
        return user;
    }

    public async Task<User?> SignInAsync(string email, string password)
    {
        var user = await _uow.Users.GetByEmailAsync(email);
        if (user == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.Now)
            throw new InvalidOperationException("Usuário está bloqueado.");

        return user;
    }

    public async Task<User> SignUpAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var userByEmail = await _uow.Users.GetByEmailAsync(user.Email ?? string.Empty);
        if (userByEmail is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");


        await _uow.Users.CreateAsync(user, userDto.Password);
        await _uow.Users.AddRoleAsync(user, Roles.Manager);
        await _uow.CommitAsync();

        return user;
    }

    public async Task<string> GenerateTokenConfirmEmailAsync(long userId)
    {
        var user = await _uow.Users.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return $"?userId={userId}&token={encodedToken}";
    }

    public async Task<bool> ConfirmEmailAsync(long userId, string token)
    {
        var user = await _uow.Users.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        var result = await _uow.Users.ConfirmEmailAsync(user, decodedToken);

        return result;
    }

    public async Task<User> InviteUserAsync(InviteUserDto inviteUserDto)
    {
        var user = _mapper.Map<User>(inviteUserDto);
        var userByEmail = _uow.Users.GetByEmailAsync(inviteUserDto.Email);
        if (userByEmail is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");

        var randomPassword = _generateRandomPassword();
        await _uow.Users.CreateAsync(user, randomPassword);
        await _uow.Users.AddRoleAsync(user, Roles.Employee);
        await _uow.CommitAsync();

        return user;
    }

    public string GenerateTemplateConfimationEmail(string userFullName, string confirmationLink)
    {
        return $@"
                    <h5>Bem-vindo ao ConectaCompany, {userFullName}!</h5>
                    <p>Para completar seu cadastro, por favor, confirme seu e-mail clicando no link abaixo:</p>
                    <a href='http://localhost:5099/api/Auth/confirm-email{confirmationLink}'>Confirmar E-mail</a>
                ";
    }

    private string _generateRandomPassword()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";

        return new string(Enumerable.Repeat(chars, 12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}