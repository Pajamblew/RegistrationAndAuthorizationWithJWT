using DBLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLibrary
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
            
        }
        public DataContext()
        {

        }
        public DbSet<User> Users { get; set; }
    }
}