using Microsoft.EntityFrameworkCore;
using Todo_App_Api.Models;

namespace Todo_App_Api.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }
}
