namespace ConectaCompany.Domain.Models;

public class BaseModel
{
    public long Id { get; init; }
    public bool Active { get; set; } = true;
    public DateTime Created { get; init; } = DateTime.Now;
    public DateTime? Updated { get; set; }
    
    public void Deactivate()
    {
        Active = false;
        Updated = DateTime.Now;
    }
}