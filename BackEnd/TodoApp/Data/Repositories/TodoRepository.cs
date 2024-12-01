using Dapper;
using Microsoft.Data.SqlClient;
using TodoApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly SqlConnection _connection;

        public TodoRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            await _connection.OpenAsync();
            return await _connection.QueryAsync<TodoItem>("GetAllTodoItems", commandType: CommandType.StoredProcedure);
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            await _connection.OpenAsync();
            var parameters = new { Id = id };
            return await _connection.QueryFirstOrDefaultAsync<TodoItem>("GetTodoItemById", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            await _connection.OpenAsync();
            var parameters = new
            {
                Name = item.Name,
                IsCompleted = item.IsCompleted
            };

            var newId = await _connection.ExecuteScalarAsync<int>("AddTodoItem", parameters, commandType: CommandType.StoredProcedure);
            item.Id = newId;

            return item;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            var parameters = new 
            { 
                Id = item.Id, 
                Name = item.Name, 
                IsCompleted = item.IsCompleted 
            };

            await _connection.ExecuteAsync("UpdateTodoItem", parameters, commandType: CommandType.StoredProcedure);

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _connection.OpenAsync())
            {

                var parameters = new { Id = id };
                Console.WriteLine(parameters);

                var rowsAffected = await _connection.ExecuteAsync("DeleteTodoItem", parameters, commandType: CommandType.StoredProcedure);
                Console.WriteLine("rowsAffected", rowsAffected);

                return rowsAffected > 0;
            }
        }

    }

}