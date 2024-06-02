using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_App_Api.Data;
using Todo_App_Api.Models;

namespace Todo_App_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;
        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;   
        }

        [HttpGet]
        [Route("Scheduletodos")]
        public async Task<IActionResult> GetAllScheduleTodos()
        {
            var todods = await _todoDbContext.Todos.Where(todo => todo.IsDeleted == false && todo.IsCompleted == false).ToListAsync();
            if(todods is not null)
            {
                return Ok(todods);
            }
            else
            {
                return BadRequest("something went wrong");
            }
        }

        [HttpGet]
        [Route("Completedtodos")]
        public async Task<IActionResult> GetAllCompletedTodos()
        {
            var todods = await _todoDbContext.Todos.Where(todo => todo.IsCompleted == true).ToListAsync();
            if (todods is not null)
            {
                return Ok(todods);
            }
            else
            {
                return BadRequest("something went wrong");
            }
        }

        [HttpGet]
        [Route("Deletedtodos")]
        public async Task<IActionResult> GetAllDeletedTodos()
        {
            var todods = await _todoDbContext.Todos.Where(todo => todo.IsDeleted == true).ToListAsync();
            if (todods is not null)
            {
                return Ok(todods);
            }
            else
            {
                return BadRequest("something went wrong");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreatedDate = DateTime.Now;
            if(!todo.IsCompleted)
            {
                todo.CompletedDate = null;
            }
            else
            {
                todo.CreatedDate = DateTime.Now;
            }
            if (!todo.IsDeleted)
            {
                todo.DeletedDate = null;
            }
            else
            {
                todo.DeletedDate = DateTime.Now;
            }
            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Updatetodo([FromRoute] Guid id, bool updaterequest)
        {
            var isAvailable = await _todoDbContext.Todos.FindAsync(id);
            if (isAvailable is null) return NotFound();
            isAvailable.IsCompleted = updaterequest;
            if (updaterequest)
            {
                isAvailable.CompletedDate = DateTime.Now;
            }
            else
            {
                isAvailable.CompletedDate = null;
            }
            await _todoDbContext.SaveChangesAsync();
            return Ok(isAvailable);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Deletetodo([FromRoute] Guid id)
        {
            var isExist = await _todoDbContext.Todos.FindAsync(id);
            if (isExist is null) return NotFound();
            isExist.IsDeleted = true;
            isExist.DeletedDate = DateTime.Now;
            await _todoDbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
