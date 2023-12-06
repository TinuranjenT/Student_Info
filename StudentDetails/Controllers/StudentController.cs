using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDetails.Data;
using StudentDetails.Model;

namespace StudentDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public StudentController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Student student = databaseContext.Students.FirstOrDefault(x => x.Id == id);
            return Ok(student);
        }
            
        [HttpPost]
        public IActionResult Create(Student student)
        {
            databaseContext.Add(student);
            databaseContext.SaveChanges();
            return Ok(student);
        }

        
    }
}
