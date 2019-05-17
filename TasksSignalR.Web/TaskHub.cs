using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksSignalR.Data;

namespace TasksSignalR.Web
{
    public class TaskHub : Hub
    {
        private string _connectionString;
        private TaskRepository _repo;

        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _repo = new TaskRepository(_connectionString);
        }

        public void AddChore(Chore chore)
        {
            Chore c = _repo.AddChore(chore);
            Clients.All.SendAsync("NewChore", c);
        }

        public void AcceptChore(Chore chore)
        {
            Chore c = _repo.AcceptChore(chore);
            Clients.Others.SendAsync("SomeoneAcceptedChore", c);
            Clients.Caller.SendAsync("IAcceptedChore", c);
        }

        public void CompletedChore(Chore chore)
        {
            _repo.CompleteChore(chore);
            Clients.All.SendAsync("RemoveChore", chore);
        }
    }
}
