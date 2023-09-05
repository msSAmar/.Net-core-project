using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using updateApi.Models;
using updateApi.Services;

namespace updateApi.controllers
{
[ApiController]
[Route("[controller]")]

    public class TaskController : ControllerBase
    {
       IOTaskServices TaskServices;
        public TaskController(IOTaskServices Task)
        {
            this.TaskServices = Task;
        }
    /////get the Id-Mail of the user from the Token
     
    [HttpGet()]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public ActionResult<List<Assiment>> Get()
    {
        string mail =TokenService.GetMailFromToken(Request.Headers.Authorization);
        List<Assiment> t = this.TaskServices.Get(mail);
        if (t == null)
            return NotFound();
            return t;
    }
    
    [HttpPost]
    [Authorize(Policy = "User")]
        public ActionResult Post(Assiment assiment)
        {
        string mail =TokenService.GetMailFromToken(Request.Headers.Authorization);
        assiment.Mail=mail;
            this.TaskServices.Add(assiment);

            return CreatedAtAction(nameof(Post), new { id = assiment.Mail }, assiment);
        }
     [HttpPut("{idTask}")]
    [Authorize(Policy = "User")]
        public ActionResult Put(int idTask, Assiment assiment)
        {
        string mail =TokenService.GetMailFromToken(Request.Headers.Authorization);
            if (! this.TaskServices.Update(mail,idTask,assiment))
                return BadRequest();
            return NoContent();
        }
        [HttpDelete("{idTask}")]
        [Authorize(Policy = "User")]
        public ActionResult Delete (int idTask)
        {
        string mail = TokenService.GetMailFromToken(Request.Headers.Authorization);
            if (! this.TaskServices.Delete(mail,idTask))
                return NotFound();
            return NoContent();            
        }
 


}

}
