using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
            if (!_context.People.Any())
            {
                _context.People.AddRange( new List<Person>()
                {
                    new Person() {Name = "Artem", Age = 19},
                    new Person() {Name = "Anastasiya", Age = 17},
                });

                _context.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get() => await _context.People.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            Person? person = await _context.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound("Person is not found in DB.");

            return new ObjectResult(person);
        }


        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }


        [HttpPut]
        public async Task<ActionResult<Person>> Put(Person person)
        {
            if(person is null) return BadRequest("Person is null.");

            if(!_context.People.Any(p => p.Id == person.Id)) return NotFound("Person is not found in DB.");

            _context.People.Update(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            Person? person = await _context.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound("Person is not found in DB.");

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }
    }
}
