using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Domain.Models;

public class User : IdentityUser<long>
{
    public DateTime Created { get; set; } = DateTime.Now;
    public Employee Employee { get; set; } = new();
}