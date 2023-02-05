using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API_Midursan_Velusamy_2031313.Data;
using Project_API_Midursan_Velusamy_2031313.Models;
using System.Linq;

namespace Project_API_Midursan_Velusamy_2031313.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }




        //2.1
        [HttpPost]
        public ActionResult Post(Dictionary<string, string> taskInfo)
        {
            var token = _context.sessions.FirstOrDefault(x => x.Token == taskInfo["Token"]);
            if (token is null)
            {
                return NotFound("Token's invalid!");
            }


            var usersEmail = token.Email;
            var createdByUser = _context.users.FirstOrDefault(x => x.Email == usersEmail);
            var assignedToUser = _context.users.FirstOrDefault(x => x.UId == taskInfo["AssignedToUid"]);
            
            
            if (assignedToUser is null)
            {
                return NotFound("AssignedToUid is invalid!");
            }


            var assignedToName = assignedToUser.Name;

            var task = new Models.task();
            task.CreatedByName = createdByUser.Name;
            task.CreatedByUid = createdByUser.UId;
            task.AssignedToName = assignedToName;
            task.AssignedToUid = taskInfo["AssignedToUid"];
            task.Description = taskInfo["Description"];

            _context.tasks.Add(task);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(task);
        }




        // 2.2
        [HttpGet("CreatedBy/{token}")]
        public ActionResult<IEnumerable<Models.task>> CreatedBy(string token)
        {
            var session = _context.sessions.FirstOrDefault(x => x.Token == token);
            
            if (session == null)
            {
                return NotFound("Token is not found!");
            }

            var user = _context.users.FirstOrDefault(x => x.Email == session.Email);
            var tasks = _context.tasks.Where(x => x.CreatedByUid == user.UId).ToList();

            if (tasks.Count == 0 || tasks == null)
            {
                return NotFound("task not found!!!");
            }

            return tasks;
        }





        //2.3
        [HttpGet("AssignedTo/{token}")]
        public ActionResult<IEnumerable<Models.task>> AssignedTo(string token)
        {
            var session = _context.sessions.FirstOrDefault(x => x.Token == token);

            if (session == null)
            {
                return NotFound("Token is not found!");
            }

            var user = _context.users.FirstOrDefault(x => x.Email == session.Email);
            var tasks = _context.tasks.Where(x => x.AssignedToUid == user.UId).ToList();

            if (tasks.Count == 0 || tasks == null)
            {
                return NotFound("task not found!!!");
            }

            return tasks;
        }





        //2.4
        [HttpPut("{taskUid}")]
        public ActionResult Put(Dictionary<string, object> updatedTask, string taskUid)
        {
            var token = updatedTask["Token"].ToString();
            var done = updatedTask["Done"].ToString();

            var session = _context.sessions.FirstOrDefault(x => x.Token == token);
            if (session is null)
            {
                return NotFound("token is not found !!");
            }

            var user = _context.users.FirstOrDefault(x => x.Email == session.Email);

            var task = _context.tasks.FirstOrDefault(x => x.TaskUId == taskUid);
            if (task == null)
            {
                return NotFound("task is not found!!!!");
            }


            if (task.AssignedToUid != user.UId)
            {
                return BadRequest("Task cannot be deleted!!!");
            }


            task.Done = Convert.ToBoolean(done);
            _context.tasks.Entry(task).State = EntityState.Modified;


            try
            {
                _context.SaveChanges();
            }


            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(task);
        }



        //2.5
        [HttpDelete("{taskUid}")]
        public ActionResult Delete(Dictionary<string , string> token, string taskUid)
        {
            var session = _context.sessions.FirstOrDefault(x => x.Token == token["Token"]);
            if (session is null)
            {
                return NotFound("Token is not Found !!");
            }
            
            var user = _context.users.FirstOrDefault(x => x.Email == session.Email);

            var task = _context.tasks.FirstOrDefault(x => x.TaskUId == taskUid);
            if (task == null)
            {
                return NotFound("task is not found!!!!");
            }

           
            if (task.CreatedByUid != user.UId)
            {
                return BadRequest("Task cannot be deleted!!!");
            }

            _context.tasks.Remove(task);
            
            
            try
            {
                _context.SaveChanges();
            }
            
            
            
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(task);
        }
    }
}