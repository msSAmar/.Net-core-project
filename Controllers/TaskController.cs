using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using updateApi.Models;


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
    
    // [HttpGet]
    // [Route("[action]")]
    // public IEnumerable<Assiment> Get()
    // {
    //   return this.TaskServices.GetAll();
    // }

    [HttpGet()]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public ActionResult<List<Assiment>> Get()
    {
        string tokenStr=Request.Headers.Authorization;
        string newToken=tokenStr.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string id = token.Claims.First(c => c.Type == "Id").Value;
        List<Assiment> t = this.TaskServices.Get(int.Parse(id));
        if (t == null)
            return NotFound();
            return t;
    }
    
    [HttpPost]
        public ActionResult Post(Assiment assiment)
        {
        string tokenStr=Request.Headers.Authorization;
        string newToken=tokenStr.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string id = token.Claims.First(c => c.Type == "Id").Value;
        assiment.Id=int.Parse(id);
            this.TaskServices.Add(assiment);

            return CreatedAtAction(nameof(Post), new { id = assiment.Id }, assiment);
        }
     [HttpPut("{id}")]
        public ActionResult Put(int idTask, Assiment assiment)
        {
        string tokenStr=Request.Headers.Authorization;
        string newToken=tokenStr.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string id = token.Claims.First(c => c.Type == "Id").Value;
            if (! this.TaskServices.Update(int.Parse(id),idTask,assiment))
                return BadRequest();
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete (int idTask)
        {
        string tokenStr=Request.Headers.Authorization;
        string newToken=tokenStr.Split(' ')[1];
        var token = new JwtSecurityToken(jwtEncodedString: newToken);
        string id = token.Claims.First(c => c.Type == "Id").Value;
            if (! this.TaskServices.Delete(int.Parse(id),idTask))
                return NotFound();
            return NoContent();            
        }
 


}

}
