using System.ComponentModel.DataAnnotations;

namespace MessageExpBoardApi.Models;

public class Group
{
    public int GroupId { get; set; }
    [Required]
    public string Name { get; set; }
}