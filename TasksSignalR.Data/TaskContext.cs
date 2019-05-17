using Microsoft.EntityFrameworkCore;

namespace TasksSignalR.Data
{
    public class TaskContext : DbContext
    {
        private string _connectionString { get; set; }

        public TaskContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chore> Chores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }




}
