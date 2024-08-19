using FirstWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServerTraining.Models;

namespace ServerTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataLayer _data;
        public UsersController(DataLayer data)
        {
            _data = data; //Recieves from the service (Dependancy Injection)
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_data.Users);
        
         }
        [HttpPut("{oldname}")]
        public IActionResult EditUser([FromBody] User newUser,[FromRoute] string oldname)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "ModelState is invalid" });
            if (!_data.Users.Any(user => user.Name == oldname))
                return NotFound(new { message = "Your name doesn't exists in Database" });
            if (_data.Users.Any(user => user.Name == newUser.Name) && oldname != newUser.Name)
                return BadRequest(new {message = "This name is already taken"});

            User CurrentUser = _data.Users.FirstOrDefault(user => user.Name == oldname);
            if (CurrentUser == null)
                return BadRequest(new { message = "User not exists" });

            CurrentUser.Name = newUser.Name;
            CurrentUser.Email = newUser.Email;
            CurrentUser.Password = newUser.Password;
            _data.SaveChanges();
            return Ok();

        }
        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_data.Users.Any(user => user.Name == newUser.Name))
                return BadRequest(new { message = "There is already a user with this name", newUser });
            _data.Users.Add(newUser);
            _data.SaveChanges();
            return Ok(new {message= "User created ", newUser});

        }
        
    }
}
