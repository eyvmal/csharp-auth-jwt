using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models;

[Table("blogpost")]
public class BlogPost
{
    [Column("id")]
    public int Id { get; init; }
    
    [ForeignKey("author_id")]
    public int AuthorId { get; set; }
    
    [Column("content")]
    public string Content { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public User Author { get; set; }
    public List<Comment> Comments { get; set; }
}