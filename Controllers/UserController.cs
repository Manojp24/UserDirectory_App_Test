using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using UserDirectory_app.EF;
using UserDirectory_app.Model;

namespace UserDirectory_app.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public UserController(AppDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await appDbContext.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await appDbContext.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            appDbContext.Users.Add(user);
            await appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id },user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,User updated)
        {
            if (id != updated.Id) {
                return BadRequest();
            }

            appDbContext.Entry(updated).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await appDbContext.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            appDbContext.Users.Remove(user);
            await appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
