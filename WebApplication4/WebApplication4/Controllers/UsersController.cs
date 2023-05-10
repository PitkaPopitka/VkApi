using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using WebApplication4.DB_Context;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersContext _users;
        public UsersController(UsersContext users)
        {
            _users = users;
        }

        [HttpGet]
        [Route("UsersList")]
        public IActionResult UsersList()
        {
            var usersList = _users.UserAccs
                .Join(_users.Groups, user => user.UserGroupId, group => group.Id, (user, group) => new {user, group})
                .Join(_users.States, u => u.user.UserStateId, state => state.Id, (u, state) => new {u.user, u.group, state = state.Name})
                .Select(u => new 
                {
                    Id = u.user.Id,
                    Login = u.user.Login,
                    Password = u.user.Password,
                    CreatedDate = u.user.CreatedDate,
                    UserGroup = u.group.Name,
                    UserState = u.state
                }).OrderBy(u => u.Id).ToList();
            ViewBag.Title = "usersList";
            var json = JsonConvert.SerializeObject(usersList);
            return View(model: json);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var curUser = _users.UserAccs.First(u => u.Id == id);
            curUser.UserStateId = 2;
            await _users.SaveChangesAsync();
            return RedirectToAction("UsersList", "Users");
        }

        [HttpPost]
        [Route("RestoreUser")]
        public async Task<IActionResult> RestoreUser(int id) 
        {
            var curUser = _users.UserAccs.First(u => u.Id == id);
            curUser.UserStateId = 1;
            await _users.SaveChangesAsync();
            return RedirectToAction("UsersList", "Users");
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] NewUserViewModel model) 
        {
            //var model = JsonConvert.DeserializeObject<NewUserViewModel>(json.ToString());
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            var userExist = _users.UserAccs.First(u => u.Login == model.Login);
            if (userExist != null)
            {
                ViewBag.Message = "User with this login already exist";
                return View("~/Views/Users/NewUser.cshtml");
            }
            else 
            {
                var user = new UserAcc
                {
                    Id = _users.UserAccs.Select(u => u.Id).Max() + 1,
                    Login = model.Login,
                    Password = model.Password,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Parse(date)),
                    UserGroupId = 2,
                    UserStateId = 1
                };
            _users.UserAccs.Add(user);
            await _users.SaveChangesAsync();
            return RedirectToAction("NewUser", "Users");
            }
        }

        [HttpGet]
        [Route("NewUser")]
        public IActionResult NewUser() 
        {
            return View();
        }
    }
}
