using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;
using MessageBoard.ViewModels;

namespace MessageBoard.Controllers;

[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly MessageContext _db;

    public GroupsController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupMessages>>> Get(string name)
    {
        IQueryable<GroupMessages> query = _db.Groups
            .AsQueryable()
            .GroupJoin(_db.Messages,
                group => group.GroupId,
                message => message.GroupId,
                (group, messages) => new GroupMessages 
                {
                    GroupId = group.GroupId,
                    Name = group.Name,
                    Messages = messages.ToList()
                });
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupMessages>> GetGroup(int id)
    {
        GroupMessages model = new GroupMessages();
        if (!_db.Groups.Any(group => group.GroupId == id))
            return NotFound();
        model = await _db.Groups
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

    [HttpPost]
    public async Task<ActionResult<Group>> Post([FromBody] Group group)
    {
        _db.Groups.Add(group);
        await _db.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetGroup), 
            new { id = group.GroupId }, 
            group);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id,[FromBody] Group group)
    {
        if (id != group.GroupId)
            return BadRequest();
        _db.Groups.Update(group);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Groups.Any(group => group.GroupId == id))
                return NotFound();
            else
                return StatusCode(418);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        Group group = await _db.Groups.FindAsync(id);
        if (group == null)
            return NotFound();
        _db.Groups.Remove(group);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}