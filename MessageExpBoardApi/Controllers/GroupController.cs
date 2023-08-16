using Microsoft.AspNetCore.Mvc;
using MessageExpBoardApi.Models;
using MessageExpBoardApi.ViewModels;

namespace MessageExpBoardApi.Controllers;

[Route("api/v{version:apiVersion}/messages/[controller]")]
[ApiVersion("1.0")]
public class GroupController : ControllerBase
{
    private readonly MessageContext _db;

    public GroupController(MessageContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Gets all messages of a specific Group.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>GroupMessages</returns>
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