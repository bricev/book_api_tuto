using System.ComponentModel.DataAnnotations;

namespace Book_API.Models;

public class Author
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public required string FirstName { get; set; }
    
    [Required]
    public required string LastName { get; set; }
}
