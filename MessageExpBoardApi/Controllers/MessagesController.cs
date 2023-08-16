using Microsoft.AspNetCore.Mvc;
using MessageExpBoardApi.Models;
using MessageExpBoardApi.ViewModels;

namespace MessageExpBoardApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MessagesController : ControllerBase
{
    private readonly MessageContext _db;

    public MessagesController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> Get(DateTime fromDate, DateTime toDate)
    {
        DateTime nullDate = new DateTime(1, 1, 1, 0, 0, 0);
        if (DateTime.Compare(toDate, nullDate) == 0)
            toDate = DateTime.Now;
        IQueryable<Message> query = _db.Messages
            .AsQueryable()
            .Where(message => message.SentAt >= fromDate && message.SentAt <= toDate);
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

    /* Note to Self */
    // POST message to a specific group

    [HttpPost]
    public async Task<ActionResult<Message>> Post([FromBody] Message message)
    {
        message.SentAt = DateTime.Now;
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _db.Messages.Add(message);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
    }

    /* Note to Self */
    // Cannot update or delete a message unless by given user

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id,[FromBody] Message message)
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