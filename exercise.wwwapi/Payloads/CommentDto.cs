using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Payloads;

[NotMapped]
public class CommentDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
}