using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TasksSignalR.Data
{
    public class TaskRepository
    {
        private string _connectionString;
        private UserRepository _urepo;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
            _urepo = new UserRepository(_connectionString);
        }

        public Chore AddChore(Chore chore)
        {
            using (var ctx = new TaskContext(_connectionString))
            {
                chore.Status = Status.DidntStart;
                ctx.Chores.Add(chore);
                ctx.SaveChanges();
                return chore;
            }
        }

        public IEnumerable<Chore> GetIncompleteChores()
        {
            using (var ctx = new TaskContext(_connectionString))
            {
                return ctx.Chores.Include(c => c.User).Where(c => c.Status != Status.Completed).ToList();
            }
        }

        public Chore AcceptChore(Chore chore)
        {
            using (var ctx = new TaskContext(_connectionString))
            {
                chore.User = _urepo.GetUserByEmail(chore.User.Email);
                ctx.Database.ExecuteSqlCommand(
                   "UPDATE Chores SET UserId = @id, Status = @status WHERE Id = @choreId",
                   new SqlParameter("@id", chore.User.Id), 
                   new SqlParameter("@status", Status.InProgress), 
                   new SqlParameter("@choreId", chore.Id));
                return chore;
            }
        }


        public void CompleteChore(Chore chore)
        {
            using (var ctx = new TaskContext(_connectionString))
            {
                ctx.Database.ExecuteSqlCommand(
                   "UPDATE Chores SET Status = @status WHERE Id = @choreId",
                   new SqlParameter("@status", Status.Completed),
                   new SqlParameter("@choreId", chore.Id));
            }
        }


    }
}