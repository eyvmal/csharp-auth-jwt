using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models;

[Table("BlogPost")]
public class BlogPost
{
    [Column("id")]
    public int Id { get; init; }
    [ForeignKey("author_id")]
    public int AuthorId { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
    public User Author { get; set; }
}