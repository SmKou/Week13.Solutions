using System.ComponentModel.DataAnnotations;

namespace MessageExpBoardApi.Models;

public class Message
{
    public int MessageId { get; set; }
    [Required]
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    [Required]
    public int GroupId { get; set; }
    [Required]
    public int UserId { get; set; }
}