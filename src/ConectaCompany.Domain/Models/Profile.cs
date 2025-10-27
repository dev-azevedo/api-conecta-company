namespace ConectaCompany.Domain.Models;

public class Profile : BaseModel
{
    public string Role { get; set; }
    
    public List<Employee> Employees { get; set; } = new();
}