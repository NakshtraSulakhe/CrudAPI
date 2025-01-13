using CrudAPI.Data;
using CrudAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Students>>> GetStudents(int id) 
        {
            var Data = await dbContext.Students.ToListAsync();
            return Ok(Data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudentsById(int id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NoContent();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Students>> CreateStudents(Students std)
        {
            
            await dbContext.Students.AddAsync(std);
            await dbContext.SaveChangesAsync(); 

            return Ok(std);

      
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Students>> UpdateStudents(int id, Students std)
        {

          if(id != std.Id)
            {
                return BadRequest();
            }

            dbContext.Entry(std).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            return Ok(std);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudents(int id)
        {

            var std = await dbContext.Students.FindAsync(id);
            if (id != std.Id)
            {
                return NotFound();
            }

            dbContext.Students.Remove(std);
            await dbContext.SaveChangesAsync();

            return Ok();

        }
    }


}
