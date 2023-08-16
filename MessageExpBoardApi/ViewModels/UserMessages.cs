using MessageExpBoardApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MessageExpBoardApi.ViewModels;

public class UserMessages
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public List<Message> Messages { get; set; }
}