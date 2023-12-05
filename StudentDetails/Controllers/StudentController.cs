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
            List<Student> students = new List<Student>();
            var addressInfo = databaseContext.Student_Address_Info.Add(new StudentAddress { AddressId = 1, Address = "Kovilpatti" });
            databaseContext.Student_Address_Info.Add(new StudentAddress { AddressId = 2, Address = "Madurai" });
            databaseContext.Student_Address_Info.Add(new StudentAddress { AddressId = 3, Address = "Tirunelveli" });

            students = databaseContext.Student_Information.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Student student = databaseContext.Student_Information.FirstOrDefault(x => x.Id == id);
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
