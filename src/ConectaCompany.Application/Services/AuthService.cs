using ConectaCompany.Application.Dto;
using ConectaCompany.Application.Dto.Auth;
using ConectaCompany.Application.Interfaces;
using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using ConectaCompany.Shared.Constants;
using MapsterMapper;

using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Application.Services;


public class AuthService(IUnitOfWork unitOfWork, IMapper mapper) : IAuthService
{
    private readonly IUnitOfWork _uow = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<User?> GetByIdAsync(long id)
    {
        return await _uow.Users.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _uow.Users.GetByEmailAsync(email);
    }

    public async Task<User?> SignInAsync(string email, string password)
    { 
        var user = await _uow.Users.GetByEmailAsync(email);
         if (user == null)
             throw new InvalidOperationException("Usuário não encontrado.");
         
         if(user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.Now)
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
    
    private string _generateRandomPassword()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        
        return new string(Enumerable.Repeat(chars, 12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}