using Microsoft.EntityFrameworkCore;
using ServerTraining.Models;

namespace FirstWebApp.Data
{
    public class DataLayer : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataLayer(DbContextOptions<DataLayer> options) : base(options)
        {
            Console.WriteLine("ReCreated Database " + Database.EnsureCreated());
        }
    }
}
