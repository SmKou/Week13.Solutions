using System.ComponentModel.DataAnnotations;

namespace MessageBoardApi.Models;

public class Group
{
    public int GroupId { get; set; }
    [Required]
    public string Name { get; set; }
}