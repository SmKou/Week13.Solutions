using Microsoft.AspNetCore.Mvc;
using MessageBoardApi.Models;
using MessageBoardApi.ViewModels;

namespace MessageBoardApi.Controllers;

[Route("api/messages/group")]
public class GroupController : ControllerBase
{
    private readonly MessageContext _db;

    public GroupController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<GroupMessages>>> Get(int id)
    {
        if (!_db.Groups.Any(group => group.GroupId == id))
            return NotFound(new { Exception = "Group not found" });

        GroupMessages model = await _db.Groups
            .AsQueryable()
            .GroupJoin(_db.Messages,
                group => group.GroupId,
                message => message.GroupId,
                (group, messages) => new GroupMessages
                {
                    GroupId = group.GroupId,
                    Name = group.Name,
                    Messages = messages.ToList()
                })
            .SingleOrDefaultAsync(group => group.GroupId == id);
        return Ok(model);
    }
}