﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNPL9.Data
{
    public class TodoServices : ITodoService
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoServices()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }
        public async Task<IList<Todo>> GetTodosAsync()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            todos.Add(todo);
            WriteTodosToFile();
            return todo;
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            todos.Remove(toRemove);
            WriteTodosToFile();
        }

        public async Task<Todo> UpdateToAsync(Todo todo)
        {
            Todo todoUpdate = todos.FirstOrDefault(t => t.TodoId == todo.TodoId);
            if (todoUpdate == null)
            {
                throw new ArgumentException($"Cannot find todo with id {todo.TodoId}");
            }
            todoUpdate.IsCompleted = todo.IsCompleted;
            WriteTodosToFile();
            return todoUpdate;
        }

        private void WriteTodosToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(todos);

            File.WriteAllText(todoFile, productsAsJson);
        }

        private void Seed()
        {
            Todo[] ts = {
            new Todo {
                UserId = 1,
                TodoId = 1,
                Title = "Do dishes",
                IsCompleted = false
            },
            new Todo {
                UserId = 1,
                TodoId = 2,
                Title = "Walk the dog",
                IsCompleted = false
            },
            new Todo {
                UserId = 2,
                TodoId = 3,
                Title = "Do DNP homework",
                IsCompleted = true
            },
            new Todo {
                UserId = 3,
                TodoId = 4,
                Title = "Eat breakfast",
                IsCompleted = false
            },
            new Todo {
                UserId = 4,
                TodoId = 5,
                Title = "Mow lawn",
                IsCompleted = true
            },
        };
            todos = ts.ToList();
        }
    }
}