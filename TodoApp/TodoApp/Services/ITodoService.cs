using TodoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task<TodoItem> AddAsync(TodoItem item); 
        Task<TodoItem> UpdateAsync(TodoItem item);

        Task<bool> DeleteAsync(int id);
    }
}
