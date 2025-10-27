namespace ConectaCompany.Domain.Models;

public class Employee : BaseModel
{
    public DateOnly Birthday { get; set; }
    public long CompanyId { get; set; }
    public long UserId { get; set; } 
    public User User { get; set; } = new();
    public Company Company { get; set; } = new();
    public List<Post> CreatedPosts { get; set; } = new();
    public List<Post> UpdatedPosts { get; set; } = new();
    
}