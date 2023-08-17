using Microsoft.AspNetCore.Mvc;
using ViacaoSystemAPI.Persistence;
using ViacaoSystemAPI.Entity;

namespace ViacaoSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public TodoController(TodoDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var todoEvents = _context.TodoEventsCx.Where(d => !d.IsDeleted).ToList();

            return Ok(todoEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        {
            var todoEvents = _context.TodoEventsCx.SingleOrDefault(d => d.Id == id);
            if (todoEvents == null)
            {
                return NotFound();
            }
            return Ok(todoEvents);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item) 
        {
             _context.TodoEventsCx.Add(item);
             _context.SaveChanges();
             return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] TodoItem input) 
        {
            var todoEvents = _context.TodoEventsCx.SingleOrDefault(d => d.Id == id);
            if (todoEvents == null)
            {
                return NotFound();
            }

            todoEvents.updated(input.Title, input.Descricao,input.IsCompleted);

            _context.TodoEventsCx.Update(todoEvents);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            var todoEvents = _context.TodoEventsCx.SingleOrDefault(d => d.Id == id);
            if (todoEvents == null)
            {
                return NotFound();
            }
            todoEvents.deleted(true);

            _context.SaveChanges();

            return NoContent();
        }


    }
}
