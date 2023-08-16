using Microsoft.AspNetCore.Mvc;
using MessageBoardApi.Models;
using MessageBoardApi.ViewModels;

namespace MessageBoardApi.Controllers;

[Route("api/messages/user")]
public class UserController : ControllerBase
{
    private readonly MessageContext _db;

    public UserController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<UserMessages>>> Get(int id)
    {
        if (!_db.Users.Any(user => user.UserId == id))
            return NotFound(new { Exception = "User not found" });

        UserMessages model = await _db.Users
            .AsQueryable()
            .GroupJoin(_db.Messages,
                user => user.UserId,
                message => message.UserId,
                (user, messages) => new UserMessages
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UserName = user.UserName,
                    Messages = messages.ToList()
                })
            .SingleOrDefaultAsync(user => user.UserId == id);
        return Ok(model);
    }
}