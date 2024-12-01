using TodoApp.Data.Repositories;
using TodoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item) 
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            return await _todoRepository.AddAsync(item);
        }


        public async Task<TodoItem> UpdateAsync(TodoItem item) 
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }
            var cuurentItem = await _todoRepository.GetByIdAsync(item.Id);
            if (cuurentItem == null)
            {
                throw new ArgumentException(nameof(item), "Item not found");
            }
            return await _todoRepository.UpdateAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cuurentItem = await _todoRepository.GetByIdAsync(id);
            Console.WriteLine(cuurentItem.Id);
            if (cuurentItem == null)
            {
                throw new ArgumentException(nameof(id), "Item not found");
            }
            else
            {
                Console.WriteLine($"item found {cuurentItem.Id}");
                return await _todoRepository.DeleteAsync(id);
            }
        }
    }
}
