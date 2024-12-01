using TodoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Data.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);

        Task<TodoItem> AddAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);

        Task<bool> DeleteAsync(int id);

  
    }
}
