using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API_Midursan_Velusamy_2031313.Data;
using Project_API_Midursan_Velusamy_2031313.Models;

namespace Project_API_Midursan_Velusamy_2031313.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        //2.6 + 2.7
        [HttpPost]
        public ActionResult Post(Dictionary<string, string> userInfo)
        {
            if (userInfo.Count < 3)
            {
                var user = _context.users.FirstOrDefault(x => x.Email == userInfo["Email"]);

                if (user.Password != userInfo["Password"] || user == null)
                {
                    return NotFound("User not Found!!!");
                }
                var session = new session() { Email = userInfo["Email"] };
           
                _context.sessions.Add(session);


                try
                {
                    _context.SaveChanges();
                }


                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                return Ok(session.Token);
            }
            else
            {
                var user = _context.users.FirstOrDefault(x => x.Email == userInfo["Email"]);
                if (user != null)
                {
                    return BadRequest("This user is found!");
                }

                user = new Models.user();
                user.Name = userInfo["Name"];
                user.Email = userInfo["Email"];
                user.Password = userInfo["Password"];
               

                _context.users.Add(user);


                s
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                return Ok(user);
            }
        }

    } 
}