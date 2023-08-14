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

    /* Model binding
    https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0#binding-source-parameter-inference
    [FromBody] Animal animal
    */

    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal)
    {
        _db.Animals.Add(animal);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);
        /* controller action, route values, resource created */
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Animal animal)
    {
        if (id != animal.AnimalId)
            return BadRequest();
        _db.Animals.Update(animal);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_db.Animals.Any(e => e.AnimalId == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
}