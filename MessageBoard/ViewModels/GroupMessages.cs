using MessageBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.ViewModels;

public class GroupMessages
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public List<Message> Messages { get; set; }
}