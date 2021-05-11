using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_dotnet_core.Controllers;
using aspnet_core_dotnet_core.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestTechAwsLogin.Models;
using TestTechAwsLogin.Service;

namespace TestTechAwsLogin.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserModelService _userService;

        public UserController(IUserModelService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Route("/")]
        public ActionResult<List<UserModel>> Get() =>
            this.HandleServiceResponse(_userService.Get());

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        //[Route("/user/")]
        public ActionResult<UserModel> Get(string id)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                    return NotFound();

                return user;
            }
            catch (Exception e)
            {
                return this.HandleServerError<UserModel>(e);
            }
        }

        [HttpPost]
        //[Route("/user/create/")]
        public ActionResult<UserModel> Create(UserModel user)
        {
            try
            {
                _userService.Create(user);

                return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
            }
            catch (Exception e)
            {
                return this.HandleServerError<UserModel>(e);
            }
        }

        [HttpPut("{id:length(24)}")]
        //[Route("/user/update/")]
        public IActionResult Update(string id, UserModel userToUpdate)
        {
            
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                    return NotFound();

                _userService.Update(id, userToUpdate);
            }
            catch (Exception e)
            {
                return this.HandleServerError(e);
            }
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        //[Route("/user/delete/")]
        public IActionResult Delete(string id)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                    return NotFound();

                _userService.Remove(user.Id);
            }
            catch (Exception e)
            {
                return this.HandleServerError(e);
            }

            return NoContent();
        }

        
    }
}
