using MessageExpBoardApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MessageExpBoardApi.ViewModels;

public class GroupMessages
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public List<Message> Messages { get; set; }
}