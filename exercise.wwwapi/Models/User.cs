using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Models;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; init; }
    [Column("username")]
    public string Username { get; set; }
    [Column("password_hashed")]
    public string PasswordHash { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}