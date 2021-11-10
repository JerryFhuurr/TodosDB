using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNPL9.Persistence
{
    public class TodoContext : DbContext

    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //name of database
            optionsBuilder.UseSqlite("Data Source = Todos.db");
        }

        /*
         * install tools:
         * in powershell:
         * dotnet tool install -g dotnet-ef
         */

        /*
         * create a migration
         * autogenerate code which sets up the database
         * When you change your domain classes, or add new, you can create and apply a new migration
         * In the project folder:
         * dotnet ef migrations add InitialCreate (replace InitialCreate to other name)
         */

        /*
         * apply the migration:
         * dotnet ef database update
         */
    }
}
