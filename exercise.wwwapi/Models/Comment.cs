using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models;

[Table("comment")]
public class Comment
{
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("author_id")]
    public int AuthorId { get; set; }
    
    [ForeignKey("post_id")]
    public int PostId { get; set; }
    
    [Column("content")]
    public string Content { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public User Author { get; set; }
}