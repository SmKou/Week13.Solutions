using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;

namespace MessageBoard.Controllers;

[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly MessageContext _db;

    public MessagesController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> Get(int groupId, int userId)
    {
        if (groupId == 0 && userId == 0)
            return Unauthorized();
        
        IQueryable<Message> query = _db.Messages.AsQueryable();
        if (groupId != 0)
            query.Where(message => message.GroupId == groupId);
        if (userId != 0)
            query.Where(message => message.UserId == userId);
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessage(int id)
    {
        Message message = await _db.Messages
            .SingleOrDefaultAsync(message => message.MessageId == id);
        if (message == null)
            return NotFound();
        else
            return Ok(message);
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Post(Message message)
    {
        _db.Messages.Add(message);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Message message)
    {
        if (id != message.MessageId)
            return BadRequest();
        _db.Messages.Update(message);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!_db.Messages.Any(message => message.MessageId == id))
                return NotFound();
            else
                return StatusCode(418);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroup(int id)
    {
        Message message = await _db.Messages.FindAsync(id);
        if (message == null)
            return NotFound();
        _db.Messages.Remove(message);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}