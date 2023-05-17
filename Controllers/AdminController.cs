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

//using updateApi.interfaces;
using updateApi.Services;

namespace updateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AdminController : ControllerBase
    {
        IOUserService UserServices;
        public AdminController(IOUserService User)
        {
            this.UserServices = User;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] Admin admin)
        {
            var dt = DateTime.Now;

            if (admin.Name != "shira"
            || admin.Password != $"12345")
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }  
        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult GenerateUser([FromBody] User user)
        {
        this.UserServices.add(user);    
        var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("Id", user.Id.ToString()),
            };

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UserLogin([FromBody] User user)
        {
           User u=this.UserServices.Exist(user.Password);
            
           if(u==null){
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("Id", user.Id.ToString()),
            };

            var token = TokenService.GetToken(claims);
           // var tokenId = new JwtSecurityToken(jwtEncodedString: user.Id);
             
            return new OkObjectResult(TokenService.WriteToken(token));
        
        }
    }



}
