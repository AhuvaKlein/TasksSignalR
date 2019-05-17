using System.Linq;

namespace TasksSignalR.Data
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddUser(User user)
        {
            using (var context = new TaskContext(_connectionString))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                context.Users.Add(user);
                context.SaveChanges();
            }
            return user.Id;
        }

        public bool LoginVerify(User user)
        {
            using (var context = new TaskContext(_connectionString))
            {
                User u = GetUserByEmail(user.Email);
                bool verify = false;
                if (u != null)
                {
                    verify = BCrypt.Net.BCrypt.Verify(user.Password, u.Password);
                }
                return verify;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var context = new TaskContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
    }


}
