using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;
using MessageBoard.ViewModels;

namespace MessageBoard.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly MessageContext _db;

    public UsersController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMessages>>> Get(string name)
    {
        IQueryable<UserMessages> query = _db.Users
            .AsQueryable()
            .GroupJoin(_db.Messages,
                user => user.UserId,
                message => message.UserId,
                (user, messages) => new UserMessages
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Messages = messages.ToList()
                });
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMessages>> GetUser(int id)
    {
        UserMessages model = new UserMessages();
        if (!_db.Users.Any(user => user.UserId == id))
            return NotFound();
        model = await _db.Users
            .GroupJoin(_db.Messages,
                user => user.UserId,
                message => message.UserId,
                (user, messages) => new UserMessages {
                    UserId = user.UserId,
                    Name = user.Name,
                    Messages = messages.ToList()
                })
            .SingleOrDefaultAsync(user => user.UserId == id);
        return Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        user.NormalizedUserName = user.UserName.ToLower();
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetUser), 
            new { id = user.UserId }, 
            user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id,[FromBody] User user)
    {
        if (id != user.UserId)
            return BadRequest();
        _db.Users.Update(user);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Users.Any(user => user.UserId == id))
                return NotFound();
            else
                return StatusCode(418);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        User user = await _db.Users.FindAsync(id);
        if (user == null)
            return NotFound();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}