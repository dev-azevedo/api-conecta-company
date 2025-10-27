using ConectaCompany.Application.Interfaces;
using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Application.Services;


public class AuthService(IUserRepository userRepository, SignInManager<User> signInManager) : IAuthService
{
    
    private readonly IUserRepository _userRepository = userRepository;
    private readonly SignInManager<User> _signInManager = signInManager;
    
    public async Task<User?> GetByIdAsync(long id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> SignInAsync(string email, string password)
    {
       var user = await _userRepository.GetByEmailAsync(email);
         if (user == null)
             throw new InvalidOperationException("Usuário não encontrado.");
         
         if(user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.Now)
             throw new InvalidOperationException("Usuário está bloqueado.");
         
         var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
         if (!result.Succeeded)
         {
             if(result.IsLockedOut)
                 throw new InvalidOperationException("Usuário bloqueado por tentativas incorretas.");
             
             throw new InvalidOperationException("E-mail ou senha inválidos.");
         }
         
         return user;
    }

    public async Task<User> SignUpAsync(User item, string password)
    {
       if (string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("Senha é obrigatória.");
        
       var userByEmail = await _userRepository.GetByEmailAsync(item.Email);
       if (userByEmail is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");

       return await _userRepository.CreateAsync(item, password);
    }
}