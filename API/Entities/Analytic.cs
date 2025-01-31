using System.ComponentModel.DataAnnotations.Schema;
using API.Base;

namespace API.Entities;

[Table("Analytic", Schema = "Shortener")]
public class Analytic : Entity<long>
{
    public long UrlId { get; set; }
    
    [ForeignKey(nameof(UrlId))]
    public Url? Url { get; set; }
    
    public DateTimeOffset Created { get; set; }
    
    public string? UserAgent { get; set; }
    
    public string? IpAddress { get; set; }
    
    public string? Referer { get; set; }
    
    public string? Country { get; set; }
    
    public string? Region { get; set; }
    
    public string? City { get; set; }
    
    public string? Browser { get; set; }
    
    public string? Os { get; set; }
    
    public string? Device { get; set; }
    
    public string? Platform { get; set; }
    
    public string? Engine { get; set; }
}