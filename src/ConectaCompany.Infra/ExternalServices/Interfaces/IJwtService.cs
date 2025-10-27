using ConectaCompany.Domain.Models;

namespace ConectaCompany.Infra.ExternalServices.Interfaces;

public interface IJwtService
{
    Task<string> GenerateToken(User user);
}