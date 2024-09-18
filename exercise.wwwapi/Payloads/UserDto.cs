using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.Payloads;

[NotMapped]
public class UserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}