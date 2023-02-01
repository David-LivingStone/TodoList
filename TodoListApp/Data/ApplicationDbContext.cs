using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListApp.Models;

namespace TodoListApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TodoListApp.Models.TodoListModel> TodoListModel { get; set; }
        public DbSet<TodoListApp.Models.ApplicationUser> ApplicationUser { get; set; }

        internal Task FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
