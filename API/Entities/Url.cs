using System.ComponentModel.DataAnnotations.Schema;
using API.Base;

namespace API.Entities;

[Table("Urls", Schema = "Shortener")]
public class Url : Entity<long>
{
    [Column(TypeName = "varchar(255)")]
    public required string ShortUrl { get; set; }
    
    
    [Column(TypeName = "varchar(255)")]
    public required string OriginalUrl { get; set; }

    
    public required DateTimeOffset Expiration { get; set; }

    
    public required DateTimeOffset Created { get; set; }

    public virtual ICollection<Analytic>? Analytics { get; set; }
}