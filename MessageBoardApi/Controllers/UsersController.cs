using Microsoft.AspNetCore.Mvc;
using MessageBoardApi.Models;
using MessageBoardApi.ViewModels;

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
    public async Task<ActionResult<IEnumerable<UserViewModel>>> Get(string name, string username)
    {
        IQueryable<User> query = _db.Users.AsQueryable();
        if (!string.IsNullOrEmpty(name))
            query = query.Where(user => user.Name.Contains(name));
        if (!string.IsNullOrEmpty(username))
            query = query.Where(user => user.UserName.Contains(username));
        IQueryable<UserViewModel> convert = query
            .Select(user => new UserViewModel 
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName
            });
        return await convert.ToListAsync();
    }

    [HttpGet("{id}")]
    public ActionResult<UserViewModel> GetUser(int id)
    {
        User user = _db.Users
            .AsQueryable()
            .SingleOrDefault(user => user.UserId == id);
        if (user == null)
            return NotFound();
        UserViewModel model = new UserViewModel
        {
            UserId = user.UserId,
            Name = user.Name,
            UserName = user.UserName
        };
        return Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult<UserViewModel>> Post([FromBody] User user)
    {
        user.NormalizedUserName = user.UserName.ToLower();
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        UserViewModel model = new UserViewModel
        {
            UserId = user.UserId,
            Name = user.Name,
            UserName = user.UserName
        };
        return CreatedAtAction(
            nameof(GetUser), 
            new { id = user.UserId }, 
            model);
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