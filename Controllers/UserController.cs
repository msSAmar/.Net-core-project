using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using updateApi.Models;
using updateApi.Services;

namespace updateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    ///////////////////the main controller, login for users and admin functions too.
    public class UserController : ControllerBase
    {
        IOUserService UserServices;
        public UserController(IOUserService User)
        {
            this.UserServices = User;
        }
      ///////////////login for users
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> UserLogin( User user)
        {
       /////////////check if this user exist this user.
           User u=this.UserServices.Exist(user);
           if(u==null){
                return Unauthorized();
            }
        //////////creating a token according to the user's type
           var claims = new List<Claim>();
            if(u.IsAdmin=="true") {
                claims.Add(new Claim("type", "Admin"));
            }
            else {
                claims.Add(new Claim("type", "User"));
            }
            claims.Add(new Claim("id",user.Mail.ToString()));
            claims.Add(new Claim("name",user.UserName.ToString()));

            var token = TokenService.GetToken(claims);
           ///////////////return the token from the TokenService
             
            return new OkObjectResult(TokenService.WriteToken(token));
        
        }
    

  /////////////generate user for the admin
        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult GenerateUser( User user)
        {
        this.UserServices.add(user);    
        var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("Id", user.Mail.ToString()),
            };

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }
        /////////////get the users from UserService
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<User> Get()
        {
            return UserServices.GetAll();
        }
////////////////delete user
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete (string id)
        {
            if (! UserServices.Delete(id))
                return NotFound();
            return NoContent();            
        }
    }
}
