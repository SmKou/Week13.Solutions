using System.ComponentModel.DataAnnotations;

namespace MessageBoardApi.Models;

public class Message
{
    public int MessageId { get; set; }
    public string MessageText { get; set; }
    public DateTime SentAt { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
}