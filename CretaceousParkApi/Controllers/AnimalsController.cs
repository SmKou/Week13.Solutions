/* Implicit
System.Collections.Generic
System.Threading.Tasks
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousParkApi.Models;

namespace CretaceousParkApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly ApiContext _db;

    public AnimalsController(ApiContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> Get()
    {
        return await _db.Animals.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        Animal animal = await _db.Animals.FindAsync(id);
        if (animal == null)
            return NotFound();
        else
            return animal;
    }
}