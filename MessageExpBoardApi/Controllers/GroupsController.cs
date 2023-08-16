using Microsoft.AspNetCore.Mvc;
using MessageExpBoardApi.Models;

namespace MessageExpBoardApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class GroupsController : ControllerBase
{
    private readonly MessageContext _db;

    public GroupsController(MessageContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Gets all Groups details. Messages not included.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of Groups</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> Get(string name)
    {
        IQueryable<Group> query = _db.Groups.AsQueryable();
        if (!string.IsNullOrEmpty(name))
            query = query.Where(entry => entry.Name.Contains(name));
        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// 
    /// 
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
        Group model = await _db.Groups.FindAsync(id);
        if (model == null)
            return NotFound();
        else
            return Ok(model);
    }

    /// <summary>
    /// Creates a Group.
    /// </summary>
    /// <param name="group"></param>
    /// <returns>A newly created Group</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /groups
    ///     {
    ///         "name": "Dykes on Bikes"
    ///     }
    ///
    /// </remarks>
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