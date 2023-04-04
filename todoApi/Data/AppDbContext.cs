using Microsoft.EntityFrameworkCore;
using todoApi.Models;

namespace todoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }


        public DbSet<ToDo> ToDos => Set<ToDo>();
    }
}
