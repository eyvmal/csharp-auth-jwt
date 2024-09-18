using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Payloads;

[NotMapped]
public class PostDto
{
    public int UserId { get; set; }
    public string Content { get; set; }
}