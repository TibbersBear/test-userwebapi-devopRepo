using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTechAwsLogin.Models;
using TestTechAwsLogin.Service;

namespace TestTechAwsLogin.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserModelService _userService;

        public UserController(UserModelService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Route("/")]
        public ActionResult<List<UserModel>> Get() =>
            _userService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        //[Route("/user/")]
        public ActionResult<UserModel> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        //[Route("/user/create/")]
        public ActionResult<UserModel> Create(UserModel user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        //[Route("/user/update/")]
        public IActionResult Update(string id, UserModel userToUpdate)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            _userService.Update(id, userToUpdate);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        //[Route("/user/delete/")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
                return NotFound();

            _userService.Remove(user.Id);

            return NoContent();
        }
    }
}
