using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaCompany.Domain.Models;

public class Post : BaseModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string PathImage { get; set; }
    
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    
    [ForeignKey("CreatedBy")]
    public long CreatedById { get; set; }
    public Employee CreatedBy { get; set; }
    
    [ForeignKey("UpdatedBy")]
    public long UpdatedById { get; set; }
    public Employee UpdatedBy { get; set; }
}