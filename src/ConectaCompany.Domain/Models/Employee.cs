namespace ConectaCompany.Domain.Models;

public class Employee : BaseModel
{
    public string FullName { get; set; }
    
    public DateOnly Birthday { get; set; }
    public long CompanyId { get; set; }
    public long UserId { get; set; } 
    
    public long ProfileId { get; set; }
    public User User { get; set; } = new();
    public Company Company { get; set; } = new();
    
    public Profile Profile { get; set; } = new();
    public List<Post> CreatedPosts { get; set; } = new();
    public List<Post> UpdatedPosts { get; set; } = new();
    
}