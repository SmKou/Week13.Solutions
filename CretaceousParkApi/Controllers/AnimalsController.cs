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

    /* [FromQuery] string species */
    /* Queries: /api/animals?species=[]&name=[] */

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> Get(string species, string name, int minimumAge)
    {
        IQuery<Animal> query = _db.Animals.AsQueryable();
        if (species != null)
            query = query.Where(entry => entry.Species == species);
        if (name != null)
            query = query.Where(entry => entry.Name == name);
        if (minimumAge > 0)
            query = query.Where(entry => entry.Age >= minimumAge);
        return await query.ToListAsync();
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

    /* PATCH (partial updates)
    https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/PATCH
    https://learn.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-6.0
    */

    /* Publicly available Swagger documentation
    Use [ApiExplorerSettings(IgnoreApi = true)] to hide from documentation
    Can apply to controller or action
    */

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        Animal animal = await _db.Animals.FindAsync(id);
        if (animal == null)
            return NotFound();

        _db.Animals.Remove(animal);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}