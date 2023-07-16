using Microsoft.AspNetCore.Mvc;
using Task_1.models;

namespace Task_1.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private static readonly List<Person> _users = new();

        static DataController()
        {
            _users.AddRange(new List<Person>
            {
                new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
            });
        }

        [HttpGet]
        public IActionResult Get() => GetAllPeople();

        private IActionResult GetAllPeople() => Ok(_users);


        [HttpGet("{id}")]
        public IActionResult Get(string id) => GetPerson(id);

        private IActionResult GetPerson(string id)
        {
            Person? user = _users.FirstOrDefault(u => u.Id == id);
            
            if (user != null)
                return Ok(user);
            else
                return NotFound(new { message = "Пользователь не найден" });
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person) => await CreatePerson(person);

        private Task<IActionResult> CreatePerson(Person person)
        {
            if (person != null)
            {
                person.Id = Guid.NewGuid().ToString();
                _users.Add(person);
                return Task.FromResult<IActionResult>(Ok(person));
            }
            else
            {
                return Task.FromResult<IActionResult>(BadRequest(new { message = "Некорректные данные" }));
            }
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person person) => await UpdatePerson(person);

        private Task<IActionResult> UpdatePerson(Person person)
        {
            if (person != null)
            {
                Person? user = _users.FirstOrDefault(u => u.Id == person.Id);

                if (user != null)
                {
                    user.Age = person.Age;
                    user.Name = person.Name;
                    return Task.FromResult<IActionResult>(Ok(user));
                }
                else
                {
                    return Task.FromResult<IActionResult>(NotFound(new { message = "Пользователь не найден" }));
                }
            }
            else
            {
                return Task.FromResult<IActionResult>(BadRequest(new { message = "Некорректные данные" }));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await DeletePerson(id);
        }

        private Task<IActionResult> DeletePerson(string id)
        {
            Person? user = _users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                _users.Remove(user);
                return Task.FromResult<IActionResult>(Ok(user));
            }
            else
            {
                return Task.FromResult<IActionResult>(NotFound(new { message = "Пользователь не найден" }));
            }
        }
    }
}
