
using Microsoft.AspNetCore.Mvc;
using TodosDB2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodosDB2.Persistence
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ITodoService todoService;

        public TodosController(ITodoService service)
        {
            this.todoService = service;
        }
        // GET: api/<TodosController>
        [HttpGet]
        public async Task<ActionResult<IList<Todo>>>
        GetTodos([FromQuery] int? userId, [FromQuery] bool? isCompleted)
        {
            try
            {
                IList<Todo> todos = await todoService.GetTodosAsync();
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //not useful, let's see
            return "value";
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Todo added = await todoService.AddTodoAsync(todo);
                return Created($"/{added.TodoId}", added); // return newly added to-do, to get the auto generated id
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        // PATCH api/<TodosController>/5
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo)
        {
            try
            {
                Todo updatedTodo = await todoService.UpdateToAsync(todo);
                return Ok(updatedTodo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<TodosController>/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteTodo([FromRoute] int id)
        {
            try
            {
                await todoService.RemoveTodoAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
