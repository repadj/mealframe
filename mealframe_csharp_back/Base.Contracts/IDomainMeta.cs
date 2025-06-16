namespace Base.Contracts;

public interface IDomainMeta
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public string? SysNotes { get; set; } 
}