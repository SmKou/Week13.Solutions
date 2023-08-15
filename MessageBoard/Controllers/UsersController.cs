using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;

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
    public async Task<ActionResult<IEnumerable<User>>> Get(string name)
    {
        IQueryable<User> query = _db.Users
            .AsQueryable()
            .GroupJoin(_db.Messages,
                user => user.UserId,
                message => message.UserId,
                (user, messages) => new User
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Messages = messages.ToList()
                }
            );
        if (name != null)
            query = query.Where(entry => entry.Name.Contains(name));
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        User user = await _db.Users
            .Include(user => user.Messages)
            .SingleOrDefaultAsync(user => user.UserId == id);
        if (user == null)
            return NotFound();
        else
            return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, User user)
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