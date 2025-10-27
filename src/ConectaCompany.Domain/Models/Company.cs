namespace ConectaCompany.Domain.Models;

public class Company : BaseModel
{
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    
    public string ImagePathLogo { get; set; }
    
    public List<Post> Posts { get; set; }
    public List<Employee> Employees { get; set; }
}