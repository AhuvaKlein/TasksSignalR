using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TasksSignalR.Data;
using TasksSignalR.Web.Models;

namespace TasksSignalR.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private TaskRepository _repo;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _repo = new TaskRepository(_connectionString);
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Chore> chores = _repo.GetIncompleteChores();
            return View(chores);
        }

      

    }
}
