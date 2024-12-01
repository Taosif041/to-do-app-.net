using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;
using TodoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todoItems = await _todoService.GetAllAsync();
            if (todoItems == null)
                return NotFound();

            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todoItem = await _todoService.GetByIdAsync(id);
            if(todoItem == null) 
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TodoItem item)
        {
            if(item == null)
            {
                return BadRequest("The new item body is not properly written. @todo -> post request");
            }
            var newItem = await _todoService.AddAsync(item);

            return CreatedAtAction(nameof(GetById), new { item.Id }, newItem);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest("Item cannot be null.");
            }

            // Ensure the item has a valid ID (assuming ID is required for updates)
            if (item.Id <= 0)
            {
                return NotFound("Invalid item ID.");
            }

            var updatedItem = await _todoService.UpdateAsync(item);

            if (updatedItem == null)
            {
                return NotFound("Item not found.");
            }

            return Ok(updatedItem);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var isDeleted = await _todoService.DeleteAsync(id);
                if (isDeleted == false)
                {
                    return NotFound();
                }
                return NoContent();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
                return StatusCode(500, $"An error occurred while deleting the item. -> DeleteAsync. {ex.Message}");
            }
        }
    }
}
