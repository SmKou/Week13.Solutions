using Microsoft.AspNetCore.Mvc;
using MessageBoardApi.Models;

namespace MessageBoardApi.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly MessageContext _db;

    public UsersController(MessageContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get(string name, string username)
    {
        IQueryable<User> query = _db.Users.AsQueryable();
        if (!string.IsNullOrEmpty(name))
            query = query.Where(user => user.Name.Contains(name));
        if (!string.IsNullOrEmpty(username))
            query = query.Where(user => user.UserName.Contains(username));
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        User model = await _db.Users.FindAsync(id);
        if (model == null)
            return NotFound();
        else
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