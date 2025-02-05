using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_API.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public required int AuthorId { get; set; }
    
    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
}
