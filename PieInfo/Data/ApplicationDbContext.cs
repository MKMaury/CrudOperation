using Microsoft.EntityFrameworkCore;
using PieInfo.Models;

namespace PieInfo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
