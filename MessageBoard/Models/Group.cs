using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models;

public class Group
{
    public int GroupId { get; set; }
    [Required]
    public string Name { get; set; }
}