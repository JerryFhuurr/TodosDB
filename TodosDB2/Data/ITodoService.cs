using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosDB2.Data
{
    public interface ITodoService
    {
        Task<IList<Todo>> GetTodosAsync();
        Task<Todo> AddTodoAsync(Todo todo);
        Task RemoveTodoAsync(int id);
        Task<Todo> UpdateToAsync(Todo todo);
    }
}
