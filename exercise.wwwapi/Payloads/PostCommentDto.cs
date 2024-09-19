using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Payloads;

[NotMapped]
public class PostCommentDto
{
    public int? Id { get; set; }
    public string Content { get; set; }
}