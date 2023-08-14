using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models;

public class User
{
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }

    public List<Message> Messages { get; set; }
}