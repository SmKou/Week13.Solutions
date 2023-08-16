using Microsoft.AspNetCore.Mvc;
using MessageBoardApi.Models;
using MessageBoardApi.ViewModels;

namespace MessageBoardApi.Controllers;

[Route("api/[controller]")]
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
        IQueryable<Message> query = _db.Messages.AsQueryable();
        if (DateTime.Compare(fromDate, nullDate) != 0)
            query.Where(message => DateTime.Compare(message.SentAt, fromDate) >= 0);
        if (DateTime.Compare(toDate, nullDate) != 0)
            query.Where(message => DateTime.Compare(message.SentAt, toDate) <= 0);
        return await query.ToListAsync();
    }

    [HttpGet("/group/{id}")]
    public async Task<ActionResult<IEnumerable<GroupMessages>>> GetGroupMessages(int groupId)
    {
        if (!_db.Groups.Any(group => group.GroupId == groupId))
            return NotFound();

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
            .SingleOrDefaultAsync(group => group.GroupId == groupId);
        return Ok(model);
    }

    [HttpGet("/user/{id}")]
    public async Task<ActionResult<IEnumerable<UserMessages>>> GetUserMessages(int userId)
    {
        if (!_db.Users.Any(user => user.UserId == userId))
            return NotFound();

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
            .SingleOrDefaultAsync(user => user.UserId == userId);
        return Ok(model);
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
    public async Task<ActionResult<Message>> Post(Message message)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _db.Messages.Add(message);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
    }

    /* Note to Self */
    // Cannot update or delete a message unless by given user

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