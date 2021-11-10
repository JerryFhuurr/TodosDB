
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodosDB2.Data;

namespace TodosDB2.Persistence
{
    internal class SqliteTodoService : ITodoService
    {
        private TodoContext todoContext;
        public SqliteTodoService(TodoContext todoContext)
        {
            this.todoContext = todoContext;
        }
        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            EntityEntry<Todo> todoAdd = await todoContext.Todos.AddAsync(todo);
            await todoContext.SaveChangesAsync();
            return todoAdd.Entity;
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            return await todoContext.Todos.ToListAsync();
        }

        public async Task RemoveTodoAsync(int id)
        {
            Todo todoRemove = await todoContext.Todos.FirstAsync(t => t.TodoId == id);
            if (todoRemove != null)
            {
                todoContext.Todos.Remove(todoRemove);
                await todoContext.SaveChangesAsync();
            }
        }

        public async Task<Todo> UpdateToAsync(Todo todo)
        {
            try
            {
                Todo todoUpdate = await todoContext.Todos.FirstOrDefaultAsync(t => t.TodoId == todo.TodoId);
                todoUpdate.IsCompleted = todo.IsCompleted;
                todoContext.Todos.Update(todoUpdate);
                todoContext.Update(todoUpdate);
                await todoContext.SaveChangesAsync();
                return todoUpdate;

            }
            catch (Exception ex)
            {
                throw new Exception($"Did not find todo with id{todo.TodoId}");
            }


        }
    }
}
