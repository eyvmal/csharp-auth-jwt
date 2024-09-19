using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models;

[Table("following")]
public class Following
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user1_id")]
    public int User1Id { get; set; }
    
    [Column("user2_id")]
    public int User2Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}