using System.ComponentModel.DataAnnotations;

namespace API.Base;

public class Entity<TKey> 
    where TKey : struct
{
    [Key]
    public TKey Id { get; set; }
}