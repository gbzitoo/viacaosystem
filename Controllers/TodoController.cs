using Microsoft.AspNetCore.Mvc;
using apiaviacaosystem;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todos = new List<TodoItem>();

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(_todos);
        }

        [HttpPost]
        public ActionResult<TodoItem> Post(TodoItem todoItem)
        {
            todoItem.Id = _todos.Count + 1;
            _todos.Add(todoItem);
            return NewMethod(todoItem);
        }

        private CreatedAtActionResult NewMethod(TodoItem todoItem)
        {
            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todoItem = _todos.Find(item => item.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }
    }
}
